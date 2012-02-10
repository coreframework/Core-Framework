using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.Permissions.Authentication;
using Core.Framework.Permissions.Contracts;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Services;
using Core.Web.NHibernate.Services.Common;
using Core.Web.NHibernate.Services.Permissions;
using Core.Web.NHibernate.Services.Widgets;
using Framework.Core.Modules;
using Framework.Facilities.NHibernate.Castle;

namespace Core.Web.NHibernate
{
    /// <summary>
    /// Register nhibernate implementation components.
    /// </summary>
    public class CoreWebNHibernateModule : IModule
    {
        #region IModule members

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register nhibernate fluent mapper.
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("core_mapper").LifeStyle.Singleton);

            container.Register(Component.For<IPluginService>().ImplementedBy<NHibernatePluginService>().LifeStyle.Singleton);
            container.Register(Component.For<IPluginLocaleService>().ImplementedBy<NHibernatePluginLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IWidgetService>().ImplementedBy<NHibernateWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IWidgetLocaleService>().ImplementedBy<NHibernateWidgetLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IMigrationService>().ImplementedBy<NHibernateMigrationService>().LifeStyle.Singleton);
            container.Register(Component.For<ISchemaInfoService>().ImplementedBy<NHibernateSchemaInfoService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageService>().ImplementedBy<NHibernatePageService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageLocaleService>().ImplementedBy<NHibernatePageLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageLayoutTemplateService>().ImplementedBy<NHibernatePageLayoutTemplateService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageLayoutService>().ImplementedBy<NHibernatePageLayoutService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageLayoutRowService>().ImplementedBy<NHibernatePageLayoutRowService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageLayoutColumnWidthValueService>().ImplementedBy<NHibernatePageLayoutColumnWidthValueService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageWidgetService>().ImplementedBy<NHibernatePageWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageWidgetSettingService>().ImplementedBy<NHibernatePageWidgetSettingService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageSettingService>().ImplementedBy<NHibernatePageSettingsService>().LifeStyle.Singleton);
            container.Register(Component.For<ISiteMapWidgetService>().ImplementedBy<NHibernateSiteMapWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IListMenuWidgetService>().ImplementedBy<NHibernateListMenuWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IBreadcrumbsWidgetService>().ImplementedBy<NHibernateBreadcrumbsWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<IUserService>().ImplementedBy<NHibernateUserService>().LifeStyle.Singleton);
            container.Register(Component.For<IUserGroupService>().ImplementedBy<NHibernateUserGroupService>().LifeStyle.Singleton);
            container.Register(Component.For<IRoleService>().ImplementedBy<NHibernateRoleService>().LifeStyle.Singleton);
            container.Register(Component.For<IRoleLocaleService>().ImplementedBy<NHibernateRoleLocaleService>().LifeStyle.Singleton);
            container.Register(Component.For<IEntityTypeService>().ImplementedBy<NHibernateEntityTypeService>().LifeStyle.Singleton);
            container.Register(Component.For<IPermissionService>().ImplementedBy<NHibernatePermissionService>().LifeStyle.Singleton);
            container.Register(Component.For<IPermissionCommonService>().ImplementedBy<PermissionCommonService>().LifeStyle.Singleton);
            container.Register(Component.For<IPageCommonService>().ImplementedBy<PageCommonService>().LifeStyle.Singleton);
            container.Register(Component.For<IAuthenticationHelper>().ImplementedBy<FormsAuthenticationHelper>().LifeStyle.Singleton);
            container.Register(Component.For<INavigationMenuWidgetService>().ImplementedBy<NHibernateNavigationMenuWidgetService>().LifeStyle.Singleton);
            container.Register(Component.For<ISiteSettingsService>().ImplementedBy<NHibernateSiteSettingsService>().LifeStyle.Singleton);
        }

        #endregion
    }
}
