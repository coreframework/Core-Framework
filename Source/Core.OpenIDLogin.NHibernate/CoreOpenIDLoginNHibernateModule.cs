using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.OpenIDLogin.NHibernate.Contracts;
using Core.OpenIDLogin.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.OpenIDLogin.NHibernate
{
    public class CoreOpenIDLoginNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("openidlogin_mapper").LifeStyle.Singleton);

            // Register data services.
            container.Register(Component.For<IOpenIDLoginWidgetService>().ImplementedBy<NHibernateOpenIDLoginWidgetService>().LifeStyle.Singleton);
        }
    }
}
