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

        private String pluginDirectory = string.Empty;

        #endregion

        #region Singleton

        private static NavigationPlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NavigationPlugin());
                }
            }
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

        public override string PluginConfigPath
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