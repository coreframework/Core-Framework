using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.ContentPages.NHibernate
{
    public class CoreContentPagesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("cpages_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<IContentPageService>().ImplementedBy<NHibernateContentPageService>().LifeStyle.Transient);
            container.Register(Component.For<IContentPageLocaleService>().ImplementedBy<NHibernateContentPageLocaleService>().LifeStyle.Transient);

            //Register widget data services. 
            container.Register(Component.For<IContentPageWidgetService>().ImplementedBy<NHibernateContentPageWidgetService>().LifeStyle.Transient);
        }
    }
}
