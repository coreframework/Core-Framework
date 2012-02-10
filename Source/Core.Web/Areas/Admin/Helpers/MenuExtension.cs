// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuExtension.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Controllers;
using Core.Web.NHibernate.Models;
using Framework.Mvc.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Helpers
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality to render admin main menu.
    /// </summary>
    public static class MenuExtension
    {
        private static Dictionary<String, IEnumerable<IMenuItem>> InitializeMenu(HttpContext context, HtmlHelper html)
        {
            var menuItems = new Dictionary<String, IEnumerable<IMenuItem>>();
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var user = context.CorePrincipal();
            bool isUsersAllowed = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(User), null);
            bool isUserGroupsAllowed = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(UserGroup), null);
            bool isRolesAllowed = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(Role), null);
            if (isUsersAllowed || isUserGroupsAllowed || isRolesAllowed)
            {
                var usersMenuItem = new List<IMenuItem>();
                if (isUsersAllowed)
                {
                    usersMenuItem.Add(new ActionLink<UserController>(html.Translate(".Users"), Links.Content.Images.Admin.ico1_png, c => c.Index()));
                }
                if (isUserGroupsAllowed)
                {
                    usersMenuItem.Add(new ActionLink<UserGroupController>(html.Translate(".UserGroups"), Links.Content.Images.Admin.ico2_png, c => c.Index()));
                }
                if (isUsersAllowed)
                {
                    usersMenuItem.Add(new ActionLink<RoleController>(html.Translate(".Roles"), Links.Content.Images.Admin.ico3_png, c => c.Index()));
                }
                menuItems.Add(html.Translate(".AccountsAndPermissions"), usersMenuItem);
            }

            var siteSettingsAccess = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(SiteSettings), null);
            var pluginsAccess = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(Plugin), null);
            var pagesAccess = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(Page), null);
            var pagesTemplateAccess = permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(PageTemplate), null);
            if (siteSettingsAccess || pluginsAccess || pagesAccess || pagesTemplateAccess)
            {
                var administrationMenuItem = new List<IMenuItem>();

                if (siteSettingsAccess)
                {
                    administrationMenuItem.Add(new ActionLink<SiteSettingsController>(html.Translate(".SiteSettings"), Links.Content.Images.Admin.ico1_png, c => c.Show()));
                }
                if (pluginsAccess)
                {
                    administrationMenuItem.Add(new ActionLink<ModuleController>(html.Translate(".Modules"), Links.Content.Images.Admin.ico5_png, c => c.Index()));
                    administrationMenuItem.Add(new ActionLink<WidgetController>(html.Translate(".Widgets"), Links.Content.Images.Admin.ico6_png, c => c.Index()));
                }
                if (pagesAccess)
                {
                    administrationMenuItem.Add(new ActionLink<PageController>(html.Translate(".Pages"), Links.Content.Images.Admin.ico6_png, c => c.Index()));
                    administrationMenuItem.Add(new ActionLink<PageTemplateController>(html.Translate(".PageTemplates"), Links.Content.Images.Admin.ico6_png, c => c.Index()));
                }

                menuItems.Add(html.Translate(".Administration"), administrationMenuItem);
            }

            var pluginHelper = ServiceLocator.Current.GetInstance<IPluginHelper>();
            var usersMenuItem1 = (from verb in Application.GetVerbsForCategory("AdminModules")
                                  where pluginHelper.IsPluginEnabled(verb.ControllerPluginIdentifier) && verb.IsAllowed(context.CorePrincipal())
                                  select new RouteLink(verb.Name, Links.Content.Images.Admin.ico3_png, verb.RouteName)).Cast<IMenuItem>().ToList();

            if (usersMenuItem1.Count > 0)
                menuItems.Add(html.Translate(".Modules"), usersMenuItem1);

            return menuItems;
        }

        /// <summary>
        /// Renders admin area main menu.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="url">The URL helper.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Html markup for admin main menu.
        /// </returns>
        public static MvcHtmlString RenderMenu(this HtmlHelper html, UrlHelper url, HttpContext context)
        {
            var items = InitializeMenu(context, html);
            var activeSection = String.Empty;
            foreach (var section in items)
            {
                if (section.Value.Any(item => item.IsCurrent(html.ViewContext.RequestContext)))
                {
                    activeSection = section.Key;
                }
                if (!String.IsNullOrEmpty(activeSection))
                {
                    break;
                }
            }

            var menu = new TagBuilder("div");
            menu.Attributes["id"] = "accordion";
            menu.InnerHtml = RenderSectionsList(html, url, items, activeSection);
            return MvcHtmlString.Create(menu.ToString());
        }

        private static String RenderSectionsList(HtmlHelper html, UrlHelper url, Dictionary<String, IEnumerable<IMenuItem>> items, String activeSection)
        {
            var buffer = new StringBuilder();
            int number = 0;
            foreach (var section in items)
            {
                var isCurrent = activeSection.Equals(section.Key);
                buffer.Append(RenderSection(html, url, section.Key, section.Value, isCurrent, number));
                number++;
            }
            return buffer.ToString();
        }

        private static String RenderSection(HtmlHelper html, UrlHelper url, String title, IEnumerable<IMenuItem> items, bool isCurrent, int number)
        {
            var sectionContent = new StringBuilder();
            var firstItem = items.FirstOrDefault();

            //renders section header
            sectionContent.Append(RenderSectionHeader(url, title, isCurrent, number, firstItem));

            var divSectionItems = new TagBuilder("div");
            var sectionItems = new TagBuilder("ul");
            sectionItems.Attributes["id"] = "nav_sub";
            sectionItems.InnerHtml = RenderMenuItems(html, url, items);
            divSectionItems.InnerHtml = sectionItems.ToString();
            sectionContent.Append(divSectionItems.ToString());

            return sectionContent.ToString();
        }

        private static String RenderSectionHeader(UrlHelper url, String title, bool isCurrent, int number, IMenuItem item)
        {
            var h3 = new TagBuilder("h3");
            if (isCurrent)
            {
                h3.Attributes["id"] = "active";
                h3.Attributes["number"] = number.ToString();
            }
            var em = new TagBuilder("em");
            if (item != null && !String.IsNullOrEmpty(item.Image))
            {
                em.Attributes["style"] = String.Format("background: url(\"{0}\") no-repeat scroll 5px 50% transparent;", item.GetImageUrl(url));
            }
            var link = new TagBuilder("a");
            link.Attributes["href"] = item != null ? item.GetUrl(url) : "javascript:void(0)";
            link.InnerHtml = title;
            em.InnerHtml = link.ToString();
            h3.InnerHtml = em.ToString();

            return h3.ToString();
        }

        private static String RenderMenuItems(HtmlHelper html, UrlHelper url, IEnumerable<IMenuItem> items)
        {
            var itemsContent = new StringBuilder();

            foreach (var item in items)
            {
                var itemTag = new TagBuilder("li");
                var link = new TagBuilder("a");
                link.Attributes["href"] = item.GetUrl(url);
                var em = new TagBuilder("em");
                if (!String.IsNullOrEmpty(item.Image))
                {
                    em.Attributes["style"] = String.Format("background: url(\"{0}\") no-repeat scroll 5px 50% transparent;", item.GetImageUrl(url));
                }
                em.InnerHtml = (new TagBuilder("spam") { InnerHtml = item.Title }).ToString();
                if (item.IsCurrent(html.ViewContext.RequestContext))
                {
                    itemTag.Attributes["class"] = "active";
                }
                link.InnerHtml = em.ToString();
                itemTag.InnerHtml = link.ToString();

                itemsContent.Append(itemTag.ToString());
            }

            return itemsContent.ToString();
        }
    }
}