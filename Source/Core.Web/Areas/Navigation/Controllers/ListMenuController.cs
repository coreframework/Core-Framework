using System;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Navigation.Helpers;
using Core.Web.Areas.Navigation.Models;
using Core.Web.Areas.Navigation.Widgets;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Controllers
{
    public partial class ListMenuController : CoreWidgetController 
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return NListMenuWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null && instance.InstanceId != null)
            {
                var model = ListMenuWidgetHelper.BindWidgetModel(instance,this.CorePrincipal());

                if (model!=null)
                    return PartialView(model);
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
                var widget = new ListMenuWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IListMenuWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new ListMenuWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(ListMenuWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = ListMenuWidgetHelper.SaveListMenuWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}