using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.ContentPages.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class ContentPagePlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String ContentPageConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static ContentPagePlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentPagePlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ContentPagePlugin());
                }
            }
        }

        #endregion

        private ContentPagePlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<ContentPagePluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            NHibernate.CoreContentPagesNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.ContentPages.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override string PluginConfigPath
        {
            get { return ContentPageConfig; }
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
