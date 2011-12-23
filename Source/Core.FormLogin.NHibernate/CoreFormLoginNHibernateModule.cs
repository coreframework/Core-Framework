using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.FormLogin.NHibernate.Contracts;
using Core.FormLogin.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.FormLogin.NHibernate
{
    public class CoreFormLoginNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("formlogin_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<IFormLoginWidgetService>().ImplementedBy<NHibernateFormLoginWidgetService>().LifeStyle.Transient);
        }
    }
}
