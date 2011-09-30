using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Navigation.Helpers;
using Core.Web.Areas.Navigation.Models;
using Core.Web.Areas.Navigation.Widgets;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Controllers
{
    public partial class SiteMapController : CoreWidgetController
    { 
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return NSiteMapWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null && instance.InstanceId!=null)
            {
                IEnumerable<SiteMapViewWidgetModel> siteMap = SiteMapWidgetHelper.BindSiteMap(instance,this.CorePrincipal());

                return PartialView(siteMap);
            }
            return Content(HttpContext.Translate("SelectItemsToDisplay", ResourceHelper.GetControllerScope(this)));
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
                var widget = new SiteMapWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<ISiteMapWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }
                return PartialView(new SiteMapWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(SiteMapWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = SiteMapWidgetHelper.SaveSiteMapWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}
