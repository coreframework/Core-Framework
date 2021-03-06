﻿using System;
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
using Framework.Mvc.Helpers;

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

        private static readonly Lazy<FormsPlugin> instance = new Lazy<FormsPlugin>(() => new FormsPlugin());

        public static FormsPlugin Instance
        {
            get
            {
                return instance.Value;
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
        public override String PluginConfigPath
        {
            get { return FormsConfig; }
        }

        #region IPermissible members

        public String PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}