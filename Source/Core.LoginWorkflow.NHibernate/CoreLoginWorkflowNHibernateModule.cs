using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.LoginWorkflow.NHibernate.Contracts;
using Core.LoginWorkflow.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.LoginWorkflow.NHibernate
{
    public class CoreLoginWorkflowNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("loginworkflow_mapper").LifeStyle.Singleton);

            // Register data services.
            container.Register(Component.For<ILoginHolderWidgetService>().ImplementedBy<NHibernateLoginHolderWidgetService>().LifeStyle.Singleton);
        }
    }
}
