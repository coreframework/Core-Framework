﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.Web.Helpers
{
    public class PageHelper
    {
        /// <summary>
        /// Binds the page view model.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static PageViewModel BindPageViewModel(Page page,ICorePrincipal user)
        {
            bool isPageOwner = page != null && user != null && page.User != null &&
                            page.User.Id == user.PrincipalId;

            return new PageViewModel {IsPageOwner = isPageOwner}.MapFrom(page);
        }

        /// <summary>
        /// Adds the widget to page.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="widgetIdentifier">The widget identifier.</param>
        /// <returns></returns>
        public static PageWidget AddWidgetToPage(long pageId, String widgetIdentifier)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();

            var page = pageService.Find(pageId);

            if (page != null)
            {
                page.Widgets.Update(
                  wd =>
                  {
                      wd.OrderNumber =
                        (wd.ColumnNumber == 1 ?
                            wd.OrderNumber + 1 :
                            wd.OrderNumber);
                  }
                      );
                pageService.Save(page);
                ICorePrincipal currentUser = HttpContext.Current.CorePrincipal();
                var newPageWidget = new PageWidget
                                        {
                                            Page = page,
                                            WidgetIdentifier = widgetIdentifier,
                                            ColumnNumber = 1,
                                            OrderNumber = 1,
                                            User = currentUser != null ? new User { Id = currentUser.PrincipalId } : null 
                                        };

                if (pageWidgetService.Save(newPageWidget))
                    return newPageWidget;
            }

            return null;
        }

        /// <summary>
        /// Removes the widget from page.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <param name="user">The user.</param>
        public static void RemoveWidgetFromPage(long pageWidgetId, ICorePrincipal user)
        {
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();

            var pageWidget = pageWidgetService.Find(pageWidgetId);

            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            bool isPageOwner = user != null && pageWidget.Page.User != null &&
                       pageWidget.Page.User.Id == user.PrincipalId;

            if (pageWidget != null && permissionService.IsAllowed((Int32)PageOperations.Update, user, typeof(Page), pageWidget.Page.Id, isPageOwner, PermissionOperationLevel.Object))
            {
                if (WidgetHelper.IsManageWidgetAllowed(pageWidget, user, pageWidget.Id))
                {
                    var page = pageService.Find(pageWidget.Page.Id);
                    page.Widgets.Update(
                        wd =>
                        {
                            wd.OrderNumber =
                              (wd.ColumnNumber == pageWidget.ColumnNumber && wd.OrderNumber > pageWidget.OrderNumber ?

                                  wd.OrderNumber - 1 :
                                  wd.OrderNumber);
                        }
                    );

                    page.RemoveWidget(pageWidget);
                    pageService.Save(page);
                }
            }
        }

        /// <summary>
        /// Updates the widgets positions.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <param name="user">The user.</param>
        public static void UpdateWidgetsPositions(long pageWidgetId, int columnNumber, int orderNumber,ICorePrincipal user)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            var pageWidget = pageWidgetService.Find(pageWidgetId);
            var permissionService  = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
         
            if (pageWidget != null)
            {
                bool isPageOwner = user != null && pageWidget.Page.User != null &&
                         pageWidget.Page.User.Id == user.PrincipalId;

                if (permissionService.IsAllowed((Int32) PageOperations.Update, user, typeof (Page), pageWidget.Page.Id,
                                                isPageOwner, PermissionOperationLevel.Object))
                {
                    pageWidget.Page.Widgets.Update(
                        wd =>
                            {
                                wd.OrderNumber =
                                    (wd.ColumnNumber == pageWidget.ColumnNumber &&
                                     wd.OrderNumber > pageWidget.OrderNumber
                                         ? wd.OrderNumber - 1
                                         : wd.OrderNumber);
                            }
                        );
                    pageWidget.Page.Widgets.Update(
                        wd =>
                            {
                                wd.OrderNumber =
                                    (wd.ColumnNumber == columnNumber && wd.OrderNumber >= orderNumber
                                         ? wd.OrderNumber + 1
                                         : wd.OrderNumber);
                            }
                        );
                    pageService.Save(pageWidget.Page);
                    pageWidget.ColumnNumber = columnNumber;
                    pageWidget.OrderNumber = orderNumber;
                    pageWidgetService.Save(pageWidget);
                }
            }

        }

        /// <summary>
        /// Updates the pages positions.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="orderNumber">The order number.</param>
        public static void UpdatePagesPositions(long pageId, int orderNumber)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();

            var page = pageService.Find(pageId);
            if (page!=null && page.OrderNumber!=orderNumber)
            {
                var siblings = pageService.FindSiblingPages(page.ParentPageId);
                siblings.Update(pg=>
                                    {
                                         pg.OrderNumber =
                                            pg.OrderNumber > page.OrderNumber
                                                ? pg.OrderNumber - 1
                                                : pg.OrderNumber;
                                    });
                siblings.Update(pg =>
                {
                    pg.OrderNumber =
                       pg.OrderNumber >=orderNumber
                           ? pg.OrderNumber + 1
                           : pg.OrderNumber;
                });

                page.OrderNumber = orderNumber;
                pageService.Save(page);

                foreach (var sibling in siblings)
                {
                    if (sibling.Id!=page.Id)
                        pageService.Save(page);
                }
            }
        }

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void RemovePage(Page page)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();

            if (page != null)
            {
                var siblings = pageService.FindSiblingPages(page.ParentPageId);
                siblings.Update(pg =>
                {
                    pg.OrderNumber =
                       pg.OrderNumber > page.OrderNumber
                           ? pg.OrderNumber - 1
                           : pg.OrderNumber;
                });
              
                pageService.Delete(page);

                foreach (var sibling in siblings)
                {
                    if (sibling.Id != page.Id)
                        pageService.Save(sibling);
                }
            }
        }

        /// <summary>
        /// Gets the page holder styles.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static String GetPageHolderStyles(PageSettings settings)
        {
            StringBuilder builder = new StringBuilder();
            if (settings != null)
            {
                AppendStyleString(builder, "background-color", settings.LookAndFeelSettings.BackgroundColor);
                AppendStyleString(builder, "font-family", settings.LookAndFeelSettings.FontFamily);
                AppendStyleString(builder, "color", settings.LookAndFeelSettings.Color);
                if (settings.LookAndFeelSettings.FontSizeValue.HasValue && !String.IsNullOrEmpty(settings.LookAndFeelSettings.FontSizeUnit))
                {
                    builder.AppendFormat("font-size:{0}{1};", settings.LookAndFeelSettings.FontSizeValue, settings.LookAndFeelSettings.FontSizeUnit);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets the page inner holder styles.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static String GetPageInnerHolderStyles(PageSettings settings)
        {
            if(settings != null && !String.IsNullOrEmpty(settings.LookAndFeelSettings.OtherStyles))
            {
                return settings.LookAndFeelSettings.OtherStyles;
            }
            return String.Empty;
        }

        /// <summary>
        /// Appends the style string.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="styleName">Name of the style.</param>
        /// <param name="styleValue">The style value.</param>
        private static void AppendStyleString(StringBuilder builder, String styleName, String styleValue)
        {
            if (!String.IsNullOrEmpty(styleValue))
            {
                builder.AppendFormat("{0}:{1};", styleName, styleValue);
            }
        }

        /// <summary>
        /// Gets the navigation menu.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<NavigationMenuModel> GetNavigationMenu(PageViewModel currentPage,ICorePrincipal user)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();

            var pages = pageService.GetAllowedPagesByOperation(user,(int)PageOperations.View).OrderBy(page=>page.OrderNumber);

            var pagesToRemove = pageService.GetAllowedPagesByOperation(user, (int)PageOperations.Delete).OrderBy(page => page.OrderNumber);

            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            bool addNewPagesAccess = permissionService.IsAllowed((int) PageOperations.AddNewPages, user, typeof (Page), null,
                                                  PermissionOperationLevel.Type);

            List<NavigationMenuModel> items = pages.Select(page => new NavigationMenuModel { Page = page, IsCurrent = currentPage != null && page.Id == currentPage.Id, RemoveAccess = pagesToRemove.FirstOrDefault(item => item.Id == page.Id) != null }).ToList();

            foreach (var item in items)
            {
                if (item.Page.ParentPageId == null)
                {
                    item.Children = Flatten(item, items, 2, addNewPagesAccess);
                    yield return item;
                }
            }
            if (addNewPagesAccess)
                yield return new NavigationMenuModel();
        }

        public static List<NavigationMenuModel> Flatten(NavigationMenuModel root, List<NavigationMenuModel> items,int level, bool addNewPagesAccess)
        {
            var flattened = new List<NavigationMenuModel>{};

            var children = items.Where(item => item.Page.ParentPageId!=null && item.Page.ParentPageId == root.Page.Id).ToList();
           
            foreach (var child in children)
            {
                child.Children = Flatten(child, items, level + 1, addNewPagesAccess);
                flattened.Add(child);
            }

            if (level <= 3 && addNewPagesAccess)
                flattened.Add(new NavigationMenuModel { Parent = root });

            return flattened;
        }
    }
}