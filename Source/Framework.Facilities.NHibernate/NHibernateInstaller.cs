// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateInstaller.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.Facilities.AutoTx;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Services.Transaction;
using Castle.Windsor;
using FluentNHibernate.Conventions;
using Framework.Core;
using Framework.Facilities.NHibernate.Castle;

namespace Framework.Facilities.NHibernate
{
    /// <summary>
    /// Registers nhibernate facility components.
    /// </summary>
    public class NHibernateInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller members

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyContaining<NHibernateInstaller>().BasedOn(typeof(IConvention)));
            container.Register(Component.For<IBootstrapperTask>().ImplementedBy<ConfigureNHibernateFacility>());
            container.AddFacility<TransactionFacility>();
            container.Register(Component.For<ITransactionManager>().ImplementedBy<DefaultTransactionManager>());
        }

        #endregion
    }
}