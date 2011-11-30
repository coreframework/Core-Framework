using System.Reflection;
using Castle.Windsor;
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
        }
    }
}
