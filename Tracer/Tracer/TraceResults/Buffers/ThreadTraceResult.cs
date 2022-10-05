using Lab1.Tracer.TraceResults.ReadOnly;


namespace Lab1.Tracer.TraceResults.Buffers
{
    internal class ThreadTraceResult
    {
        public Stack<MethodTraceResult> RunningMethods = new Stack<MethodTraceResult>();

        public List<ReadOnlyMethodTraceResult> Methods = new List<ReadOnlyMethodTraceResult>();
    }
}
