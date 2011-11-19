using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Linq;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Helpers;
using Core.Web.Helpers.Layouts;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Mvc.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    public partial class PagesController : FrameworkController
    {
        #region Fields

        private readonly IPageService pageService;

        private readonly IPermissionCommonService permissionService;

        private readonly IPermissionsHelper permissionHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesController"/> class.
        /// </summary>
        public PagesController()
        {
            pageService = ServiceLocator.Current.GetInstance<IPageService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether [is page owner] [the specified page].
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// 	<c>true</c> if [is page owner] [the specified page]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPageOwner(Page page)
        {
            return page != null && this.CorePrincipal() != null && page.User != null &&
                             page.User.Id == this.CorePrincipal().PrincipalId;
        }

        #endregion

        #region Actions

        #region Common

        /// <summary>
        /// Select all pages.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult Index(PageViewModel currentPage)
        {
            var model = PageHelper.GetNavigationMenu(currentPage, this.CorePrincipal());

            if (model.MenuItems.Any())
            {
                return View(MVC.Pages.Views.NavigationMenu, model);
            }
            return Content(String.Empty);
        }

        /// <summary>
        /// Shows page details.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Page details.</returns>
        [HttpGet]
        public virtual ActionResult Show(String url)
        {
            var page = pageService.FindByUrl(url);

            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.View, this.CorePrincipal(), typeof(Page), page.Id, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            return View(PageHelper.BindPageViewModel(page, this.CorePrincipal()));
        }

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult RemovePage(long pageId)
        {
            var page = pageService.Find(pageId);

            if (page == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            if (permissionService.IsAllowed((Int32)PageOperations.Delete, this.CorePrincipal(), typeof(Page), page.Id, IsPageOwner(page), PermissionOperationLevel.Object))
                PageHelper.RemovePage(page);

            return Content(String.Empty);
        }

        /// <summary>
        /// Creates the new page.
        /// </summary>
        /// <param name="parentPageId">The parent page id.</param>
        /// <returns></returns>
        public virtual ActionResult CreateNewPage(long? parentPageId)
        {
            if (!permissionService.IsAllowed((Int32)PageOperations.AddNewPages, this.CorePrincipal(), typeof(Page), null))
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, Translate("Messages.NotFound"));
            }

            return PartialView(MVC.Pages.Views.PageCreateForm, PageHelper.BindPageViewModel(
                new Page { ParentPageId = parentPageId == 0 ? null : parentPageId }, this.CorePrincipal()));
        }

        [HttpPost]
        public virtual ActionResult UpdatePagePosition(long pageId, int orderNumber)
        {
            if (permissionService.IsAllowed((Int32)PageOperations.AddNewPages, this.CorePrincipal(), typeof(Page), null))
            {
                PageHelper.UpdatePagesPositions(pageId, orderNumber);
            }
            return null;
        }

        #endregion

        #region Layouts

        [HttpPost]
        public virtual ActionResult ChangePageMode(PageMode pageMode)
        {
            PageHelper.ChangePageMode(pageMode);

            return null;
        }

        /// <summary>
        /// Changes the layout.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="layoutTemplateId">The layout template id.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ChangeLayout(long pageId, long layoutTemplateId)
        {
            var page = pageService.Find(pageId);
            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            var pageLayoutTemplateService =
                  ServiceLocator.Current.GetInstance<IPageLayoutTemplateService>();
            PageLayoutTemplate layoutTemplate = pageLayoutTemplateService.Find(layoutTemplateId);
            if (layoutTemplate == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }
            LayoutHelper.ChangePageLayout(page, page.PageLayout.LayoutTemplate, layoutTemplate);

            return PartialView(MVC.Shared.Views.Layouts.Layout, PageHelper.BindPageViewModel(page, this.CorePrincipal()));
        }


        /// <summary>
        /// Shows the change layout form.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowChangeLayoutForm(long pageId)
        {
            var page = pageService.Find(pageId);

            if (page != null && permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                var pageLayoutTemplateService =
                    ServiceLocator.Current.GetInstance<IPageLayoutTemplateService>();

                return PartialView(MVC.Shared.Views.Layouts.ChangeLayout,
                              new ChangeLayoutModel { PageId = pageId, CurrentLayout = page.PageLayout, Layouts = pageLayoutTemplateService.GetAll() });
            }

            return null;
        }

        [HttpPost]
        public virtual ActionResult ShowLayoutSettingsForm(long pageId)
        {
            var page = pageService.Find(pageId);
            if (page != null && permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                var pageLayoutTemplateService =
                    ServiceLocator.Current.GetInstance<IPageLayoutTemplateService>();

                return PartialView(MVC.Shared.Views.Layouts.LayoutSettings,
                              new ChangeLayoutModel { PageId = pageId, CurrentLayout = page.PageLayout, Layouts = pageLayoutTemplateService.GetAll() });
            }

            return null;
        }

        [HttpPost]
        public virtual ActionResult UpdateLayoutSettingsForm(LayoutSettingsModel layoutSettings)
        {
            bool isSuccessed = true;
            if (ModelState.IsValid)
            {
                var pageLayoutService = ServiceLocator.Current.GetInstance<IPageLayoutService>();
                var pageLayoutRowService = ServiceLocator.Current.GetInstance<IPageLayoutRowService>();
                var columnWidthValueService = ServiceLocator.Current.GetInstance<IPageLayoutColumnWidthValueService>();
                PageLayout pageLayout = pageLayoutService.Find(layoutSettings.LayoutId);

                //check page permissions
                if (pageLayout == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageLayout.Page.Id, IsPageOwner(pageLayout.Page), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                }

                foreach (RowSettings rowSettings in layoutSettings.RowsSetting)
                {
                    PageLayoutRow row = pageLayoutRowService.Find(rowSettings.RowId);
                    if (row == null)
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                    }
                    if (row.Columns.Count() != rowSettings.ColumnsWidth.Count)
                    {
                        throw new HttpException((int)HttpStatusCode.BadRequest, Translate("Messages.BadRequest"));
                    }
                    int columnIndex = 0;
                    foreach (PageLayoutColumn column in row.Columns)
                    {
                        PageLayoutColumnWidthValue columnWidthValue = pageLayout.ColumnWidths.FirstOrDefault(columnWidths => columnWidths.Column == column);
                        if (columnWidthValue == null)
                        {
                            columnWidthValue = new PageLayoutColumnWidthValue
                            {
                                Column = column,
                                PageLayout = pageLayout,

                            };
                        }
                        columnWidthValue.WidthValue = rowSettings.ColumnsWidth[columnIndex++];
                        isSuccessed = isSuccessed && columnWidthValueService.Save(columnWidthValue);
                    }
                }
            }
            return Json(new { IsSuccessed = isSuccessed });
        }

        #endregion

        #region Page Settings

        /// <summary>
        /// Shows the page look and feel.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowPageLookAndFeel(long pageId)
        {
            Page page = pageService.Find(pageId);

            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), page.Id, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            PageSettings pageSetting = page.Settings ?? new PageSettings { Page = page };

            return PartialView(MVC.Pages.Views.PageLookAndFeelSettings, new PageLookAndFeelModel().MapFrom(pageSetting));
        }

        /// <summary>
        /// Updates the page look and feel.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult UpdatePageLookAndFeel(PageLookAndFeelModel model)
        {
            bool isSuccessed = false;
            if (ModelState.IsValid)
            {
                Page page = pageService.Find(model.PageId);
                if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), model.PageId, IsPageOwner(page), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                }

                var pageSettingService = ServiceLocator.Current.GetInstance<IPageSettingService>();
                PageSettings pageSetting = model.MapTo(new PageSettings
                                                        {
                                                            Id = model.SettingId,
                                                            Page = new Page { Id = model.PageId }
                                                        });
                isSuccessed = pageSettingService.Save(pageSetting);
                model.SettingId = pageSetting.Id;
            }
            if (isSuccessed)
                Success(Translate("Messages.Success"));
            else
                Error(Translate("Messages.UnknownError"));
            return PartialView(MVC.Pages.Views.PageLookAndFeelForm, model);
        }

        /// <summary>
        /// Shows the available widgets.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="isTemplate">if set to <c>true</c> [is template].</param>
        /// <returns></returns>
        public virtual ActionResult ShowAvailableWidgets(long pageId, bool isTemplate)
        {
            ViewData["pageId"] = pageId;
            var allowedWidgets = WidgetHelper.GetAvailableWidgets(isTemplate);

            return PartialView(MVC.Shared.Views.Widgets.WidgetsList, allowedWidgets);
        }

        /// <summary>
        /// Adds the widget.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="widgetId">The widget id.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult AddWidget(long pageId, long widgetId)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            Page currentPage = pageService.Find(pageId);
            if (currentPage != null)
            {
                var curWidget = widgetService.Find(widgetId);
                
                if (curWidget != null && widgetService.IsWidgetEnable(curWidget) && permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page),
                                                pageId, IsPageOwner(currentPage), PermissionOperationLevel.Object))
                {
                    ICoreWidget coreWidget =
                            MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == curWidget.Identifier);

                    if (coreWidget != null && coreWidget is BaseWidget && permissionService.IsAllowed(((BaseWidget)coreWidget).AddToPageOperationCode,
                            this.CorePrincipal(), coreWidget.GetType(), null))
                    {
                        var widget = PageHelper.AddWidgetToPage(pageId, widgetId, this.CorePrincipal(), null);
                        if (widget != null)
                        {

                            permissionService.SetupDefaultRolePermissions(
                                ResourcePermissionsHelper.GetResourceOperations(coreWidget.GetType()),
                                coreWidget.GetType(), widget.Id);
                            if(currentPage.IsTemplate && widget.Widget.IsPlaceHolder)
                            {
                                IEnumerable<Page> pagesFromTemplate = pageService.GetPagesFromTemplate(currentPage);
                                foreach (var pageFromTemplate in pagesFromTemplate)
                                {
                                    var widgetFromTemplate = PageHelper.AddWidgetToPage(pageFromTemplate.Id, widgetId, this.CorePrincipal(), widget.Id);
                                    if (widgetFromTemplate != null)
                                    {

                                        permissionService.SetupDefaultRolePermissions(
                                            ResourcePermissionsHelper.GetResourceOperations(coreWidget.GetType()),
                                            coreWidget.GetType(), widgetFromTemplate.Id);
                                    }
                                }
                            }

                            return PartialView(MVC.Shared.Views.Widgets.WidgetContentHolder,
                                               WidgetHelper.GetWidgetViewModel(widget));
                        }
                    }
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates the page widget instance.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <param name="instanceId">The instance id.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdatePageWidgetInstance(long pageWidgetId, long instanceId)
        {
            //check if user has privileges to manage current page and manage current widget
            //check permissions inside UpdatePageWidgetInstance method
            WidgetHelper.UpdatePageWidgetInstance(pageWidgetId, instanceId, this.CorePrincipal());

            return PartialView(MVC.Shared.Views.Widgets.WidgetContentHolder, WidgetHelper.GetWidgetViewModel(pageWidgetId));
        }

        /// <summary>
        /// Removes the page widget.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult RemovePageWidget(long pageWidgetId)
        {
            //check if user has privileges to manage current page and manage current widget
            //check permissions inside RemoveWidgetFromPage method
            PageHelper.RemoveWidgetFromPage(pageWidgetId, this.CorePrincipal());
            return null;
        }

        /// <summary>
        /// Updates the widgets positions.
        /// </summary>
        /// <param name="widgetId">The widget id.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="orderNumber">The order number.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidgetsPositions(long widgetId, int pageSection, int columnNumber, int orderNumber)
        {
            //check permissions inside UpdateWidgetsPositions method
            PageHelper.UpdateWidgetsPositions(widgetId, pageSection, columnNumber, orderNumber, this.CorePrincipal());
            return null;
        }

        /// <summary>
        /// Shows the page common settings.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowPageCommonSettings(long pageId)
        {
            var page = pageService.Find(pageId);

            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            return PartialView(MVC.Pages.Views.PageCommonSettings, new PageLocaleViewModel().MapFrom(page));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long pageId, String culture)
        {
            var page = pageService.Find(pageId);
            if (page == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }
            var localeService = ServiceLocator.Current.GetInstance<IPageLocaleService>();
            PageLocale locale = localeService.GetLocale(pageId, culture);
            String pageTitle = locale != null ? locale.Title : page.Title;
            var serializer = new JavaScriptSerializer();
            return Content(serializer.Serialize(new { Title = pageTitle, Culture = culture }));
        }

        /// <summary>
        /// Updates the page common settings.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SavePageCommonSettings(PageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var page = new Page();
                if (!permissionService.IsAllowed((Int32)PageOperations.AddNewPages, this.CorePrincipal(), typeof(Page), null))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                }

                page.OrderNumber = pageService.GetLastOrderNumber(model.ParentPageId == 0 ? null : model.ParentPageId);
                if (this.CorePrincipal() != null)
                    page.User = new User
                    {
                        Id = this.CorePrincipal().PrincipalId
                    };
                page = model.MapTo(page);
                if (!model.TemplateId.HasValue)
                {
                    page.PageLayout = new PageLayout
                                          {
                                              LayoutTemplate = LayoutHelper.DefaultLayoutTemplate,
                                              Page = page
                                          };
                }
                if (pageService.Save(page))
                {
                    permissionService.SetupDefaultRolePermissions(
                        ResourcePermissionsHelper.GetResourceOperations(typeof(Page)), typeof(Page), page.Id);
                    if (model.ClonedPageId != null)
                    {
                        var sourcePage = pageService.Find((long)model.ClonedPageId);
                        if (permissionService.IsAllowed((Int32)PageOperations.View, this.CorePrincipal(),
                                                        typeof(Page), sourcePage.Id, IsPageOwner(sourcePage),
                                                        PermissionOperationLevel.Object))
                        {
                            PageHelper.ClonePageSettings(sourcePage, page);
                        }
                    }
                    else if(model.TemplateId.HasValue)
                    {
                        var sourceTemplate = pageService.Find((long)model.TemplateId);
                        if (permissionService.IsAllowed((Int32)PageOperations.View, this.CorePrincipal(),
                                                        typeof(Page), sourceTemplate.Id, IsPageOwner(sourceTemplate),
                                                        PermissionOperationLevel.Object))
                        {
                            PageHelper.ClonePageSettingsFromTemplate(sourceTemplate, page, model.WidgetId);
                        }
                    }

                    TempData["Success"] = true;
                }

                model = model.MapFrom(page);
            }
            else Error(Translate("Messages.ValidationError"));

            return PartialView(MVC.Pages.Views.PageCreateForm, model);
        }

        /// <summary>
        /// Updates the page common settings.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdatePageCommonSettings(PageLocaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var page = new Page();
                page = pageService.Find(model.Id);
                if (!permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), model.Id, IsPageOwner(page), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                }
                var locales = new JavaScriptSerializer().Deserialize<IDictionary<String, String>>(model.LocalesString);
                var localeService = ServiceLocator.Current.GetInstance<IPageLocaleService>();
                IList<PageLocale> pageLocales = localeService.GetLocales(page.Id);
                foreach (var locale in locales)
                {
                    PageLocale pageLocale = pageLocales.Where(pl => pl.Culture == locale.Key).FirstOrDefault();
                    if (pageLocale == null)
                    {
                        pageLocale = new PageLocale { Page = page, Culture = locale.Key };
                    }
                    pageLocale.Title = locale.Value;
                    localeService.Save(pageLocale);
                }
                if (pageService.Save(model.MapTo(page)))
                {
                    TempData["Success"] = true;
                }
                model = model.MapFrom(page);
            }
            else Error(Translate("Messages.ValidationError"));

            return PartialView(MVC.Pages.Views.PageCommonSettings, model);
        }

        /// <summary>
        /// Shows the page CSS.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowPageCSS(long pageId)
        {
            Page page = pageService.Find(pageId);

            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            PageSettings pageSetting = page.Settings ?? new PageSettings { Page = page };

            return PartialView(MVC.Pages.Views.PageCSSSettings, new PageCSSModel().MapFrom(pageSetting));
        }

        /// <summary>
        /// Updates the page CSS.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult UpdatePageCSS(PageCSSModel model)
        {
            bool isSuccessed = false;
            if (ModelState.IsValid)
            {
                Page page = pageService.Find(model.PageId);

                if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Update, this.CorePrincipal(), typeof(Page), model.PageId, IsPageOwner(page), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
                }

                var pageSettingService = ServiceLocator.Current.GetInstance<IPageSettingService>();
                PageSettings pageSettings = pageSettingService.Find(model.SettingId) ?? new PageSettings
                {
                    Id = model.SettingId,
                    Page = new Page { Id = model.PageId }
                };
                pageSettings = model.MapTo(pageSettings);
                isSuccessed = pageSettingService.Save(pageSettings);
                model.SettingId = pageSettings.Id;
            }

            if (isSuccessed)
                Success(Translate("Messages.Success"));
            else
                Error(Translate("Messages.UnknownError"));

            return PartialView(MVC.Pages.Views.PageCSSForm, model);
        }

        /// <summary>
        /// Shows the page CSS.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowPagePermissions(long pageId)
        {
            Page page = pageService.Find(pageId);

            if (page == null || !permissionService.IsAllowed((Int32)PageOperations.Permissions, this.CorePrincipal(), typeof(Page), pageId, IsPageOwner(page), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            return PartialView(MVC.Pages.Views.PagePermissions, permissionHelper.BindPermissionsModel(pageId, typeof(Page), false));
        }

        [HttpPost]
        public virtual ActionResult ApplyPagePermissions(PermissionsModel model)
        {
            Page page = pageService.Find(model.EntityId);

            if (page != null)
            {
                if (permissionService.IsAllowed((Int32)PageOperations.Permissions, this.CorePrincipal(), typeof(Page), page.Id, IsPageOwner(page), PermissionOperationLevel.Object))
                {
                    permissionHelper.ApplyPermissions(model, typeof(Page));
                }
                if (permissionService.IsAllowed((Int32)PageOperations.Permissions, this.CorePrincipal(), typeof(Page), page.Id, IsPageOwner(page), PermissionOperationLevel.Object))
                    return Content(Url.Action(MVC.Pages.Show(page.Url)));
            }

            return Content(Url.Action(MVC.Home.Index()));
        }

        #endregion

        #region Widget Settings

        /// <summary>
        /// Shows the settings.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult ShowSettings(long pageWidgetId)
        {
            return PartialView(MVC.Shared.Views.Widgets.WidgetSettings, WidgetHelper.GetWidgetViewModel(pageWidgetId));
        }

        /// <summary>
        /// Shows the look and feel.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowWidgetLookAndFeel(long pageWidgetId)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            PageWidget widget = widgetService.Find(pageWidgetId);
            PageWidgetSettings widgetSetting = widget.Settings ?? new PageWidgetSettings { Widget = widget };

            return PartialView(MVC.Shared.Views.Widgets.WidgetLookAndFeelSettings, new WidgetLookAndFeelModel().MapFrom(widgetSetting));
        }

        /// <summary>
        /// Updates the widget look and feel.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult UpdateWidgetLookAndFeel(WidgetLookAndFeelModel model)
        {
            bool isSuccessed = false;
            if (ModelState.IsValid)
            {
                var widgetSettingService = ServiceLocator.Current.GetInstance<IPageWidgetSettingService>();
                PageWidgetSettings widgetSetting = widgetSettingService.Find(model.SettingId) ??
                                                   new PageWidgetSettings
                                                   {
                                                       Id = model.SettingId,
                                                       Widget = new PageWidget { Id = model.WidgetId }
                                                   };
                widgetSetting = model.MapTo(widgetSetting);
                isSuccessed = widgetSettingService.Save(widgetSetting);
                model.SettingId = widgetSetting.Id;
            }
            if (isSuccessed)
                Success(Translate("Messages.Success"));
            else
                Error(Translate("Messages.UnknownError"));

            return PartialView(MVC.Shared.Views.Widgets.WidgetLookAndFeelForm, model);
        }

        public virtual ActionResult ShowWidgetCSS(long pageWidgetId)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            PageWidget widget = widgetService.Find(pageWidgetId);
            PageWidgetSettings widgetSetting = widget.Settings ?? new PageWidgetSettings { Widget = widget };

            return PartialView(MVC.Shared.Views.Widgets.WidgetCSSSettings, new WidgetCSSModel().MapFrom(widgetSetting));
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult UpdateWidgetCSS(WidgetCSSModel model)
        {
            bool isSuccessed = false;
            if (ModelState.IsValid)
            {
                var widgetSettingService = ServiceLocator.Current.GetInstance<IPageWidgetSettingService>();
                PageWidgetSettings widgetSetting = widgetSettingService.Find(model.SettingId) ??
                                                   new PageWidgetSettings
                                                       {
                                                           Id = model.SettingId,
                                                           Widget = new PageWidget { Id = model.WidgetId }
                                                       };
                widgetSetting = model.MapTo(widgetSetting);
                isSuccessed = widgetSettingService.Save(widgetSetting);
                model.SettingId = widgetSetting.Id;
                var pageSettingService = ServiceLocator.Current.GetInstance<IPageSettingService>();
                PageSettings pageSettings = pageSettingService.Find(model.PageCssModel.SettingId) ?? new PageSettings
                                                                                                         {
                                                                                                             Id = model.PageCssModel.SettingId,
                                                                                                             Page = new Page { Id = model.PageCssModel.PageId }
                                                                                                         };
                pageSettings = model.PageCssModel.MapTo(pageSettings);
                isSuccessed = isSuccessed && pageSettingService.Save(pageSettings);
                model.PageCssModel.SettingId = pageSettings.Id;
            }
            if (isSuccessed)
                Success(Translate("Messages.Success"));
            else
                Error(Translate("Messages.UnknownError"));

            return PartialView(MVC.Shared.Views.Widgets.WidgetCSSForm, model);
        }

        /// <summary>
        /// Shows the widget permissions.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <returns></returns>
        public virtual ActionResult ShowWidgetPermissions(long pageWidgetId)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            PageWidget pageWidget = widgetService.Find(pageWidgetId);
            if (pageWidget == null || pageWidget.Widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }
            ICoreWidget coreWidget =
                MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == pageWidget.Widget.Identifier);
            ICorePrincipal currentPrincipal = this.CorePrincipal();
            bool isOwner = currentPrincipal != null && pageWidget.User != null &&
                           currentPrincipal.PrincipalId == pageWidget.User.PrincipalId;

            if (coreWidget == null || !(coreWidget is BaseWidget) || !permissionService.IsAllowed(((BaseWidget)coreWidget).PermissionOperationCode, currentPrincipal, coreWidget.GetType(), pageWidgetId, isOwner, PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }
            return PartialView(MVC.Shared.Views.Widgets.WidgetPermissions, permissionHelper.BindPermissionsModel(pageWidgetId, coreWidget.GetType(), true));
        }

        [HttpPost]
        public virtual ActionResult ApplyWidgetPermissions(PermissionsModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            PageWidget pageWidget = widgetService.Find(model.EntityId);
            if (pageWidget != null && pageWidget.Widget != null)
            {
                ICoreWidget coreWidget =
                    MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == pageWidget.Widget.Identifier);
                ICorePrincipal currentPrincipal = this.CorePrincipal();
                bool isOwner = currentPrincipal != null && pageWidget.User != null &&
                               currentPrincipal.PrincipalId == pageWidget.User.PrincipalId;

                if (coreWidget != null && coreWidget is BaseWidget && permissionService.IsAllowed(((BaseWidget)coreWidget).PermissionOperationCode, currentPrincipal, coreWidget.GetType(), pageWidget.Id, isOwner, PermissionOperationLevel.Object))
                {
                    permissionHelper.ApplyPermissions(model, coreWidget.GetType());

                    if (permissionService.IsAllowed(((BaseWidget)coreWidget).PermissionOperationCode, currentPrincipal, coreWidget.GetType(), pageWidget.Id, isOwner, PermissionOperationLevel.Object))
                    {
                        return Content(Url.Action(MVC.Pages.Show(pageWidget.Page.Url)));
                    }
                }
            }

            return Content(Url.Action(MVC.Home.Index()));
        }

        #endregion

        #endregion
    }
}
