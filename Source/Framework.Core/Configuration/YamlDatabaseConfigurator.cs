// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlDatabaseConfigurator.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.IO;
using Castle.MicroKernel;

using Framework.Core.Helpers;

using String = System.String;
using YamlDocument = Framework.Core.Helpers.Yaml.YamlDocument;

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Reads database configuration from yaml file.
    /// </summary>
    public class YamlDatabaseConfigurator : IApplicationConfigurator
    {
        #region Fields

        private readonly String configPath;
        private DatabaseConfiguration databaseConfiguration;

        #endregion

        #region Properties

        /// <summary>
        /// Current database configuration.
        /// </summary>
        public DatabaseConfiguration DatabaseConfiguration
        {
            get
            {
                return databaseConfiguration;
            }
        }

        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlDatabaseConfigurator"/> class.
        /// </summary>
        /// <param name="configPath">The config path.</param>
        public YamlDatabaseConfigurator(String configPath)
        {
            this.configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
        }

        #endregion

        #region IDatabaseConfigurator members

        /// <summary>
        /// Fills specified database configuration.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="kernel">The kernel.</param>
        public void Configure(IApplication application, IKernel kernel)
        {
            dynamic databases = YamlDocument.FromFile(configPath);
            String currentEnvironment = ConfigurationManager.AppSettings[Constants.Environment];
            foreach (var database in databases)
            {
                application.DatabaseConfiguration[database.Key] = ParseConfiguration(database.Value, application);
                if (currentEnvironment!=null && currentEnvironment.Equals(database.Key))
                {
                    databaseConfiguration = application.DatabaseConfiguration[database.Key];
                }
            }
        }

        #endregion

        #region Helper methods

        private DatabaseConfiguration ParseConfiguration(dynamic config, IApplication application)
        {
            var database = new DatabaseConfiguration();
            database.Platform = EnumHelper.Parse(config.Platform, DatabasePlatform.SqlServer);
            database.Host = config.Host ?? String.Empty;
            database.Database = config.Database ?? String.Empty;
            database.Username = config.Username ?? String.Empty;
            database.Password = config.Password ?? String.Empty;

            if (config.Properties != null)
            {
                foreach (var property in config.Properties)
                {
                    if (property.Value is String)
                    {
                        database.Properties[property.Key] = property.Value;
                    }
                }
            }

            if (DatabasePlatform.SQLite.Equals(database.Platform) && !DatabaseConfiguration.InMemoryDatabase.Equals(database.Database))
            {
                database.Database = Path.Combine(application.RootPath, database.Database);
            }

            return database;
        }

        #endregion
    }
}