using System.Collections.Generic;
using System.Linq;
using Core.Framework.MEF.Web;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Provides methods for working with registered plugins
    /// </summary>
    public class PluginHelper : IPluginHelper
    {
        /// <summary>
        /// Determines whether plugin with the specified identifier is founded and installed.
        /// </summary>
        /// <param name="pluginIdentifier">The plugin identifier.</param>
        /// <returns>
        /// 	<c>true</c> if [is plugin enabled] [the specified plugin identified]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPluginEnabled(string pluginIdentifier)
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();
            Plugin plugin = pluginService.FindPluginByIdentifier(pluginIdentifier);

            return plugin != null && plugin.Status.Equals(PluginStatus.Installed);
        }

        /// <summary>
        /// Gets the available registered plugins.
        /// </summary>
        /// <returns>List of registered plugins.</returns>
        public static IEnumerable<PluginListModel> GetAvailablePlugins()
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();

            IEnumerable<Plugin> plugins = pluginService.GetAll();

            var registeredPlugins = MvcApplication.Plugins;

            return (from plugin in plugins
                    where registeredPlugins.FirstOrDefault(pl => pl.Identifier == plugin.Identifier) != null
                    select new PluginListModel
                               {
                                   Title = registeredPlugins.FirstOrDefault(pl => pl.Identifier == plugin.Identifier).Title,
                                   Description = registeredPlugins.FirstOrDefault(pl => pl.Identifier == plugin.Identifier).Description
                               }.MapFrom(plugin)).ToList();
        }
    }
}