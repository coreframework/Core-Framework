using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Framework.Facilities.NHibernate.Castle;
using Products.NHibernate.Contracts;
using Products.NHibernate.Services;

namespace Products.NHibernate
{
    public class ProductsNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("products_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<IProductService>().ImplementedBy<NHibernateProductService>().LifeStyle.Transient);
            container.Register(Component.For<IProductLocaleService>().ImplementedBy<NHibernateProductLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<IProductWidgetService>().ImplementedBy<NHibernateProductWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<ICategoryService>().ImplementedBy<NHibernateCategoryService>().LifeStyle.Transient);
            container.Register(Component.For<ICategoryLocaleService>().ImplementedBy<NHibernateCategoryLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<ICategoryWidgetService>().ImplementedBy<NHibernateCategoryWidgetService>().LifeStyle.Transient);
        }
    }
}
