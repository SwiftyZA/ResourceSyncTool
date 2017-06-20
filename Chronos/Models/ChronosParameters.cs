using System;
using System.Globalization;

namespace Chronos.Models
{
    public class ChronosParameters
    {
        public int MaxNumberOfActiveThreads { get; set; }
        public int RequestsPerSecond { get; set; }
        public CultureInfo Culture { get; set; }
    }
}
