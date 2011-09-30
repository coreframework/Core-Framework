using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

namespace Core.Web.Helpers
{
    public static class PageHelper
    {
        #region Fields

        private const String PageWidgetTemplate = "#widget_{0}";

        #endregion

        #region Properties

        public static PageMode CurrentUserPageMode
        {
            get
            {
                return (PageMode)HttpContext.Current.Items[typeof(PageMode)];
            }
        }

        #endregion

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
        /// <param name="widgetId">The widget id.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static PageWidget AddWidgetToPage(long pageId, long widgetId, ICorePrincipal user)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();

            var page = pageService.Find(pageId);

            if (page != null)
            {
                Widget widget = widgetService.Find(widgetId);
                if (widget != null && widgetService.IsWidgetEnable(widget))
                {
                    page.Widgets.Update(
                        wd =>
                            {
                                wd.OrderNumber =
                                    wd.ColumnNumber == 1
                                         ? wd.OrderNumber + 1
                                         : wd.OrderNumber;
                            }
                        );

                    pageService.Save(page);

                    var newPageWidget = new PageWidget
                                            {
                                                Page = page,
                                                ColumnNumber = 1,
                                                OrderNumber = 1,
                                                User =
                                                    user != null ? new User { Id = user.PrincipalId } : null,
                                                Widget = widget
                                            };

                    if (pageWidgetService.Save(newPageWidget))
                        return newPageWidget;
                }
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
                              wd.ColumnNumber == pageWidget.ColumnNumber && wd.OrderNumber > pageWidget.OrderNumber ?

                                  wd.OrderNumber - 1 :
                                  wd.OrderNumber;
                        }
                    );

