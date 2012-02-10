using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.FormLogin.NHibernate;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin
{
    [Export(typeof(ICorePlugin))]
    public class FormLoginPlugin : BasePlugin
    {
        #region Constants

        private const String ProfilesConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static readonly Lazy<FormLoginPlugin> instance = new Lazy<FormLoginPlugin>(() => new FormLoginPlugin());

        public static FormLoginPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FormLoginPlugin()
        {
        }

        #endregion

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            CoreFormLoginNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.FormLogin.Migrations");
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
