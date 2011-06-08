using System;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Navigation.Helpers;
using Core.Web.Areas.Navigation.Models;
using Core.Web.Areas.Navigation.Widgets;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Controllers
{
    public partial class BreadcrumbsController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return NBreadcrumbsWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null && instance.InstanceId != null)
            {
                var model = BreadcrumbsWidgetHelper.BindWidgetModel(instance, this.CorePrincipal());

                if (model != null)
                    return PartialView(model);
            }
            return Content("Setup your breadcrumbs");
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
                var widget = new BreadcrumbsWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IBreadcrumbsWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new BreadcrumbsWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(BreadcrumbsWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = BreadcrumbsWidgetHelper.SaveBreadcrumbsWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}
