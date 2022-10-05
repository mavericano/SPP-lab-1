using System.Collections.Generic;
using System.Diagnostics;
using Lab1.Tracer.TraceResults.ReadOnly;

namespace Lab1.Tracer.TraceResults.Buffers
{
    internal class MethodTraceResult
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public Stopwatch Stopwatch { get; set; } = new();
        public List<ReadOnlyMethodTraceResult> ChildMethods = new();
    }
}
