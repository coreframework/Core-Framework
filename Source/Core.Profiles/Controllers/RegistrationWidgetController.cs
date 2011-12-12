using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Authentication;
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
            if (instance != null && instance.InstanceId.HasValue)
            {
                var widgetModel = RegistrationWidgetHelper.BindWidgetModel(instance);

                return PartialView(widgetModel);
            }

            return Content(HttpContext.Translate("Messages.SetupRegistrationForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public virtual ActionResult RegisterUser(RegistrationWidgetViewModel model, FormCollection collection)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IRegistrationWidgetService>();
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            var widget = widgetService.Find(model.PageWidgetId);

            if (widget != null)
            {
                RegistrationWidgetHelper.Validate(widget, collection, ModelState);
                if (ModelState.IsValid)
                {
                    User user;
                    if (RegistrationWidgetHelper.RegisterUser(widget, model, collection, out user) && user.Id > 0)
                    {
                        Success(HttpContext.Translate("Messages.UserCreated", String.Empty));

                        authenticationHelper.LoginUser(user, true);
                        model.IsSuccessfulRegistration = true;
                    }
                }
                else
                {
                    ViewData[String.Format("FormCollection{0}", widget.Id)] = collection;
                    Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
                }

                model.Widget = widget;
            }

            return PartialView("ViewWidget", model);
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