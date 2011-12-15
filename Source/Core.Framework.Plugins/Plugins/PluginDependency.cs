using System;

namespace Core.Framework.Plugins.Plugins
{
    public class PluginDependency
    {
        public String Identifier { get; set; }

        public String MinVersion { get; set; }

        public String MaxVersion { get; set; }
    }
}
