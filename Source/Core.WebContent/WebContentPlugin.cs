using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.WebContent.NHibernate;

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

        private static readonly Lazy<WebContentPlugin> instance = new Lazy<WebContentPlugin>(() => new WebContentPlugin());

        public static WebContentPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentPlugin()
        {
        }

        #endregion

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
