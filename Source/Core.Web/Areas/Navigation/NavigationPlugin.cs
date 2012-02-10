using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Web;
using Castle.Windsor;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation
{
    [Export(typeof(ICorePlugin))]
    public class NavigationPlugin : BasePlugin
    {
        #region Constants

        private const String NavigationConfig = @"Config\PluginConfig.xml";

        private const String NavigationVirtualPath = "~/Areas/Navigation/";

        #endregion

        #region Fields

        private String pluginDirectory = String.Empty;

        #endregion

        #region Singleton

        private static readonly Lazy<NavigationPlugin> instance = new Lazy<NavigationPlugin>(() => new NavigationPlugin());

        public static NavigationPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NavigationPlugin()
        {
        }

        #endregion

        //TODO Remove virtual for this property from BASE class, when plugin move out from web project !!!
        public override String PluginDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(pluginDirectory) && HttpContext.Current != null)
                {
                    pluginDirectory = HttpContext.Current.Server.MapPath(NavigationVirtualPath);
                }
                return pluginDirectory;
            }
        }

        public override String PluginConfigPath
        {
            get
            {
                return NavigationConfig;
            }
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
           
        }

        public override void Install()
        {

        }

        public override void Uninstall()
        {

        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.Navigation.Migrations");
        }
    }
}