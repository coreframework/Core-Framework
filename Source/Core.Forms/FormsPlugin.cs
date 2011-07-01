using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Web;
using Castle.Windsor;
using Core.Forms.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Framework.MVC.Helpers;

namespace Core.Forms
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class FormsPlugin: BasePlugin, IPermissible
    {
        #region Constants

        private const String FormsConfig = @"Config\PluginConfig.xml";

        #endregion
        
        #region Singleton

        private static FormsPlugin instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new FormsPlugin());
                }
            }
        }

        #endregion

        private FormsPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<FormsPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            NHibernate.CoreFormsNHibernateModule.Install(container);
        }

        public override void Install()
        {
            AssetsHelper.BuildPluginCssPack(this, HttpContext.Current.Request.PhysicalApplicationPath);
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
             return Assembly.Load("Core.Forms.Migrations");
        }

        /// <summary>
        /// Gets the Identifiers config path. Default String.Empty.
        /// [Example: @"Config\asset_packages.yml"]
        /// </summary>
        public override string PluginConfigPath
        {
            get { return FormsConfig; }
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}