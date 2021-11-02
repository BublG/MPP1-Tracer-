using System.Linq;
using System.Text.Json;
using Library.Model;

namespace Main.Serialization.Impl
{
    public class JsonSerialization : ISerialization
    {
        public string Serialize(TraceResult traceResult)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            return JsonSerializer.Serialize(traceResult.Threads.Values.ToArray(), options);
        }
    }
}