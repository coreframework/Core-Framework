using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Web;
using Castle.Windsor;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.Languages.Modules;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Permissions.Operations;
using Framework.Core.Localization;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class LanguagesPlugin : BasePlugin, IPermissible
    {
        #region Constants

        private const String LanguagesConfig = @"Config\PluginConfig.xml";

        #endregion

        #region Singleton

        private static LanguagesPlugin instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguagesPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new LanguagesPlugin());
                }
            }
        }

        #endregion

        private LanguagesPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<LanguagesPluginOperations>();
        }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Register(IWindsorContainer container)
        {
            NHibernate.CoreLanguagesNHibernateModule.Install(container);
        }

        public override void Install()
        {
            AssetsHelper.BuildPluginCssPack(this, HttpContext.Current.Request.PhysicalApplicationPath);
//            Language currentLanguage = new Language
//                                           {
//                                               Title = CultureHelper.NeutralCultureName,
//                                               Code = null,
//                                               Culture = null,
//                                               IsDefault = true
//                                           };
//            ServiceLocator.Current.GetInstance<ILanguageService>().Save(currentLanguage);
        }

        public override void Uninstall()
        {
            Application.UnregisterHttpModule(typeof(LocalizationModule));
        }

        public override void Start()
        {
            Application.RegisterHttpModule(typeof(LocalizationModule));
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.Languages.Migrations");
        }

        /// <summary>
        /// Gets the Identifiers config path. Default String.Empty.
        /// [Example: @"Config\asset_packages.yml"]
        /// </summary>
        public override string PluginConfigPath
        {
            get { return LanguagesConfig; }
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
