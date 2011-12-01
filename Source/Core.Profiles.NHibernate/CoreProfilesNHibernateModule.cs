using System.Reflection;
using Castle.Windsor;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;
using Castle.MicroKernel.Registration;

namespace Core.Profiles.NHibernate
{
    public static class CoreProfilesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("profiles_mapper").LifeStyle.Transient);

            container.Register(Component.For<IProfileTypeService>().ImplementedBy<NHibernateProfileTypeService>().LifeStyle.Transient);
            container.Register(Component.For<IProfileTypeLocaleService>().ImplementedBy<NHibernateProfileTypeLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<IRegistrationWidgetService>().ImplementedBy<NHibernateRegistrationWidgetService>().LifeStyle.Transient);
        }
    }
}
