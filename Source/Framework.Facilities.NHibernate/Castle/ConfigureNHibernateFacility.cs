// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureNHibernateFacility.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;

using Castle.Facilities.NHibernateIntegration;
using Castle.MicroKernel;
using Castle.Core.Configuration;

using Framework.Core;
using Framework.Core.Helpers;

using Environment = Framework.Core.Configuration.Environment;

namespace Framework.Facilities.NHibernate.Castle
{
    /// <summary>
    /// Configures and registers castle nhibernate facility.
    /// </summary>
    public class ConfigureNHibernateFacility : IBootstrapperTask
    {
        private readonly Regex complexConfigurationPattern = new Regex(@"(\w+)-(.*)");

        private const String EnvironmentSpecificTemplate = "{0}-{1}";

        #region IBootstrapperTask members

        /// <summary>
        /// Executes task.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="kernel">IoC container.</param>
        public void Execute(IApplication application, IKernel kernel)
        {
            var facilityConfig = GetFacilityConfig(application);

            kernel.ConfigurationStore.AddFacilityConfiguration("nhibernate.facility", facilityConfig);
            kernel.AddFacility("nhibernate.facility", new NHibernateFacility(new NHibernateConfigurator(application, kernel)));
        }

        #endregion

        #region Helper methods

        private MutableConfiguration GetFacilityConfig(IApplication application)
        {
            var currentEnvironment = EnumHelper.GetKey(application.Environment);
            var allEnvironments = EnumHelper.GetKeys<Environment>();

            var facilityConfig = new MutableConfiguration("facility");
            if (HttpContext.Current != null)
            {
                facilityConfig.Attribute("isWeb", bool.TrueString);
            }

            foreach (var config in application.DatabaseConfiguration)
            {
                if (complexConfigurationPattern.IsMatch(config.Key))
                {
                    var match = complexConfigurationPattern.Match(config.Key);
                    var environment = match.Groups[1].Value;
                    var alias = match.Groups[2].Value;

                    if (currentEnvironment.Equals(environment))
                    {
                        AddFactory(facilityConfig, alias);
                    }
                }
                else
                {
                    if (currentEnvironment.Equals(config.Key))
                    {
                        AddDefaultFactory(facilityConfig);
                    }
                    else
                    {
                        if (!Enumerable.Contains(allEnvironments, config.Key))
                        {
                            var alias = config.Key;
                            var environmentSpecific = String.Format(EnvironmentSpecificTemplate, currentEnvironment, alias);
                            if (!application.DatabaseConfiguration.ContainsKey(environmentSpecific))
                            {
                                AddFactory(facilityConfig, alias);
                            }
                        }
                    }
                }
            }

            return facilityConfig;
        }

        private void AddDefaultFactory(MutableConfiguration config)
        {
            config.CreateChild("factory").Attribute("id", "nhibernate-factory.default");
        }

        private void AddFactory(MutableConfiguration config, String alias)
        {
            config.CreateChild("factory").Attribute("id", String.Format("nhibernate-factory.{0}", alias)).Attribute("alias", alias);
        }

        #endregion
    }
}