using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.Languages.NHibernate
{
    public class CoreLanguagesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("languages_mapper").LifeStyle.Transient);

            // Register data services.
            container.Register(Component.For<ILanguageService>().ImplementedBy<NHibernateLanguageService>().LifeStyle.Transient);
//            container.Register(Component.For<IFormElementService>().ImplementedBy<NHibernateFormElementService>().LifeStyle.Transient);
//            container.Register(Component.For<IFormBuilderWidgetService>().ImplementedBy<NHibernateFormBuilderWidgetService>().LifeStyle.Transient);
//            container.Register(Component.For<IFormWidgetAnswerService>().ImplementedBy<NHibernateFormWidgetAnswerService>().LifeStyle.Transient);
//            container.Register(Component.For<IFormWidgetAnswerValueService>().ImplementedBy<NHibernateFormWidgetAnswerValueService>().LifeStyle.Transient);
        }
    }
}
