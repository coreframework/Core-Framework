using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

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
        public bool IsPluginEnabled(String pluginIdentifier)
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();
            Plugin plugin = pluginService.FindPluginByIdentifier(pluginIdentifier);

            return plugin != null && plugin.Status.Equals(PluginStatus.Installed);
        }

        public IEnumerable<ICorePlugin> GetInstalledPlugins()
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();

            IEnumerable<Plugin> plugins = pluginService.GetAll();

            var registeredPlugins = MvcApplication.Plugins;

            return (from plugin in registeredPlugins
                    where plugins.Any(pl => pl.Identifier == plugin.Identifier && pl.Status.Equals(PluginStatus.Installed))
                    select plugin).ToList();
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
                                   Title = plugin.Title,
                                   Description = plugin.Description
                               }.MapFrom(plugin)).ToList();
        }

        /// <summary>
        /// Gets the available plugins.
        /// </summary>
        /// <param name="pluginCriteria">The plugin criteria.</param>
        /// <returns></returns>
        public static IEnumerable<PluginLocale> GetAvailablePlugins(ICriteria pluginCriteria)
        {
            return (from plugin in pluginCriteria.List<PluginLocale>()
                    where Application.Plugins.FirstOrDefault(pl => pl.Identifier == plugin.Plugin.Identifier) != null
                    select plugin).ToList();
        }

        /// <summary>
        /// Counts the available plugins.
        /// </summary>
        /// <param name="pluginCriteria">The plugin criteria.</param>
        /// <returns></returns>
        public static int CountAvailablePlugins(ICriteria pluginCriteria)
        {
            return pluginCriteria.List<PluginLocale>().Where(plugin => Application.Plugins.FirstOrDefault(pl => pl.Identifier == plugin.Plugin.Identifier) != null).Count();
        }

        public IList<PluginDependency> GetMissingPlugins(ICorePlugin plugin)
        {
            IList<PluginDependency> missingDependencies = new List<PluginDependency>();
            var installedPlugins = GetInstalledPlugins();
            if (plugin.PluginSetting.PluginDependencies != null)
            {
                foreach (var pluginDependency in plugin.PluginSetting.PluginDependencies)
                {
                    if (
                        !installedPlugins.Where(
                            installedPlugin => installedPlugin.Identifier.Equals(pluginDependency.Identifier)
                                               &&
                                               IsAppropriateVersion(installedPlugin.Version, pluginDependency.MinVersion,
                                                                    pluginDependency.MaxVersion)).Any())
                    {
                        missingDependencies.Add(pluginDependency);
                    }

                }
            }

            return missingDependencies;
        }

        private bool IsAppropriateVersion(String version, String minVersion, String maxVersion)
        {
            //            ((String.IsNullOrEmpty(pluginDependency.MinVersion) && String.IsNullOrEmpty(pluginDependency.MaxVersion))
            //                    || (String.IsNullOrEmpty(installedPlugin.Version)))
            return false;
        }
    }
}