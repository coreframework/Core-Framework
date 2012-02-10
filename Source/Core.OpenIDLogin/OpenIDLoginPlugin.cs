using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.OpenIDLogin.NHibernate;

namespace Core.OpenIDLogin
{
    [Export(typeof(ICorePlugin))]
    public class OpenIDLoginPlugin : BasePlugin
    {
        #region Constants

        private const String ProfilesConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static readonly Lazy<OpenIDLoginPlugin> instance = new Lazy<OpenIDLoginPlugin>(() => new OpenIDLoginPlugin());

        public static OpenIDLoginPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        #endregion

        private OpenIDLoginPlugin()
        {
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            CoreOpenIDLoginNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.OpenIDLogin.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override String PluginConfigPath
        {
            get { return ProfilesConfig; }
        }
    }
}
