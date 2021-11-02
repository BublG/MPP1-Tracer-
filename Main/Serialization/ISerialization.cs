using Library.Model;

namespace Main.Serialization
{
    public interface ISerialization
    {
        string Serialize(TraceResult traceResult);
    }
}