using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation
{
    [Export(typeof(ICorePlugin))]
    public class NavigationPlugin : BasePlugin
    {
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

        public override string Identifier
        {
            get { return GetPluginIdentifier(); }
        }

        public override string Title
        {
            get
            {
                return "Navigation";
            }
        }

        public override string Description
        {
            get
            {
                return "Navigation";
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

        //TODO: kill it!!!
        public static string GetPluginIdentifier()
        {
            return "5224B396-44E1-11E0-B8AF-801ADFD72185";
        }
    }
}