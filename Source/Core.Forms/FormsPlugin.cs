using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Forms.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.Forms
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class FormsPlugin: BasePlugin, IPermissible
    {
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
                return null;
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
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
             return Assembly.Load("Core.Forms.Migrations");
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