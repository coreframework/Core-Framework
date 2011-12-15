using System;
using System.Collections.Generic;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.Framework.MEF.Web
{
    public interface IPluginHelper
    {
        /// <summary>
        /// Determines whether [is plugin enabled] [the specified plugin identified].
        /// </summary>
        /// <param name="pluginIdentified">The plugin identified.</param>
        /// <returns>
        /// 	<c>true</c> if [is plugin enabled] [the specified plugin identified]; otherwise, <c>false</c>.
        /// </returns>
        bool IsPluginEnabled(String pluginIdentified);

        /// <summary>
        /// Gets the installed plugins.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICorePlugin> GetInstalledPlugins();

        /// <summary>
        /// Gets the missing plugins.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns></returns>
        IList<PluginDependency> GetMissingPlugins(ICorePlugin plugin);
    }
}
