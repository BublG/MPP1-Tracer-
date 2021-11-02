using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Library.Model;

namespace Main.Serialization.Impl
{
    public class XmlSerialization : ISerialization
    {
        public string Serialize(TraceResult traceResult)
        {
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(Thread[]));
            serializer.Serialize(stringWriter, traceResult.Threads.Values.ToArray());
            return stringWriter.ToString();
        }
    }
}