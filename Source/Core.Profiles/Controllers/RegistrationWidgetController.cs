using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Profiles.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "RegistrationWidget")]
    public partial class RegistrationWidgetController : CoreWidgetController
    {
        #region Fields

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return RegistrationWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructor

        public RegistrationWidgetController()
        {
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            /*if (instance != null)
            {
                var widget = WebContentWidgetHelper.BindWidgetModel(instance);
                if (widget != null)
                {
                    if (widget.Article != null)
                    {
                        return PartialView("WebContentWidget/DetailsMode", new WidgetDetailsModel(widget.Article, true));
                    }

                    return PartialView("ListingMode", WebContentWidgetHelper.BindListingModel(widget, 1));
                }
            }
            return Content(HttpContext.Translate("Messages.Setup", ResourceHelper.GetControllerScope(this)));*/
            return Content(String.Empty);
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
                var widget = new NHibernate.Models.RegistrationWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IRegistrationWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new RegistrationWidgetEditModel().MapFrom(widget));
            }

            return Content(HttpContext.Translate("Messages.SetupRegistrationForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(RegistrationWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                model = RegistrationWidgetHelper.SaveWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}