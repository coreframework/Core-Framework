using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Plugins.Helpers;
using Core.Web.Helpers;

namespace Core.Web
{
    public class CoreWebInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPermissionsHelper>().ImplementedBy<ResourcePermissionsHelper>());
            container.Register(Component.For<IPluginHelper>().ImplementedBy<PluginHelper>().LifeStyle.Transient);
            container.Register(Component.For<IWidgetHelper>().ImplementedBy<WidgetHelper>().LifeStyle.Transient);
            container.Register(Component.For<ICoreWidgetInstanceBuilder>().ImplementedBy<CoreWidgetInstanceBuilder>().LifeStyle.Transient);
        }
    }
}