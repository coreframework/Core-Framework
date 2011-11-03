using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.WebContent.NHibernate;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.WebContent.Permissions.Operations;

namespace Core.WebContent
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class WebContentPlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String WebContentConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static WebContentPlugin instance;

        private static readonly Object SyncRoot = new Object();

        public static WebContentPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new WebContentPlugin());
                }
            }
        }

        #endregion

        private WebContentPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<WebContentPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            CoreWebContentNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.WebContent.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override String  PluginConfigPath
        {
            get { return WebContentConfig; }
        }

        #region IPermissible members

        public String  PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
