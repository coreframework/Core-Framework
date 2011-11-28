using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.WebContent.NHibernate
{
    public static class CoreWebContentNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("wc_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<ISectionService>().ImplementedBy<NHibernateSectionService>().LifeStyle.Transient);
            container.Register(Component.For<ISectionLocaleService>().ImplementedBy<NHibernateSectionLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<ISectionSettingsService>().ImplementedBy<NHibernateSectionSettingsService>().LifeStyle.Transient);

            container.Register(Component.For<ICategoryService>().ImplementedBy<NHibernateCategoryService>().LifeStyle.Transient);
            container.Register(Component.For<ICategoryLocaleService>().ImplementedBy<NHibernateCategoryLocaleService>().LifeStyle.Transient);

            container.Register(Component.For<IArticleService>().ImplementedBy<NHibernateArticleService>().LifeStyle.Transient);
            container.Register(Component.For<IArticleLocaleService>().ImplementedBy<NHibernateArticleLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<IArticleFileService>().ImplementedBy<NHibernateArticleFileService>().LifeStyle.Transient);

            // Widgets
            container.Register(Component.For<IWebContentWidgetService>().ImplementedBy<NHibernateWebContentWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IWebContentWidgetCategoryService>().ImplementedBy<NHibernateWebContentWidgetCategoryService>().LifeStyle.Transient);
            container.Register(Component.For<IWebContentDetailsWidgetService>().ImplementedBy<NHibernateWebContentDetailsWidgetService>().LifeStyle.Transient);
        }
    }
}
