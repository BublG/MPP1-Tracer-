using System.Threading;
using Library.Tracer;

namespace Main.Classes
{
    public class Bar
    {
        private readonly ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}