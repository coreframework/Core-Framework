using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.News.Permissions.Operations;

namespace Core.News
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class NewsPlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String NewsConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static NewsPlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsPlugin());
                }
            }
        }

        #endregion

        private NewsPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<NewsPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            Nhibernate.CoreNewsArticlesNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.News.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override string PluginConfigPath
        {
            get { return NewsConfig; }
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
