using Lab1.Tracer.TraceResults;

namespace Lab1.Serialization
{
    public interface ITraceSerializer
    {
        public string Serialaze(TraceResult result);
    }
}
