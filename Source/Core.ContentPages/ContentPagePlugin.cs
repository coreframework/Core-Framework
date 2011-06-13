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

        public override string Identifier
        {
            get { return GetPluginIdentifier(); }
        }

        public override string Title
        {
            get
            {
                return "Content Pages";
            }
        }

        public override string ResourcesDirectory
        {
            get
            {
                return "ContentPage/Resources";
            }
        }

        public override string Description
        {
            get 
            { 
                return "Allow managing Content Pages";
            }
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

        //TODO: kill it!!!
        public static string GetPluginIdentifier()
        {
            return "0"; 
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
