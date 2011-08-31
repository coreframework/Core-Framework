using System;

namespace Core.Framework.Plugins.Handlers
{
    public interface IPluginHttpHandler
    {
        String Verb { get; set; }

        String Path { get; set; }

        String HandlerType { get; set; }
    }
}
