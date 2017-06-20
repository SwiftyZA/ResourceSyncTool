using System.Collections.Generic;
using Chronos.Enums;
using Chronos.Models;

namespace Chronos
{
    internal class ThreadSafeList<T>
    {
        private static List<ThreadItem<T>> Items = new List<ThreadItem<T>>();

        private object _lock = new object();

        internal void Add(ThreadItem<T> item)
        {
            lock (_lock)
            {
                Items.Add(item);
            }
        }

        internal void Remove(ThreadItem<T> item)
        {
            lock (_lock)
            {
                Items.Remove(item);
            }
        }

        internal int Count
        {
            get
            {
                lock (_lock)
                {
                    return Items.Count;
                }
            }
        }

        internal void PurgeDoneTrackers()
        {
            lock (_lock)
            {
                Items.RemoveAll(x => x.Status == ThreadStatus.Done);
            }
        }
    }
}
