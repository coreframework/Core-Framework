using System;
using System;
using System.ComponentModel.Composition;
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
    public partial class NavigationMenuController :  CoreWidgetController 
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return NNavigationMenuWidget.Instance.Identifier; }
        }

        #endregion


        #region Actions

        /// <summary>
        /// View the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null && instance.InstanceId != null)
            {
                var widgetService = ServiceLocator.Current.GetInstance<INavigationMenuWidgetService>();
                var existingWidget = widgetService.Find((long)instance.InstanceId);

                if (existingWidget != null)
                    return View(MVC.Navigation.NavigationMenu.Views.ViewWidget, NavigationMenuWidgetHelper.GetNavigationMenu(this.CorePrincipal(), existingWidget));
            }
            return Content(HttpContext.Translate("SelectOrientation", ResourceHelper.GetControllerScope(this)));
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
                var widget = new NavigationMenuWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<INavigationMenuWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }
                return PartialView(new NavigationMenuWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(NavigationMenuWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = NavigationMenuWidgetHelper.SaveNavigationMenuWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion

    }
}
