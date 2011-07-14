using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Framework.Permissions.Contracts;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Helpers;
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
            container.Register(Component.For<INHibernateMapper>().Instance(new StandardFluentMapper(Assembly.GetExecutingAssembly())).Named("core_mapper").LifeStyle.Transient);

            container.Register(Component.For<IPluginService>().ImplementedBy<NHibernatePluginService>().LifeStyle.Transient);
            container.Register(Component.For<IWidgetService>().ImplementedBy<NHibernateWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IMigrationService>().ImplementedBy<NHibernateMigrationService>().LifeStyle.Transient);
            container.Register(Component.For<ISchemaInfoService>().ImplementedBy<NHibernateSchemaInfoService>().LifeStyle.Transient);
            container.Register(Component.For<IPageService>().ImplementedBy<NHibernatePageService>().LifeStyle.Transient);
            container.Register(Component.For<IPageLayoutTemplateService>().ImplementedBy<NHibernatePageLayoutTemplateService>().LifeStyle.Transient);
            container.Register(Component.For<IPageLayoutService>().ImplementedBy<NHibernatePageLayoutService>().LifeStyle.Transient);
            container.Register(Component.For<IPageLayoutRowService>().ImplementedBy<NHibernatePageLayoutRowService>().LifeStyle.Transient);
            container.Register(Component.For<IPageLayoutColumnWidthValueService>().ImplementedBy<NHibernatePageLayoutColumnWidthValueService>().LifeStyle.Transient);
            container.Register(Component.For<IPageWidgetService>().ImplementedBy<NHibernatePageWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IPageWidgetSettingService>().ImplementedBy<NHibernatePageWidgetSettingService>().LifeStyle.Transient);
            container.Register(Component.For<IPageSettingService>().ImplementedBy<NHibernatePageSettingsService>().LifeStyle.Transient);
            container.Register(Component.For<ISiteMapWidgetService>().ImplementedBy<NHibernateSiteMapWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IListMenuWidgetService>().ImplementedBy<NHibernateListMenuWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IBreadcrumbsWidgetService>().ImplementedBy<NHibernateBreadcrumbsWidgetService>().LifeStyle.Transient);
            container.Register(Component.For<IUserService>().ImplementedBy<NHibernateUserService>().LifeStyle.Transient);
            container.Register(Component.For<IUserGroupService>().ImplementedBy<NHibernateUserGroupService>().LifeStyle.Transient);
            container.Register(Component.For<IRoleService>().ImplementedBy<NHibernateRoleService>().LifeStyle.Transient);
            container.Register(Component.For<IRoleLocaleService>().ImplementedBy<NHibernateRoleLocaleService>().LifeStyle.Transient);
            container.Register(Component.For<IEntityTypeService>().ImplementedBy<NHibernateEntityTypeService>().LifeStyle.Transient);
            container.Register(Component.For<IPermissionService>().ImplementedBy<NHibernatePermissionService>().LifeStyle.Transient);
            container.Register(Component.For<IPermissionCommonService>().ImplementedBy<PermissionCommonService>().LifeStyle.Transient);
            container.Register(Component.For<IPageCommonService>().ImplementedBy<PageCommonService>().LifeStyle.Transient);
            container.Register(Component.For<IAuthenticationHelper>().ImplementedBy<FormsAuthenticationHelper>().LifeStyle.Transient);
        }

        #endregion
    }
}
