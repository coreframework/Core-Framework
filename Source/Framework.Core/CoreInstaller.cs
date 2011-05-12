// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreInstaller.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Framework.Core.Configuration;
using Framework.Core.Helpers.Images;

namespace Framework.Core
{
    /// <summary>
    /// Registers standard framework components.
    /// </summary>
    public class CoreInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationManager>()
                                .ImplementedBy<ConfigurationManagerWrapper>()
                                .Named("configuration_manager"));

            container.Register(Component.For<IApplicationConfigurator>()
                                .ImplementedBy<YamlDatabaseConfigurator>()
                                .Named("database_configurator")
                                .Parameters(Parameter.ForKey("configPath").Eq("Config/database.yml")));

            container.Register(Component.For<IImageUtility>()
                                .ImplementedBy<DefaultImageUtility>()
                                .Named("image_utility"));
        }

        #endregion
    }
}