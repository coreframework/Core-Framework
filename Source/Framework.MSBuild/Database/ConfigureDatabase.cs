// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureDatabase.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

using Castle.Windsor;

using Framework.Core;
using Framework.Core.Configuration;
using Framework.Migrator;

namespace Framework.MSBuild.Database
{
    /// <summary>
    /// Reads database configuration from config specified.
    /// </summary>
    public class ConfigureDatabase : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets database config path.
        /// </summary>
        /// <value>The config path.</value>
        [Required]
        public String ConfigPath { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public String Environment { get; set; }

        /// <summary>
        /// Gets or sets the application root.
        /// </summary>
        /// <value>The application root.</value>
        public String ApplicationRoot { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        [Output]
        public String ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the dialect.
        /// </summary>
        /// <value>The dialect.</value>
        [Output]
        public String Dialect { get; set; }

        #endregion

        #region Task members

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            bool result = true;

            var application = ConfigureApplication();
            var configKey = GetConfigKey();

            if (application.DatabaseConfiguration.ContainsKey(configKey))
            {
                var databaseConfiguration = application.DatabaseConfiguration[configKey];
                ConnectionString = databaseConfiguration.GetConnectionString();
                Dialect = MigratorUtility.GetDialect(databaseConfiguration.Platform);
            }
            else
            {
                Log.LogError("Could not find database configuration for Environment = \"{0}\".", Environment);
                result = false;
            }

            return result;
        }

        #endregion

        #region Helper members

        private String GetConfigKey()
        {
            var configKey = Environment;
            if (String.IsNullOrEmpty(configKey))
            {
                configKey = Core.Configuration.Environment.Development.ToString();
            }
            configKey = configKey.ToLowerInvariant();
            return configKey;
        }

        private Application ConfigureApplication()
        {
            var container = new WindsorContainer();

            var application = new Application { RootPath = ApplicationRoot };

            var databaseConfigurator = new YamlDatabaseConfigurator(ConfigPath);
            databaseConfigurator.Configure(application, container.Kernel);

            return application;
        }

        #endregion
    }
}