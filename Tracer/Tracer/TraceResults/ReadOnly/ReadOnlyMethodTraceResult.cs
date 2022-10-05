using System.Collections.Immutable;
using System.Xml;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Lab1.Tracer.TraceResults.ReadOnly
{
    public class ReadOnlyMethodTraceResult
    {
        private List<ReadOnlyMethodTraceResult> _childMethods;

        private string _className;
        private string _methodName;
        private long _executionTime;

        [XmlAttribute(AttributeName = "class")]
        public string ClassName
        {
            get => _className;
            set => _className = _className;
        }

        [XmlAttribute(AttributeName = "name")]
        public string MethodName
        {
            get => _methodName;
            set => _methodName = _methodName;
        }

        [XmlAttribute(AttributeName = "executionTime")]
        public long ExecutionTime
        {
            get => _executionTime;
            set => _executionTime = _executionTime;
        }

        [XmlElement(ElementName = "Method"), JsonPropertyName("Methods")]
        public ImmutableList<ReadOnlyMethodTraceResult> ChildMethods { get => _childMethods.ToImmutableList(); }

        public ReadOnlyMethodTraceResult(string className, string methodName, long executionTime, List<ReadOnlyMethodTraceResult> childMethods)
        {
            _className = className;
            _methodName = methodName;
            _executionTime = executionTime;
            _childMethods = childMethods;
        }

        public ReadOnlyMethodTraceResult()
        {
            _className = "";
            _methodName = "";
            _executionTime = 0;
            _childMethods = new();
        }
    }
}
