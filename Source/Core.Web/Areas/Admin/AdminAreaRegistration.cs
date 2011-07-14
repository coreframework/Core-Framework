// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminAreaRegistration.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Framework.MVC.Routing;
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
            context.MapRoute("Admin.Users.Remove", "admin/user/{id}/remove", MVC.Admin.User.Remove());
            context.MapRoute("Admin.Users.ConfirmRemove", "admin/user/{id}", MVC.Admin.User.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.Users.UserGroups", "admin/user/{id}/groups", MVC.Admin.User.UserGroups(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Users.UserGroupsDynamicGridData", "admin/user/{id}/groups/UserGroupsDynamicGridData", MVC.Admin.User.UserGroupsDynamicGridData());
            context.MapRoute("Admin.Users.UpdateUserGroups", "admin/user/{id}/groups/UpdateUserGroups", MVC.Admin.User.UpdateUserGroups());

            context.MapRoute("Admin.UserGroups", "admin/groups", MVC.Admin.UserGroup.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.DynamicGridData", "admin/groups/DynamicGridData", MVC.Admin.UserGroup.DynamicGridData());
            context.MapRoute("Admin.UserGroups.New", "admin/groups/new", MVC.Admin.UserGroup.New());
            context.MapRoute("Admin.UserGroups.Create", "admin/groups", MVC.Admin.UserGroup.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.UserGroups.Edit", "admin/groups/{id}", new { controller = "UserGroup", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.Update", "admin/groups/{id}", MVC.Admin.UserGroup.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.UserGroups.UsersDynamicGridData", "admin/groups/{id}/groups/UsersDynamicGridData", MVC.Admin.UserGroup.UsersDynamicGridData());
            context.MapRoute("Admin.UserGroups.Remove", "admin/groups/{id}/remove", MVC.Admin.UserGroup.Remove());
            context.MapRoute("Admin.UserGroups.ConfirmRemove", "admin/groups/{id}", MVC.Admin.UserGroup.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.UserGroups.Users", "admin/groups/{id}/users", MVC.Admin.UserGroup.Users(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.UserGroups.UpdateUsers", "admin/groups/{id}/users/UpdateUsers", MVC.Admin.UserGroup.UpdateUsers());

            context.MapRoute("Admin.Roles", "admin/roles", MVC.Admin.Role.Index(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.DynamicGridData", "admin/roles/DynamicGridData", MVC.Admin.Role.DynamicGridData());
            context.MapRoute("Admin.Roles.New", "admin/roles/new", MVC.Admin.Role.New());
            context.MapRoute("Admin.Roles.Create", "admin/roles", MVC.Admin.Role.Create(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.Roles.Edit", "admin/role/{id}", new { controller = "Role", action = "Edit", area = AreaName, id = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.ChangeLanguage", "admin/role/change-language", new { controller = "Role", action = "ChangeLanguage", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Roles.Update", "admin/role/{id}", MVC.Admin.Role.Update(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Roles.Remove", "admin/role/{id}/remove", MVC.Admin.Role.Remove());
            context.MapRoute("Admin.Roles.ConfirmRemove", "admin/role/{id}", MVC.Admin.Role.ConfirmRemove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Delete) });
            context.MapRoute("Admin.Roles.Users", "admin/role/{id}/users", MVC.Admin.Role.Users(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.UpdateUsers", "admin/role/{id}/users/UpdateUsers", MVC.Admin.Role.UpdateUsers());
            context.MapRoute("Admin.Roles.UsersDynamicGridData", "admin/role/{id}/users/UsersDynamicGridData", MVC.Admin.Role.UsersDynamicGridData());
            context.MapRoute("Admin.Roles.UserGroups", "admin/role/{id}/groups", MVC.Admin.Role.UserGroups(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Roles.UserGroupsDynamicGridData", "admin/role/{id}/groups/UserGroupsDynamicGridData", MVC.Admin.Role.UserGroupsDynamicGridData());
            context.MapRoute("Admin.Roles.UpdateUserGroups", "admin/role/{id}/groups/UpdateUserGroups", MVC.Admin.Role.UpdateUserGroups());
            context.MapRoute("Admin.Roles.Permissions", "admin/role/{roleId}/permissions", MVC.Admin.Role.Permissions());
            context.MapRoute("Admin.Roles.PermissionsByResource", "admin/role/{roleId}/permissions/{resource}", MVC.Admin.Role.Permissions());
            context.MapRoute(null, "admin/apply-permissions", MVC.Admin.Role.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.Modules", "admin/modules", MVC.Admin.Module.Index());
            context.MapRoute("Admin.Modules.DynamicGridData", "admin/modules/DynamicGridData", MVC.Admin.Module.DynamicGridData());
            context.MapRoute("Admin.Modules.Install", "admin/modules/{id}/install", MVC.Admin.Module.Install(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Modules.Uninstall", "admin/modules/{id}/uninstall", MVC.Admin.Module.Uninstall(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Modules.ConfirmInstall", "admin/modules/{id}/install", MVC.Admin.Module.ConfirmInstall(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.Modules.ConfirmUninstall", "admin/modules/{id}/uninstall", MVC.Admin.Module.ConfirmUninstall(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.Widgets", "admin/widgets", MVC.Admin.Widget.Index());
            context.MapRoute("Admin.Widgets.DynamicGridData", "admin/widgets/DynamicGridData", MVC.Admin.Widget.DynamicGridData());
            context.MapRoute("Admin.Widgets.Enable", "admin/widgets/{id}/enable", MVC.Admin.Widget.Enable());
            context.MapRoute("Admin.Widgets.Disable", "admin/widgets/{id}/disable", MVC.Admin.Widget.Disable());          
        }

        #region Helper Methods

        /// <summary>
        /// Setups the modules.
        /// </summary>
        protected void SetupModules()
        {
            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();

            var existingPlugins =  pluginService.GetAll();

            var registeredPlugins = MvcApplication.Plugins;

            foreach (var plugin in registeredPlugins)
            {
                ICorePlugin plugin1 = plugin;

                if (!((List<Plugin>)existingPlugins).Exists(pl=>pl.Identifier==plugin1.Identifier))
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

            SetupPlugins((List<Plugin>) pluginService.GetAll());
        }

        /// <summary>
        /// Setups the plugins.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        protected void SetupPlugins(List<Plugin> plugins)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();

            var existingWidgets = widgetService.GetAll();

            var registeredWidgets = MvcApplication.Widgets;

            foreach (ICoreWidget widget in registeredWidgets)
            {
                ICoreWidget widget1 = widget;

                if (!((List<Widget>) existingWidgets).Exists(
                        wd => wd.Identifier == widget1.Identifier && wd.Plugin.Identifier == widget1.Plugin.Identifier))
                {
                    Plugin plugin = plugins.Find(pl => pl.Identifier == widget1.Plugin.Identifier);
                    if (plugin != null)
                    {
                        var newWidget = new Widget
                                            {
                                                Identifier = widget1.Identifier,
                                                Title = widget1.Title,
                                                Plugin = plugin
                                            };

                        widgetService.Save(newWidget);
                    }
                }
            }
        }

        protected static void SetupPermissions()
        {
            var entityTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();
            var existingItems = entityTypeService.GetAll().ToList();

            var itemsToRemove = existingItems.Where(item => !MvcApplication.PermissibleObjects.Exists(it => item.Name==PermissionsHelper.GetEntityType(it.GetType()))).ToList();

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
