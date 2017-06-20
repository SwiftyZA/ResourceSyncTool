using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Chronos.Enums;
using Chronos.Models;
using Common;
using GoogleAPIClient;

namespace Chronos
{
    public static class Chronos
    {
        static Timer _chronos;
        static readonly ConcurrentQueue<ResXEntry> ResXEntries = new ConcurrentQueue<ResXEntry>();
        static ThreadSafeList<ResXEntry> threads;
        static ChronosParameters _parameters;
        static WaitHandle[] _stopHandle;

        public static event EventHandler<ChronosEvents> OutputEvent;
        public static event EventHandler<ChronosEvents> CompletedEvent;

        public static void StartProcessing(ChronosParameters parameters)
        {
            _parameters = parameters;
            threads = new ThreadSafeList<ResXEntry>();
            _chronos = new Timer(InsertJob, null, 0, 1000 / _parameters.RequestsPerSecond);
        }

        public static void AddResXEntries(List<ResXEntry> entries)
        {
            entries.ForEach(x => ResXEntries.Enqueue(x));
        }

        private static void InsertJob(object stateInfo)
        {
            //Purger trackers in done status
            threads.PurgeDoneTrackers();
            //Check if there are any translations to be done
            if (ResXEntries.Any())
            {
                //Check if we have available threads
                if (threads.Count >= _parameters.MaxNumberOfActiveThreads) return;
                //Drop job in thread pool
                ThreadPool.QueueUserWorkItem(HitGoogle);
            }
            else if (threads.Count == 0)
            {
                //If nothing is found, kill the timer
                StopChronos();
                CompletedEvent.Invoke(stateInfo, null);
            }
        }

        private static async void HitGoogle(object state)
        {
            //Add tracker item thread-safe list
            var threadTracker = new ThreadItem<ResXEntry>
            {
                Id = Thread.CurrentThread.ManagedThreadId,
                Status = ThreadStatus.New
            };
            threads.Add(threadTracker);

            //Get next entry to process
            ResXEntry entry;
            ResXEntries.TryDequeue(out entry);
            //If none is found, we're done, exit
            if (entry == null)
            {
                //Update tracker item
                threadTracker.Status = ThreadStatus.Done;
                return;
            }
            //Update tracker item
            threadTracker.Status = ThreadStatus.InProgres;
            using (var _apiClient = new DeepThroat())
            {
                int tries = 0;
                do
                {
                    try
                    {
                        entry.LocalizedValue = await _apiClient.TranslateText("en", _parameters.Culture.Name, entry.Value);
                        if (!string.IsNullOrWhiteSpace(entry.Comment))
                        {
                            entry.LocalizedComment = await _apiClient.TranslateText("en", _parameters.Culture.Name, entry.Comment);
                        }
                        threadTracker.Status = ThreadStatus.InProgressGoogleResponded;
                    }
                    catch (Exception e)
                    {
                        tries++;
                        threadTracker.Status = ThreadStatus.Faulted;
                    }
                } while (threadTracker.Status == ThreadStatus.Faulted && tries < 3);
            }

            OutputEvent.Invoke(state, new ChronosEvents()
            {
                JobItem = entry
            });

            threadTracker.Status = ThreadStatus.Done;
        }

        private static WaitHandle[] StopChronos()
        {
            _stopHandle = new WaitHandle[]
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false),
            };

            if (_chronos == null) return _stopHandle;
            _chronos.Change(Timeout.Infinite, Timeout.Infinite);
            _chronos.Dispose(_stopHandle[0]);

            return _stopHandle;
        }
    }
}
