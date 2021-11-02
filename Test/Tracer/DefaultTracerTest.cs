using System;
using System.Linq;
using System.Threading;
using Library.Tracer;
using Library.Tracer.Impl;
using NUnit.Framework;

namespace Test.Tracer
{
    public class DefaultTracerTest
    {
        private ITracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new DefaultTracer();
        }

        [Test]
        public void StartTraceTest()
        {
            _tracer.StartTrace();

            Assert.NotNull(_tracer.GetTraceResult().Threads.Values.ToArray()[0]);
            var thread = _tracer.GetTraceResult().Threads.Values.ToArray()[0];
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            Assert.AreEqual(currentThreadId, thread.Id);

            Assert.NotNull(thread.Methods.ToArray()[0]);
            var method = thread.Methods.ToArray()[0];

            var expectedMethodName = "StartTraceTest";
            Assert.AreEqual(expectedMethodName, method.MethodName);

            var expectedClassName = "DefaultTracerTest";
            Assert.AreEqual(expectedClassName, method.ClassName);
        }

        [Test]
        public void StopTraceTest()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
            var thread = _tracer.GetTraceResult().Threads.Values.ToArray()[0];
            var method = thread.Methods.First();
            var time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - method.Time;

            Assert.True(method.IsCompleted);
            Assert.True(method.Time <= time);
        }

        [Test]
        public void GetTraceResultTest()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
            var thread = _tracer.GetTraceResult().Threads.Values.ToArray()[0];
            long time = 0;
            foreach (var method in thread.Methods) time += method.Time;
            Assert.AreEqual(time, thread.Time);
        }
    }
}