// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateConfigurator.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

using Castle.Core.Configuration;
using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using Framework.Core;
using Framework.Core.Configuration;
using Framework.Core.Helpers;
using Framework.Facilities.NHibernate.Filters;
using Configuration = NHibernate.Cfg.Configuration;

namespace Framework.Facilities.NHibernate.Castle
{
    /// <summary>
    /// Builds nhibernate configuration.
    /// </summary>
    public class NHibernateConfigurator : IConfigurationBuilder
    {
        #region Fields

        private readonly IApplication application;

        private readonly IKernel kernel;

        private readonly Regex nhibernatePropertyPattern = new Regex("^hibernate\\.", RegexOptions.IgnoreCase);

        private BinaryFormatter binaryFormatter; 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateConfigurator"/> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="kernel">The kernel.</param>
        public NHibernateConfigurator(IApplication application, IKernel kernel)
        {
            this.application = application;
            this.kernel = kernel;
            this.binaryFormatter = new BinaryFormatter(); 
        }

        #endregion

        #region IConfigurationBuilder members

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>nhibernate configuration.</returns>
        public Configuration GetConfiguration(IConfiguration config)
        {
            var environment = EnumHelper.GetKey(application.Environment);
            var alias = config.Attributes["alias"];

            if (!String.IsNullOrEmpty(alias))
            {
                var environmentSpecific = String.Format("{0}-{1}", environment, alias);
                if (application.DatabaseConfiguration.ContainsKey(environmentSpecific))
                {
                    return BuildConfig(application.DatabaseConfiguration[environmentSpecific], config);
                }

                if (application.DatabaseConfiguration.ContainsKey(alias))
                {
                    return BuildConfig(application.DatabaseConfiguration[alias], config);
                }
            }
            else
            {
                if (application.DatabaseConfiguration.ContainsKey(environment))
                {
                    return BuildConfig(application.DatabaseConfiguration[environment], config);
                }
            }

            throw new ConfigurationErrorsException(String.Format("Could not build configuration for database (environment = \"{0}\", alias = \"{1}\").", environment, alias));
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Determines whether [is new configuration required] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if [is new configuration required] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsNewConfigurationRequired(string fileName)
        {
            return !File.Exists(fileName);
        }

        /// <summary>
        /// Writes the <see cref="Configuration"/> to stream.
        /// </summary>
        /// <param name="stream">The stream to be written.</param>
        /// <param name="cfg">The configuration.</param>
        protected virtual void WriteConfigurationToStream(Stream stream, Configuration cfg)
        {
            binaryFormatter.Serialize(stream, cfg);
        }

        /// <summary>
        /// Gets the <see cref="Configuration"/> from stream.
        /// </summary>
        /// <param name="fs">The stream from which the configuration will be deserialized.</param>
        /// <returns>The <see cref="Configuration"/>.</returns>
        protected virtual Configuration GetConfigurationFromStream(Stream fs)
        {
            return binaryFormatter.Deserialize(fs) as Configuration;
        } 

        private static IPersistenceConfigurer GetDatabase(DatabaseConfiguration databaseConfiguration)
        {
            switch (databaseConfiguration.Platform)
            {
                case DatabasePlatform.SqlServer:
                    return MsSqlConfiguration.MsSql2008.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.SqlServer2000:
                    return MsSqlConfiguration.MsSql2000.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.SqlServer2005:
                    return MsSqlConfiguration.MsSql2005.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.SQLite:
                    return SQLiteConfiguration.Standard.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.MySQL:
                    return MySQLConfiguration.Standard.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.PostgreSQL:
                    return PostgreSQLConfiguration.Standard.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.Oracle9:
                    return OracleClientConfiguration.Oracle9.ConnectionString(databaseConfiguration.GetConnectionString());
                case DatabasePlatform.Oracle10:
                    return OracleClientConfiguration.Oracle9.ConnectionString(databaseConfiguration.GetConnectionString());
                default:
                    throw new ConfigurationErrorsException(String.Format("{0} platform is not supported by nhibernate facility.", databaseConfiguration.Platform));
            }
        }

        private Configuration BuildConfig(DatabaseConfiguration databaseConfiguration, IConfiguration config)
        {
            string fileName = config.Attributes["fileName"];

            Configuration cfg;
            if (IsNewConfigurationRequired(fileName))
            {
                using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    var fluenty = Fluently.Configure()
                        .Database(GetDatabase(databaseConfiguration))
                        .Mappings(m =>
                        {
                            foreach (var mapper in kernel.ResolveAll<INHibernateMapper>())
                            {
                                mapper.Map(m, databaseConfiguration);
                            }
                            m.FluentMappings.Add(typeof(CultureFilter));
                        });

                    cfg = fluenty.ExposeConfiguration(ProcessConfiguration).BuildConfiguration().AddProperties(GetNHibernateProperties(databaseConfiguration));
                    WriteConfigurationToStream(fileStream, cfg);
                }
            }
            else
            {
                using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    cfg = GetConfigurationFromStream(fileStream);
                }
            }
            return cfg;
        }

        private void ProcessConfiguration(Configuration configuration)
        {
            foreach (var configurationChain in kernel.ResolveAll<INHibernateConfigurationChain>())
            {
                configurationChain.Process(configuration);
            }
        }

        private Dictionary<String, String> GetNHibernateProperties(DatabaseConfiguration databaseConfiguration)
        {
            var nhibernateProperties = new Dictionary<String, String>();
            foreach (var property in databaseConfiguration.Properties)
            {
                if (nhibernatePropertyPattern.IsMatch(property.Key))
                {
                    var key = nhibernatePropertyPattern.Replace(property.Key, String.Empty);
                    nhibernateProperties[key] = property.Value;
                }
            }
            return nhibernateProperties;
        }

        #endregion
    }
}