                    page.RemoveWidget(pageWidget);
                    if (pageService.Save(page))
                    {
                        var currentWidget = MvcApplication.Widgets.FirstOrDefault(widget => widget.Identifier == pageWidget.Widget.Identifier);

                        if (currentWidget!=null)
                        {
                            currentWidget.Remove(new CoreWidgetInstance{InstanceId = pageWidget.InstanceId});
                        }
                    }
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
        public static void UpdateWidgetsPositions(long pageWidgetId, int pageSection, int columnNumber, int orderNumber, ICorePrincipal user)
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
                                    wd.PageSection == pageWidget.PageSection && wd.ColumnNumber == pageWidget.ColumnNumber &&
                                     wd.OrderNumber > pageWidget.OrderNumber
                                         ? wd.OrderNumber - 1
                                         : wd.OrderNumber;
                            }
                        );
                    pageWidget.Page.Widgets.Update(
                        wd =>
                            {
                                wd.OrderNumber =
                                    (int)wd.PageSection == pageSection && wd.ColumnNumber == columnNumber && wd.OrderNumber >= orderNumber
                                         ? wd.OrderNumber + 1
                                         : wd.OrderNumber;
                            }
                        );
                    pageService.Save(pageWidget.Page);
                    pageWidget.PageSection = (PageSection)pageSection;
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
        public static NavigationMenuModel GetNavigationMenu(PageViewModel currentPage,ICorePrincipal user)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var pages = pageService.GetAllowedPagesByOperation(user,(int)PageOperations.View).OrderBy(page=>page.OrderNumber);
            var pagesToRemove = pageService.GetAllowedPagesByOperation(user, (int)PageOperations.Delete).OrderBy(page => page.OrderNumber);
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var pageMode = CurrentUserPageMode;
            var menuItems = new List<NavigationMenuItemModel>();

            bool addNewPagesAccess = permissionService.IsAllowed((int) PageOperations.AddNewPages, user, typeof (Page), null,
                                                  PermissionOperationLevel.Type);

            List<NavigationMenuItemModel> items = pages.Select(page => new NavigationMenuItemModel
                                                                       {
                                                                           Page = page,
                                                                           IsCurrent = currentPage != null && page.Id == currentPage.Id,
                                                                           RemoveAccess = pagesToRemove.FirstOrDefault(item => item.Id == page.Id) != null,
                                                                           PageMode = pageMode
                                                                       }).ToList();

            foreach (var item in items)
            {
                if (item.Page.ParentPageId == null)
                {
                    item.Children = Flatten(item, items, 2, addNewPagesAccess, pageMode);
                    menuItems.Add(item);
                }
            }
            if (addNewPagesAccess && pageMode==PageMode.Edit)
                menuItems.Add(new NavigationMenuItemModel
                                  {
                                      PageMode = pageMode
                                  });

            return new NavigationMenuModel
                       {
                           MenuItems = menuItems,
                           ManageAccess = addNewPagesAccess,
                           PageMode = pageMode
                       };
        }

        public static List<NavigationMenuItemModel> Flatten(NavigationMenuItemModel root, List<NavigationMenuItemModel> items,int level, bool addNewPagesAccess, PageMode pageMode)
        {
            var flattened = new List<NavigationMenuItemModel>{};

            var children = items.Where(item => item.Page.ParentPageId!=null && item.Page.ParentPageId == root.Page.Id).ToList();
           
            foreach (var child in children)
            {
                child.Children = Flatten(child, items, level + 1, addNewPagesAccess, pageMode);
                flattened.Add(child);
            }

            if (level <= 3 && addNewPagesAccess && pageMode == PageMode.Edit)
                flattened.Add(new NavigationMenuItemModel { Parent = root, PageMode = pageMode });

            return flattened;
        }

        public static void ChangePageMode(PageMode pageMode)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constants.PageModeCookieName, pageMode.ToString()));
            HttpContext.Current.Items[typeof (PageMode)] = pageMode;
        }

        /// <summary>
        /// Clones the target page settings to source page.
        /// </summary>
        /// <param name="sourcePage">The source page.</param>
        /// <param name="targetPage">The target page.</param>
        /// <returns></returns>
        public static bool ClonePageSettings(Page sourcePage, Page targetPage)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            sourcePage.Widgets.AsParallel().ForAll(widget =>
            {
                var pageWidget = new PageWidget();                        
                pageWidget.InjectFrom<CloneEntityInjection>(widget);
                pageWidget.Page = targetPage;
                pageWidget.ParentWidgetId = widget.Id;


                //copy widget settings
                if (widget.Settings != null)
                {
                    pageWidget.Settings = (PageWidgetSettings)new PageWidgetSettings().InjectFrom<CloneEntityInjection>(widget.Settings);
                    pageWidget.Settings.LookAndFeelSettings = new LookAndFeelSettings().InjectFrom<CloneEntityInjection>(widget.Settings.LookAndFeelSettings) as LookAndFeelSettings;
                    pageWidget.Settings.Widget = pageWidget;
                }
               
                //clone page widget instance
                if (widget.InstanceId!=null)
                {
                    pageWidget.InstanceId = CloneWidgetInstance(widget);
                }
                targetPage.AddWidget(pageWidget);
            });

            //copy page layout
            targetPage.PageLayout = (PageLayout)new PageLayout{Id = targetPage.PageLayout.Id}.InjectFrom<CloneEntityInjection>(sourcePage.PageLayout);
            targetPage.PageLayout.Page = targetPage;
            sourcePage.PageLayout.ColumnWidths.AsParallel().ForAll(column =>
                                                                       {
                                                                           var columnWidth = (PageLayoutColumnWidthValue)new PageLayoutColumnWidthValue().InjectFrom<CloneEntityInjection>(column);
                                                                           columnWidth.PageLayout = targetPage.PageLayout;
                                                                           targetPage.PageLayout.AddColumnWidth(columnWidth);
                                                                       });

            //copy page settings)
            if (sourcePage.Settings!=null)
            {
                targetPage.Settings = (PageSettings) new PageSettings().InjectFrom<CloneEntityInjection>(sourcePage.Settings);
                targetPage.Settings.LookAndFeelSettings = (LookAndFeelSettings)new LookAndFeelSettings().InjectFrom<CloneEntityInjection>(sourcePage.Settings.LookAndFeelSettings);
                targetPage.Settings.Page = targetPage;
            }
            
            if (pageService.Save(targetPage))
            {
                //copy permissions and update page styles
                foreach (var item in targetPage.Widgets)
                {
                    if (item.ParentWidgetId == null) continue;
                    var systemWidget = MvcApplication.Widgets.FirstOrDefault(w => w.Identifier == item.Widget.Identifier);
                    var sourceWidget = sourcePage.Widgets.FirstOrDefault(w => w.Id == item.ParentWidgetId);
                    if (sourceWidget!=null)
                    {
                        if (targetPage.Settings!=null && !String.IsNullOrEmpty(targetPage.Settings.CustomCSS))
                        {
                            targetPage.Settings.CustomCSS = targetPage.Settings.CustomCSS.Replace(String.Format(PageWidgetTemplate, sourceWidget.Id), String.Format(PageWidgetTemplate, item.Id));
                        }
                        permissionCommonService.CloneObjectPermisions(systemWidget.GetType(), sourceWidget.Id, item.Id);
                    }
                }
                return pageService.Save(targetPage);
            }
            return false;
        }

        /// <summary>
        /// Clones the widget instance.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <returns></returns>
        private static long? CloneWidgetInstance(PageWidget widget)
        {
            var systemWidget = MvcApplication.Widgets.FirstOrDefault(w => w.Identifier == widget.Widget.Identifier);
            if (widget.InstanceId!=null)
            {
                return systemWidget.Clone(new CoreWidgetInstance { InstanceId = widget.InstanceId, WidgetIdentifier = systemWidget.Identifier });
            }
            return null;
        }
    }
}