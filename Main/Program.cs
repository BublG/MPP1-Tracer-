using System.IO;
using System.Threading;
using Library.Tracer;
using Library.Tracer.Impl;
using Main.Classes;
using Main.Output;
using Main.Output.Impl;
using Main.Serialization;
using Main.Serialization.Impl;

namespace Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileInfo = new FileInfo(FileOutput.FilePath);
            if (fileInfo.Exists) fileInfo.Delete();

            ITracer tracer = new DefaultTracer();
            var thread1 = new Thread(TestThread1);
            var thread2 = new Thread(TestThread2);

            thread1.Start(tracer);
            thread2.Start(tracer);

            thread1.Join();
            thread2.Join();

            IOutput fileOutput = new FileOutput();
            IOutput consoleOutput = new ConsoleOutput();
            ISerialization xmlSerialization = new XmlSerialization();
            ISerialization jsonSerialization = new JsonSerialization();
            var xml = xmlSerialization.Serialize(tracer.GetTraceResult());
            var json = jsonSerialization.Serialize(tracer.GetTraceResult());
            fileOutput.Print(xml + '\n' + json);
            consoleOutput.Print(xml + '\n' + json);
        }

        private static void TestThread1(object tracer)
        {
            var foo = new Foo((ITracer) tracer);
            foo.MyMethod();
        }

        private static void TestThread2(object tracer)
        {
            var foo = new Foo((ITracer) tracer);
            foo.MyMethod();
            var bar = new Bar((ITracer) tracer);
            bar.InnerMethod();
        }
    }
}