// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.WindsorAdapter;

using Framework.Core.Configuration;
using Framework.Core.Helpers;

using Environment = Framework.Core.Configuration.Environment;

namespace Framework.Core
{
    /// <summary>
    /// Default <see cref="IApplication"/> implementation.
    /// </summary>
    public class Application : IApplication
    {
        #region Fields

        private readonly Dictionary<String, DatabaseConfiguration> databaseConfiguration = new Dictionary<String, DatabaseConfiguration>();

        private String rootPath;

        #endregion

        #region IApplication members

        /// <summary>
        /// Gets the application environment.
        /// </summary>
        /// <value>The application environment.</value>
        public Environment Environment { get; private set; }

        /// <summary>
        /// Gets or sets application root path.
        /// </summary>
        /// <value>The application path.</value>
        public String RootPath
        {
            get
            {
                var path = rootPath;
                if (String.IsNullOrEmpty(path))
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                }
                return path;
            }
            set
            {
                rootPath = value;
            }
        }

        /// <summary>
        /// Gets the database configuration.
        /// </summary>
        /// <value>The database configuration.</value>
        public Dictionary<String, DatabaseConfiguration> DatabaseConfiguration
        {
            get
            {
                return databaseConfiguration;
            }
        }

        /// <summary>
        /// Configures application instance.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Configure(IWindsorContainer container)
        {
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            container.Kernel.AddComponentInstance<IApplication>(this);

            var environmentSetting = container.Resolve<IConfigurationManager>().AppSettings[Constants.Environment];
            if (!String.IsNullOrEmpty(environmentSetting))
            {
                Environment = EnumHelper.Parse(environmentSetting, Environment.Development);
            }
            else
            {
                Environment = Environment.Development;
            }

            foreach (var configurator in container.ResolveAll<IApplicationConfigurator>())
            {
                configurator.Configure(this, container.Kernel);
            }
        }

        #endregion
    }
}