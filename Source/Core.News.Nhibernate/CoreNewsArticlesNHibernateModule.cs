using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.News.Nhibernate
{
    public class CoreNewsArticlesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("cnews_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<INewsArticleService>().ImplementedBy<NHibernateNewsArticleService>().LifeStyle.Transient);
            container.Register(Component.For<INewsArticleLocaleService>().ImplementedBy<NHibernateNewsArticleLocaleService>().LifeStyle.Transient);

            //Register widget data services. 
            container.Register(Component.For<INewsArticleWidgetService>().ImplementedBy<NHibernateNewsArticleWidgetService>().LifeStyle.Transient);
        }
    }
}
