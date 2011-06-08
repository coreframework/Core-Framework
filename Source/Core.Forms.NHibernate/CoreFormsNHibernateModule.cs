using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.Forms.NHibernate
{
    public class CoreFormsNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("forms_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<IFormService>().ImplementedBy<NHibernateFormService>().LifeStyle.Transient);
        }
    }
}
