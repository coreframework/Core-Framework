// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateConfigurator.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

using Castle.Core.Configuration;
using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using Framework.Core;
using Framework.Core.Configuration;
using Framework.Core.Helpers;

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
                    return BuildConfig(application.DatabaseConfiguration[environmentSpecific]);
                }

                if (application.DatabaseConfiguration.ContainsKey(alias))
                {
                    return BuildConfig(application.DatabaseConfiguration[alias]);
                }
            }
            else
            {
                if (application.DatabaseConfiguration.ContainsKey(environment))
                {
                    return BuildConfig(application.DatabaseConfiguration[environment]);
                }
            }

            throw new ConfigurationErrorsException(String.Format("Could not build configuration for database (environment = \"{0}\", alias = \"{1}\").", environment, alias));
        }

        #endregion

        #region Helper methods

        private Configuration BuildConfig(DatabaseConfiguration databaseConfiguration)
        {
            var fluenty = Fluently.Configure()
                            .Database(GetDatabase(databaseConfiguration))
                            .Mappings(m =>
                                        {
                                            foreach (var mapper in kernel.ResolveAll<INHibernateMapper>())
                                            {
                                                mapper.Map(m, databaseConfiguration);
                                            }
                                        });

            return fluenty.ExposeConfiguration(ProcessConfiguration).BuildConfiguration().AddProperties(GetNHibernateProperties(databaseConfiguration));
        }

        private void ProcessConfiguration(Configuration configuration)
        {
            foreach (var configurationChain in kernel.ResolveAll<INHibernateConfigurationChain>())
            {
                configurationChain.Process(configuration);
            }
        }

        private IPersistenceConfigurer GetDatabase(DatabaseConfiguration databaseConfiguration)
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