using System;
using System.Xml.Serialization;

namespace Core.Framework.Plugins.Handlers
{
    [XmlRoot("handler")]
    public class PluginHttpHandler : IPluginHttpHandler
    {
        [XmlElement("verb")]
        public String Verb { get; set; }

        [XmlElement("path")]
        public String Path { get; set; }

        [XmlElement("type")]
        public String HandlerType { get; set; }
    }
}
