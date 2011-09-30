using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.News.Nhibernate.Contracts;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Services;
using Core.News.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.News.Nhibernate
{
    public static class CoreNewsArticlesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            //Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("cnews_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<INewsArticleService>().ImplementedBy<NHibernateNewsArticleService>().LifeStyle.Transient);
            container.Register(Component.For<INewsArticleLocaleService>().ImplementedBy<NHibernateNewsArticleLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<INewsCategoryService>().ImplementedBy<NHibernateNewsCategoryService>().LifeStyle.Transient);
            container.Register(Component.For<INewsCategoryLocaleService>().ImplementedBy<NHibernateNewsCategoryLocaleService>().LifeStyle.Transient);

            //Register widget data services. 
            container.Register(Component.For<INewsArticleWidgetService>().ImplementedBy<NHibernateNewsArticleWidgetService>().LifeStyle.Transient);
        }
    }
}
