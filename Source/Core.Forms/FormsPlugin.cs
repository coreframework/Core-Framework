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

        private const String CssPackPlugin = "base1";

        private const String Config = @"Config\asset_packages.yml";

        private const String ImagesPlugin = @"Content\";

        private const String CssPlugin = @"Content\Css\";

        #endregion
        
        #region Singleton

        private static FormsPlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new FormsPlugin());
                }
            }
        }

        #endregion

        private FormsPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<FormsPluginOperations>();
        }

        public override string Identifier
        {
            get { return GetPluginIdentifier(); }
        }

        public override string Title
        {
            get
            {
                return "Forms";
            }
        }

        public override string ResourcesDirectory
        {
            get
            {
                return "Resources";
            }
        }

        public override string Description
        {
            get 
            { 
                return "Allow managing Web Forms";
            }
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
        /// Gets the config path. Default String.Empty.
        /// [Example: @"Config\asset_packages.yml"]
        /// </summary>
        public override string ConfigPath
        {
            get { return Config; }
        }

        /// <summary>
        /// Gets the images path. Default String.Empty.
        /// [Example: @"Content/"] "Content" - folder that contains images folder.
        /// </summary>
        public override string ImagesPath
        {
            get { return ImagesPlugin; }
        }

        /// <summary>
        /// Gets the CSS path. Default String.Empty.
        /// [Example: @"Content\Css\"]
        /// </summary>
        public override string CssPath
        {
            get { return CssPlugin; }
        }

        /// <summary>
        /// Gets the CSS pack which placed in the config. Default String.Empty.
        /// [Example: "base1"]
        /// </summary>
        public override string CssPack
        {
            get { return CssPackPlugin; }
        }


        public static string GetPluginIdentifier()
        {
            return "123"; 
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}