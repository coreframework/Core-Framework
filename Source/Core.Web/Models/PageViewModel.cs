using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Helpers;
using Core.Web.NHibernate.Models;
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

        private List<WidgetHolderViewModel> _widgets = new List<WidgetHolderViewModel>();

        private IPermissionCommonService permissionService;

        #endregion

        #region Constructor

        public PageViewModel()
        {
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
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
            get { return _widgets; }
            set { _widgets = value; }
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
            Layout = from.PageLayout;
            Id = from.Id;
            Settings = from.Settings;
            ParentPageId = from.ParentPageId;
            var pageAccess = permissionService.GetAccess(from.Operations, HttpContext.Current.CorePrincipal(), typeof(Page), from.Id, IsPageOwner);
            Access = pageAccess;
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();

            foreach (var widget in from.Widgets.OrderBy(wd => wd.OrderNumber))
            {
                PageWidget widget1 = widget;
                ICoreWidget coreWidget =
                    (MvcApplication.Widgets).FirstOrDefault(wd => wd.Identifier == widget1.WidgetIdentifier);
                Widgets.Add(new WidgetHolderViewModel
                                {
                                    Id = widget.Id,
                                    Column = widget.ColumnNumber,
                                    Order = widget.OrderNumber,
                                    WidgetInstance = new CoreWidgetInstance
                                                         {
                                                             InstanceId = widget.InstanceId,
                                                             WidgetIdentifier = widget.WidgetIdentifier,
                                                             PageSettings = new CorePageSettings { PageId = from.Id }
                                                         },
                                    Settings = widget1.Settings,
                                    Widget = new WidgetHelper().IsWidgetEnabled(widget.WidgetIdentifier) ? coreWidget : null,
                                    Access = coreWidget is BaseWidget ?
                                        permissionService.GetAccess(((BaseWidget)coreWidget).Operations,
                                        HttpContext.Current.CorePrincipal(), coreWidget.GetType(), widget1.EntityId,
                                        currentPrincipal != null && widget1.User != null && widget1.User.PrincipalId == currentPrincipal.PrincipalId) : null,
                                    PageAccess = pageAccess
                                });
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
            to.Title = Title;
            to.Url = Url;
            to.ParentPageId = ParentPageId == 0 ? null : ParentPageId;
            return to;
        }

        #endregion
    }
}