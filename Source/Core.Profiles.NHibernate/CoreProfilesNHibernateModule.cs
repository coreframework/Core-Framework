using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Services;
using Framework.Facilities.NHibernate.Castle;

namespace Core.Profiles.NHibernate
{
    public static class CoreProfilesNHibernateModule
    {
        public static void Install(IWindsorContainer container)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("profiles_mapper").LifeStyle.Singleton);

            container.Register(Component.For<IProfileTypeService>().ImplementedBy<NHibernateProfileTypeService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileTypeLocaleService>().ImplementedBy<NHibernateProfileTypeLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IRegistrationWidgetService>().ImplementedBy<NHibernateRegistrationWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileElementService>().ImplementedBy<NHibernateProfileElementService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileElementLocaleService>().ImplementedBy<NHibernateProfileElementLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileHeaderService>().ImplementedBy<NHibernateProfileHeaderService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileHeaderLocaleService>().ImplementedBy<NHibernateProfileHeaderLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IUserProfileService>().ImplementedBy<NHibernateUserProfileService>().LifeStyle.Singleton);
            container.Register(Component.For<IUserProfileElementService>().ImplementedBy<NHibernateUserProfileElementService>().LifeStyle.Singleton);
            container.Register(Component.For<IProfileWidgetService>().ImplementedBy<NHibernateProfileWidgetService>().LifeStyle.Singleton);
        }
    }
}
