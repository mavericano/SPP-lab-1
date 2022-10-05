using System.Xml.Serialization;
using System.Xml;
using Lab1.Tracer.TraceResults;
using System.Text;

namespace Lab1.Serialization
{
    public class XmlTraceSerializer : ITraceSerializer
    {
        public string Serialaze(TraceResult result)
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(result.GetType());

            StringBuilder stringBuilder = new StringBuilder();

            using (XmlWriter xw = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Indent = true }))
            {
                xmlSerializer.Serialize(xw, result);
            }

            return stringBuilder.ToString();
        }
    }
}
