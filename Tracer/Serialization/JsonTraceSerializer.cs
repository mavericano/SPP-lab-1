using System.Text.Json;
using Lab1.Tracer.TraceResults;

namespace Lab1.Serialization
{
    public class JsonTraceSerializer : ITraceSerializer
    {
        public string Serialaze(TraceResult result)
        {
            JsonSerializerOptions options = new();
            options.WriteIndented = true;
            return JsonSerializer.Serialize(result, options);
        }
    }
}
