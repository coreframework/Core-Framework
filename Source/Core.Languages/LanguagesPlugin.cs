using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages
{
    [Export(typeof(ICorePlugin))]
    [Export(typeof(IPermissible))]
    public class LanguagesPlugin : BasePlugin, IPermissible
    {
        #region Singleton

        private static LanguagesPlugin _instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguagesPlugin Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new LanguagesPlugin());
                }
            }
        }

        #endregion

        private LanguagesPlugin()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<LanguagesPluginOperations>();
        }

        public override string Identifier
        {
            get { return GetPluginIdentifier(); }
        }

        public override string Title
        {
            get
            {
                return "Languages";
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
                return "Allow managing Languages";
            }
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
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
            Language currentLanguage = new Language
                                           {
                                               Title = currentCultureInfo.NativeName,
                                               Code = currentCultureInfo.TwoLetterISOLanguageName,
                                               Culture = currentCultureInfo.Name
                                           };
            ServiceLocator.Current.GetInstance<ILanguageService>().Save(currentLanguage);
        }

        public override void Uninstall()
        {
           
        }

        public override Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.Languages.Migrations");
        }

        public static string GetPluginIdentifier()
        {
            return "1235"; 
        }

        #region IPermissible members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}