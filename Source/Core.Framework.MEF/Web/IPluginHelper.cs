using System;
using System.Collections.Generic;
using Core.Framework.Plugins.Web;

namespace Core.Framework.MEF.Web
{
    public interface IPluginHelper
    {
        bool IsPluginEnabled(String pluginIdentified);

        IEnumerable<ICorePlugin> GetInstalledPlugins();
    }
}
