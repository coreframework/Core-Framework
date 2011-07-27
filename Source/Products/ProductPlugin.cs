using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Products.Permissions.Operations;

namespace Products
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class ProductPlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String ProductConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static ProductPlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ProductPlugin());
                }
            }
        }

        #endregion

        private ProductPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<ProductsPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            NHibernate.ProductsNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Products.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override string PluginConfigPath
        {
            get { return ProductConfig; }
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
