using System;

namespace Core.Framework.Plugins.Modules
{
    public interface IPluginHttpModule
    {
        void OnBeginRequest(object sender, EventArgs e);
    }
}
