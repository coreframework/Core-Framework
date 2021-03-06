﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminAreaRegistration.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Framework.Mvc.Routing;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin
{
    /// <summary>
    /// Registers admin area.
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to be registered.
        /// </summary>
        /// <value>THe nam of area.</value>
        /// <returns>The name of the area to be registered.</returns>
        public override String AreaName
        {
            get
            {
                return "Admin";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            SetupModules();

            SetupPermissions();

            context.MapRoute(null, "admin", MVC.Admin.AdminHome.Index());

            context.MapRoute("Admin.Users", "admin/users", MVC.Admin.User.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.DynamicGridData", "admin/users/DynamicGridData", MVC.Admin.User.DynamicGridData());
            context.MapRoute("Admin.Users.New", "admin/users/new", MVC.Admin.User.New());
            context.MapRoute("Admin.Users.Create", "admin/users", MVC.Admin.User.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.Users.Edit", "admin/user/{id}", new { controller = "User", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.Update", "admin/user/{id}", MVC.Admin.User.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Users.Remove", "admin/user/remove/{id}", new { controller = "User", action = "Remove", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.ConfirmRemove", "admin/user/{id}", MVC.Admin.User.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.Users.UpdateUserGroups", "admin/user/groups/{id}/UpdateUserGroups", MVC.Admin.User.UpdateUserGroups());
            context.MapRoute("Admin.Users.UserGroups", "admin/user/groups/{id}", new { controller = "User", action = "UserGroups", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.UserGroupsDynamicGridData", "admin/user/{id}/groups/UserGroupsDynamicGridData", MVC.Admin.User.UserGroupsDynamicGridData());
            context.MapRoute("Admin.Users.UpdateRoles", "admin/user/roles/{id}/UpdateRoles", MVC.Admin.User.UpdateRoles());
            context.MapRoute("Admin.Users.UserRoles", "admin/user/roles/{id}", new { controller = "User", action = "Roles", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.RolesDynamicGridData", "admin/user/{id}/roles/RolesDynamicGridData", MVC.Admin.User.RolesDynamicGridData());

            context.MapRoute("Admin.UserGroups", "admin/groups", MVC.Admin.UserGroup.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.DynamicGridData", "admin/group/DynamicGridData", MVC.Admin.UserGroup.DynamicGridData());
            context.MapRoute("Admin.UserGroups.New", "admin/new-group/new", MVC.Admin.UserGroup.New());
            context.MapRoute("Admin.UserGroups.Create", "admin/new-group", MVC.Admin.UserGroup.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.UserGroups.Edit", "admin/edit-group/{id}", new { controller = "UserGroup", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.Update", "admin/edit-group/{id}", MVC.Admin.UserGroup.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.UserGroups.UsersDynamicGridData", "admin/group/{id}/groups/UsersDynamicGridData", MVC.Admin.UserGroup.UsersDynamicGridData());
            context.MapRoute("Admin.UserGroups.Remove", "admin/remove-group/{id}", new { controller = "UserGroup", action = "Remove", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.ConfirmRemove", "admin/remove-group/{id}", MVC.Admin.UserGroup.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.UserGroups.UpdateUsers", "admin/group/users/{id}/UpdateUsers", MVC.Admin.UserGroup.UpdateUsers());
            context.MapRoute("Admin.UserGroups.Users", "admin/group/users/{id}", new { controller = "UserGroup", action = "Users", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.UpdateRoles", "admin/group/roles/{id}/UpdateRoles", MVC.Admin.UserGroup.UpdateRoles());
            context.MapRoute("Admin.UserGroups.UserRoles", "admin/group/roles/{id}", new { controller = "UserGroup", action = "Roles", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.RolesDynamicGridData", "admin/group/{id}/roles/RolesDynamicGridData", MVC.Admin.UserGroup.RolesDynamicGridData());

            context.MapRoute("Admin.Roles", "admin/roles", MVC.Admin.Role.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.DynamicGridData", "admin/roles/DynamicGridData", MVC.Admin.Role.DynamicGridData());
            context.MapRoute("Admin.Roles.New", "admin/roles/new", MVC.Admin.Role.New());
            context.MapRoute("Admin.Roles.Create", "admin/roles", MVC.Admin.Role.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.Roles.Edit", "admin/role/{id}", new { controller = "Role", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.ChangeLanguage", "admin/role/change-language", new { controller = "Role", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Roles.Update", "admin/role/{id}", MVC.Admin.Role.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Roles.Remove", "admin/role/remove/{id}", new { controller = "Role", action = "Remove", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.ConfirmRemove", "admin/role/{id}", MVC.Admin.Role.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.Roles.UpdateUsers", "admin/role/users/{id}/UpdateUsers", MVC.Admin.Role.UpdateUsers());
            context.MapRoute("Admin.Roles.Users", "admin/role/users/{id}", new { controller = "Role", action = "Users", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.UsersDynamicGridData", "admin/role/{id}/users/UsersDynamicGridData", MVC.Admin.Role.UsersDynamicGridData());
            context.MapRoute("Admin.Roles.UpdateUserGroups", "admin/role/groups/{id}/UpdateUserGroups", MVC.Admin.Role.UpdateUserGroups());
            context.MapRoute("Admin.Roles.UserGroups", "admin/role/groups/{id}", new { controller = "Role", action = "UserGroups", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.UserGroupsDynamicGridData", "admin/role/{id}/groups/UserGroupsDynamicGridData", MVC.Admin.Role.UserGroupsDynamicGridData());
            context.MapRoute("Admin.Roles.Permissions", "admin/role/permissions/{roleId}", new { controller = "Role", action = "Permissions", area = AreaName, roleId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.PermissionsByResource", "admin/role/{roleId}/permissions/{resource}", MVC.Admin.Role.Permissions());
            context.MapRoute(null, "admin/apply-permissions", MVC.Admin.Role.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.Modules", "admin/modules", MVC.Admin.Module.Index());
            context.MapRoute("Admin.Modules.DynamicGridData", "admin/module/DynamicGridData", MVC.Admin.Module.DynamicGridData());
            context.MapRoute("Admin.Modules.Edit", "admin/module/{id}", new { controller = "Module", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Modules.ChangeLanguage", "admin/module/change-language", new { controller = "Module", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Modules.Update", "admin/module/{id}", MVC.Admin.Module.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Modules.Install", "admin/install-module/{id}", new { controller = "Module", action = "Install", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Modules.Uninstall", "admin/uninstall-module/{id}", new { controller = "Module", action = "Uninstall", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Modules.ConfirmInstall", "admin/install-module/{id}", MVC.Admin.Module.ConfirmInstall(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Modules.ConfirmUninstall", "admin/uninstall-module/{id}", MVC.Admin.Module.ConfirmUninstall(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.Pages", "admin/pages", MVC.Admin.Page.Index());
            context.MapRoute("Admin.Pages.DynamicGridData", "admin/pages/DynamicGridData", MVC.Admin.Page.DynamicGridData());

            context.MapRoute("Admin.PageTemplates", "admin/page-templates", MVC.Admin.PageTemplate.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.PageTemplates.DynamicGridData", "admin/page-templates/DynamicGridData", MVC.Admin.PageTemplate.DynamicGridData());
            context.MapRoute("Admin.PageTemplates.New", "admin/page-templates/new", MVC.Admin.PageTemplate.New());
            context.MapRoute("Admin.PageTemplates.Create", "admin/page-templates", MVC.Admin.PageTemplate.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.PageTemplates.Edit", "admin/page-templates/{id}", new { controller = "PageTemplate", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.PageTemplates.Update", "admin/page-templates/{id}", MVC.Admin.PageTemplate.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.PageTemplates.Remove", "admin/page-templates/remove/{id}", new { controller = "PageTemplate", action = "Remove", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.PageTemplates.ConfirmRemove", "admin/page-templates/{id}", MVC.Admin.PageTemplate.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });

            context.MapRoute("Admin.Widgets", "admin/widgets", MVC.Admin.Widget.Index());
            context.MapRoute("Admin.Widgets.DynamicGridData", "admin/widget/DynamicGridData", MVC.Admin.Widget.DynamicGridData());
            context.MapRoute("Admin.Widgets.Edit", "admin/widget/{id}", new { controller = "Widget", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Widgets.ChangeLanguage", "admin/widget/change-language", new { controller = "Widget", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Widgets.Update", "admin/widget/{id}", MVC.Admin.Widget.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Widgets.Enable", "admin/widget/enable/{id}", new { controller = "Widget", action = "Enable", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Widgets.Disable", "admin/widget/disable/{id}", new { controller = "Widget", action = "Disable", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });

            context.MapRoute("Admin.SiteSettings", "admin/site-settings", MVC.Admin.SiteSettings.Show());
            context.MapRoute("Admin.UpdateSiteSettings", "admin/update-site-settings", MVC.Admin.SiteSettings.Edit(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.Error", "admin/error", MVC.Admin.AdminError.Index());

        }

        #region Helper Methods

        /// <summary>
        /// Setups the modules.
        /// </summary>
        protected static void SetupModules()
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();

            var existingPlugins = pluginService.GetAll();

            var registeredPlugins = Application.Plugins;

            foreach (var plugin in registeredPlugins)
            {
                ICorePlugin plugin1 = plugin;

                if (!existingPlugins.Any(pl => pl.Identifier == plugin1.Identifier))
                {
                    var newPlugin = new Plugin
                    {
                        Title = plugin1.Title,
                        Description = plugin1.Description,
                        Identifier = plugin1.Identifier,
                        Status = PluginStatus.NotInstalled,
                        CreateDate = DateTime.Now

                    };
                    pluginService.Save(newPlugin);
                }
            }

            SetupPlugins(pluginService.GetAll());
        }

        /// <summary>
        /// Setups the plugins.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        protected static void SetupPlugins(IEnumerable<Plugin> plugins)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();

            var existingWidgets = widgetService.GetAll();

            var registeredWidgets = MvcApplication.Widgets;

            foreach (ICoreWidget widget in registeredWidgets)
            {
                ICoreWidget widget1 = widget;

                if (!existingWidgets.Any(
                        wd => wd.Identifier == widget1.Identifier && ((wd.Plugin == null && widget1.Plugin == null) || (wd.Plugin != null && widget1.Plugin != null && wd.Plugin.Identifier == widget1.Plugin.Identifier))))
                {
                    Plugin plugin = null;
                    if (widget1.Plugin != null)
                    {
                        plugin = plugins.FirstOrDefault(pl => pl.Identifier == widget1.Plugin.Identifier);
                    }
                    var newWidget = new Widget
                    {
                        Identifier = widget1.Identifier,
                        Title = widget1.Title,
                        IsDetailsWidget = widget1.IsDetailsWidget,
                        IsPlaceHolder = widget1.IsPlaceHolder,
                        Plugin = plugin,
                    };
                    if (plugin == null)
                    {
                        newWidget.Status = WidgetStatus.Enabled;
                    }
                    widgetService.Save(newWidget);
                }
            }
        }

        protected static void SetupPermissions()
        {
            var entityTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();
            var existingItems = entityTypeService.GetAll().ToList();

            var itemsToRemove = existingItems.Where(item => !MvcApplication.PermissibleObjects.Exists(it => item.Name == PermissionsHelper.GetEntityType(it.GetType()))).ToList();

            var itemsToAdd = MvcApplication.PermissibleObjects.Where(item => !existingItems.Exists(it => it.Name == PermissionsHelper.GetEntityType(item.GetType()))).ToList();

            foreach (IPermissible item in itemsToAdd)
            {
                var entityType = new EntityType
                {
                    Name = PermissionsHelper.GetEntityType(item.GetType()),

                };

                entityTypeService.Save(entityType);
            }

            foreach (var item in itemsToRemove)
            {
                entityTypeService.Delete(item);
            }
        }

        #endregion
    }
}
