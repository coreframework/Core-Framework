using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Models
{
    public class TemplateViewModel : IMappedModel<Page, TemplateViewModel>
    {
        #region Fields

        private List<WidgetHolderViewModel> widgets = new List<WidgetHolderViewModel>();

        #endregion

        #region Properties

        public PageLayout Layout { get; set; }

        public List<WidgetHolderViewModel> Widgets
        {
            get { return widgets; }
            set { widgets = value; }
        }

        #endregion

        public TemplateViewModel MapFrom(Page from)
        {
            Layout = from.PageLayout;
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();
            var pageAccess = permissionService.GetAccess(from.Operations, HttpContext.Current.CorePrincipal(), typeof(Page), from.Id, true);

            foreach (var widget in from.Widgets.OrderBy(wd => wd.OrderNumber))
            {
                PageWidget widget1 = widget;
                ICoreWidget coreWidget =
                    widget.Widget != null
                        ? MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == widget1.Widget.Identifier)
                        : null;

                bool isWidetEnabled = widgetService.IsWidgetEnable(widget1.Widget);

                Widgets.Add(new WidgetHolderViewModel
                                {
                                    WidgetInstance = new CoreWidgetInstance
                                                         {
                                                             InstanceId = widget.InstanceId,
                                                             WidgetIdentifier =
                                                                 coreWidget != null ? coreWidget.Identifier : null,
                                                             PageSettings = new CorePageSettings { PageId = from.Id }
                                                         },
                                    Widget = widget1,

                                    Access = coreWidget is BaseWidget
                                                 ? permissionService.GetAccess(
                                                     ((BaseWidget)coreWidget).Operations,
                                                     HttpContext.Current.CorePrincipal(), coreWidget.GetType(),
                                                     widget1.EntityId,
                                                     currentPrincipal != null && widget1.User != null &&
                                                     widget1.User.PrincipalId == currentPrincipal.PrincipalId)
                                                 : null,
                                    PageAccess = pageAccess,
                                    SystemWidget = (isWidetEnabled && coreWidget != null) ? coreWidget : null
                                });
            }

            return this;
        }

        public Page MapTo(Page to)
        {
            throw new NotImplementedException();
        }
    }
}