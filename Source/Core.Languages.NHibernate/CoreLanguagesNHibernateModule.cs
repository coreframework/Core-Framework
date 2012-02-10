using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Helpers;
using Core.Languages.NHibernate.Services;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Castle;

namespace Core.Languages.NHibernate
{
    public static class CoreLanguagesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("languages_mapper").LifeStyle.Singleton);

            // Register data services.
            container.Register(Component.For<ILanguageService>().ImplementedBy<NHibernateLanguageService>().LifeStyle.Singleton);

            //Register helpers.
            container.Register(Component.For<ICultureProvider>().ImplementedBy<CultureProvider>().LifeStyle.Singleton);
        }
    }
}
