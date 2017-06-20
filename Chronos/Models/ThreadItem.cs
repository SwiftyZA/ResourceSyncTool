using Chronos.Enums;

namespace Chronos.Models
{
    internal class ThreadItem<T>
    {
        public T JobItem { get; set; }

        public int Id { get; set; }

        public ThreadStatus Status { get; set; }
    }
}
