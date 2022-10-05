using Lab1.Tracer.TraceResults.ReadOnly;
using System.Xml;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Collections.Immutable;

namespace Lab1.Tracer.TraceResults
{
    [XmlRoot(ElementName = "root")]
    public class TraceResult
    {
        private List<ReadOnlyThreadTraceResult> _threads;

        [XmlElement(ElementName = "Thread"), JsonPropertyName("Threads")]
        public ImmutableList<ReadOnlyThreadTraceResult> Threads { get => _threads.ToImmutableList(); }

        public TraceResult(List<ReadOnlyThreadTraceResult> threads)
        {
            _threads = threads;
        }

        public TraceResult()
        {
            _threads = new List<ReadOnlyThreadTraceResult>();
        }
    }
}
