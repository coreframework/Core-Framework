// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlResourcesInstaller.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.Core;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// Registers bootstrapper task for initialization yaml resources cache.
    /// </summary>
    public class YamlResourcesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IBootstrapperTask>().ImplementedBy<InitializeYamlResourceCache>());
        }
    }
}