using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.Profiles.NHibernate;
using Core.Profiles.Permissions.Operations;

namespace Core.Profiles
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class ProfilesPlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String ProfilesConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static readonly Lazy<ProfilesPlugin> instance = new Lazy<ProfilesPlugin>(() => new ProfilesPlugin());

        public static ProfilesPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        #endregion

        private ProfilesPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<ProfilesPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            CoreProfilesNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.Profiles.Migrations");
        }


        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public override String PluginConfigPath
        {
            get { return ProfilesConfig; }
        }

        #region IPermissible members

        public String  PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
