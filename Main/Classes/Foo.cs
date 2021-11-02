using System.Threading;
using Library.Tracer;

namespace Main.Classes
{
    public class Foo
    {
        private readonly Bar _bar;
        private readonly ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(50);
            _bar.InnerMethod();
            Thread.Sleep(50);
            _tracer.StopTrace();
        }
    }
}