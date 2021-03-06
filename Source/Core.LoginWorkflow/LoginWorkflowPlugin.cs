﻿using System;
using System.ComponentModel.Composition;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.LoginWorkflow.NHibernate;

namespace Core.LoginWorkflow
{
    [Export(typeof(ICorePlugin))]
    public class LoginWorkflowPlugin : BasePlugin
    {
        #region Constants

        private const String ProfilesConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static readonly Lazy<LoginWorkflowPlugin> instance = new Lazy<LoginWorkflowPlugin>(() => new LoginWorkflowPlugin());

        public static LoginWorkflowPlugin Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginWorkflowPlugin()
        {
        }

        #endregion        

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            CoreLoginWorkflowNHibernateModule.Install(container);
        }

        public override void Install()
        {
            
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.LoginWorkflow.Migrations");
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
