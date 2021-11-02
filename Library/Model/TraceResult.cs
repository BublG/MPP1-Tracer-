using System.Collections.Concurrent;

namespace Library.Model
{
    public class TraceResult
    {
        public TraceResult()
        {
            Threads = new ConcurrentDictionary<int, Thread>();
        }

        public ConcurrentDictionary<int, Thread> Threads { get; }
    }
}