using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using Core.Framework.MEF.Extensions;

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

        public bool HideInMainMenu { get; set; }

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
            Layout = from.PageLayout;
            Id = from.Id;
            Settings = from.Settings;
            ParentPageId = from.ParentPageId;
            var pageAccess = permissionService.GetAccess(from.Operations, HttpContext.Current.CorePrincipal(), typeof(Page), from.Id, IsPageOwner);
            Access = pageAccess;
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();
            PageMode = PageHelper.CurrentUserPageMode;

            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();

            foreach (var widget in from.Widgets.OrderBy(wd => wd.OrderNumber))
            {
                PageWidget widget1 = widget;
                ICoreWidget coreWidget =
                    widget.Widget!=null?MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == widget1.Widget.Identifier):null;

                bool isWidetEnabled = widgetService.IsWidgetEnable(widget1.Widget);

                Widgets.Add(new WidgetHolderViewModel
                                {
                                    WidgetInstance = new CoreWidgetInstance
                                                         {
                                                             InstanceId = widget.InstanceId,
                                                             WidgetIdentifier = coreWidget != null?coreWidget.Identifier:null,
                                                             PageSettings = new CorePageSettings {PageId = from.Id}
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
                                    PageAccess = pageAccess,
                                    SystemWidget = (isWidetEnabled && coreWidget != null) ? coreWidget : null
                });

                if (coreWidget!=null && !PagePlugins.Any(t => t.PluginLocation == coreWidget.Plugin.PluginLocation))
                {
                    PagePlugins.Add(coreWidget.Plugin);
                }
            }
            var plugins = ServiceLocator.Current.GetInstance<IPluginService>().FindPluginsByIdentifiers(PagePlugins.Select(t => t.Identifier).ToList());
            if (plugins.Any())
            {
                plugins.ForEach(t => { CssFileName += t.Id + "_"; });
                CssFileName = CssFileName.Remove(CssFileName.Length - 1);
            }

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

        #endregion
    }
}