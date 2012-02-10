using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Extensions;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Models
{
    /// <summary>
    /// Describes Page Model.
    /// </summary>
    public class PageViewModel : IMappedModel<Page, PageViewModel>
    {
        #region Fields

        private List<WidgetHolderViewModel> widgets = new List<WidgetHolderViewModel>();

        private readonly IPermissionCommonService permissionService;

        private List<Page> availablePages;

        private List<Page> availableTemplates;

        private IList<Widget> availableWidgets;

        #endregion

        #region Constructor

        public PageViewModel()
        {
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            PagePlugins = new List<ICorePlugin>();
        }

        #endregion

        #region Properties

        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [Required]
        public String Url { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>The widgets.</value>
        public List<WidgetHolderViewModel> Widgets
        {
            get { return widgets; }
            set { widgets = value; }
        }

        /// <summary>
        /// Gets or sets the page layout.
        /// </summary>
        /// <value>The page layout.</value>
        public PageLayout Layout { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public PageSettings Settings { get; set; }

        public long? ParentPageId { get; set; }

        public Int32 OrderNumber { get; set; }

        public Dictionary<Int32, bool> Access { get; set; }

        public bool IsPageOwner { get; set; }

        public PageMode PageMode { get; set; }

        public String CssFileName { get; set; }

        public List<ICorePlugin> PagePlugins { get; set; }

        public List<Page> AvailablePages
        {
            get
            {
                if (availablePages == null)
                {
                    var pageService = ServiceLocator.Current.GetInstance<IPageService>();
                    availablePages = (List<Page>)pageService.GetAllowedPagesByOperation(HttpContext.Current.User as ICorePrincipal, (Int32)PageOperations.View);
                }
                return availablePages;
            }
        }

        public long? ClonedPageId { get; set; }

        public List<Page> AvailableTemplates
        {
            get
            {
                if (availableTemplates == null)
                {
                    var pageService = ServiceLocator.Current.GetInstance<IPageService>();
                    availableTemplates = (List<Page>)pageService.GetAllowedPageTemplatesByOperation(HttpContext.Current.User as ICorePrincipal, (Int32)PageOperations.View);
                }
                return availableTemplates;
            }
        }

        public long? TemplateId { get; set; }

        public bool HideInMainMenu { get; set; }

        public bool IsTemplate { get; set; }

        public bool HasTemplate { get; set; }

        public IList<Widget> AvailableWidgets
        {
            get
            {
                if (availableWidgets == null)
                {
                    availableWidgets = WidgetHelper.GetAvailableWidgets(false);
                }
                return availableWidgets;
            }
        }

        public long? WidgetId { get; set; }

        public bool IsServicePage { get; set; }

        #endregion

        #region IMappedModel members

        /// <summary>
        /// Maps from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public PageViewModel MapFrom(Page from)
        {
            Title = from.Title;
            Url = from.Url;
            HideInMainMenu = from.HideInMainMenu;
            Id = from.Id;
            ParentPageId = from.ParentPageId;
            var pageAccess = permissionService.GetAccess(from.Operations, HttpContext.Current.CorePrincipal(), typeof(Page), from.Id, IsPageOwner);
            Access = pageAccess;
            PageMode = PageHelper.CurrentUserPageMode;
            if(from.Template == null)
            {
                MapFromPage(from);
            }
            else
            {
                MapFromTemplate(from);
            }
            IsTemplate = from.IsTemplate && from.Template == null;
            var plugins = ServiceLocator.Current.GetInstance<IPluginService>().FindPluginsByIdentifiers(PagePlugins.Select(t => t.Identifier).ToList());
            if (plugins.Any())
            {
                plugins.ForEach(t => { CssFileName += t.Id + "_"; });
                CssFileName = CssFileName.Remove(CssFileName.Length - 1);
            }
            IsServicePage = from.IsServicePage;

            return this;
        }

        /// <summary>
        /// Maps to.
        /// </summary>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public Page MapTo(Page to)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            to.Title = Title;
            to.Url = Url = urlHelper.EncodeForSEO(Url);
            to.HideInMainMenu = HideInMainMenu;
            to.ParentPageId = ParentPageId == 0 ? null : ParentPageId;

            return to;
        }

        private void MapFromPage(Page page)
        {
            Layout = page.PageLayout;
            Settings = page.Settings;
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            foreach (var widget in page.Widgets.OrderBy(wd => wd.OrderNumber))
            {
                PageWidget widget1 = widget;
                ICoreWidget coreWidget =
                    widget.Widget != null ? MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == widget1.Widget.Identifier) : null;

                bool isWidetEnabled = widgetService.IsWidgetEnable(widget1.Widget);

                var widgetModel = new WidgetHolderViewModel
                                      {
                                          WidgetInstance = new CoreWidgetInstance
                                                               {
                                                                   InstanceId = widget.InstanceId,
                                                                   WidgetIdentifier =
                                                                       coreWidget != null ? coreWidget.Identifier : null,
                                                                   PageSettings =
                                                                       new CorePageSettings {PageId = page.Id},
                                                                   PageWidgetId = widget.Id
                                                               },
                                          Widget = widget1,

                                          Access = coreWidget is BaseWidget
                                                       ? permissionService.GetAccess(
                                                           ((BaseWidget) coreWidget).Operations,
                                                           HttpContext.Current.CorePrincipal(), coreWidget.GetType(),
                                                           widget1.EntityId,
                                                           currentPrincipal != null && widget1.User != null &&
                                                           widget1.User.PrincipalId == currentPrincipal.PrincipalId)
                                                       : null,
                                          PageAccess = Access,
                                          SystemWidget = (isWidetEnabled && coreWidget != null) ? coreWidget : null
                                      };
                if (page.IsTemplate && widget.Widget.IsPlaceHolder)
                {
                    widgetModel.Access[((BaseWidget)widgetModel.SystemWidget).PermissionOperationCode] = false;
                }
                Widgets.Add(widgetModel);

                if (coreWidget != null && coreWidget.Plugin != null)
                {
                    if (!PagePlugins.Any(t => t.PluginLocation == coreWidget.Plugin.PluginLocation))
                    {
                        PagePlugins.Add(coreWidget.Plugin);
                    }
                    if(coreWidget is BaseWorkflowWidget)
                    {
                        var workflowWidget = (BaseWorkflowWidget)coreWidget;
                        foreach(var innerPlugin in workflowWidget.InnerPlugins)
                        {
                            ICorePlugin plugin = innerPlugin;
                            if(!PagePlugins.Any(t => t.Identifier == plugin.Identifier))
                            {
                                PagePlugins.Add(plugin);
                            }
                        }
                    }
                }
            }
        }

        private void MapFromTemplate(Page page)
        {
            HasTemplate = true;
            Layout = page.Template.PageLayout;
            Settings = page.Template.Settings;
            Access[PageTemplate.UnlinkOperationCode] = Access[(int)PageOperations.Update];
            Access[(int) PageOperations.Update] = false;
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            foreach (var widget in page.Template.Widgets.OrderBy(wd => wd.OrderNumber))
            {
                var widgetToShow = !widget.Widget.IsPlaceHolder
                                       ? widget
                                       : page.Widgets.Where(pageWidget => pageWidget.TemplateWidgetId == widget.Id).
                                             FirstOrDefault();
                if(widget.Widget.IsPlaceHolder)
                {
                    widgetToShow.PageSection = widget.PageSection;
                    widgetToShow.ColumnNumber = widget.ColumnNumber;
                }
                PageWidget widget1 = widgetToShow;
                ICoreWidget coreWidget =
                    widgetToShow.Widget != null ? MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == widget1.Widget.Identifier) : null;

                bool isWidetEnabled = widgetService.IsWidgetEnable(widget1.Widget);
                var widgetModel = new WidgetHolderViewModel
                                      {
                                          WidgetInstance = new CoreWidgetInstance
                                                               {
                                                                   InstanceId = widgetToShow.InstanceId,
                                                                   WidgetIdentifier =
                                                                       coreWidget != null ? coreWidget.Identifier : null,
                                                                   PageSettings =
                                                                       new CorePageSettings { PageId = page.Template.Id },
                                                                   PageWidgetId = widgetToShow.Id
                                                               },
                                          Widget = widget1,
                                          Access = coreWidget is BaseWidget
                                                       ? permissionService.GetAccess(
                                                           ((BaseWidget) coreWidget).Operations,
                                                           HttpContext.Current.CorePrincipal(), coreWidget.GetType(),
                                                           widget1.EntityId,
                                                           currentPrincipal != null && widget1.User != null &&
                                                           widget1.User.PrincipalId == currentPrincipal.PrincipalId)
                                                       : null,
                                          PageAccess = Access,
                                          SystemWidget = (isWidetEnabled && coreWidget != null) ? coreWidget : null
                                      };
                if (!widget.Widget.IsPlaceHolder)
                {
                    widgetModel.Access[((BaseWidget) widgetModel.SystemWidget).ManageOperationCode] = false;
                    widgetModel.Access[((BaseWidget) widgetModel.SystemWidget).PermissionOperationCode] = false;
                }
                Widgets.Add(widgetModel);

                if (coreWidget != null && coreWidget.Plugin != null)
                {
                    if (!PagePlugins.Any(t => t.PluginLocation == coreWidget.Plugin.PluginLocation))
                    {
                        PagePlugins.Add(coreWidget.Plugin);
                    }
                    if (coreWidget is BaseWorkflowWidget)
                    {
                        var workflowWidget = (BaseWorkflowWidget)coreWidget;
                        foreach (var innerPlugin in workflowWidget.InnerPlugins)
                        {
                            ICorePlugin plugin = innerPlugin;
                            if (!PagePlugins.Any(t => t.Identifier == plugin.Identifier))
                            {
                                PagePlugins.Add(plugin);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}