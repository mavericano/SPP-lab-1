using System.Collections.Immutable;
using System.Xml.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Lab1.Tracer.TraceResults.ReadOnly
{
    public class ReadOnlyThreadTraceResult
    {
        private List<ReadOnlyMethodTraceResult> _methods;

        private int _id;
        private long _time;

        [XmlAttribute(AttributeName = "id")]
        public int ID {
            get => _id;
            set => _id = _id;
        }

        [XmlAttribute(AttributeName = "time")]
        public long Time
        {
            get => _time;
            set => _time = _time;
        }

        [XmlElement(ElementName = "Method"), JsonPropertyName("Methods")]
        public ImmutableList<ReadOnlyMethodTraceResult> Methods { get => _methods.ToImmutableList(); }

        public ReadOnlyThreadTraceResult(int id, List<ReadOnlyMethodTraceResult> methods)
        {
            _id = id;
            _methods = methods;
            _time = methods.Sum(method => method.ExecutionTime);
        }

        public ReadOnlyThreadTraceResult()
        {
            _id = 0;
            _time = 0;
            _methods = new();
        }
    }
}
