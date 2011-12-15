using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Authentication;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Core.Profiles.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Static;
using Core.Profiles.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ProfileWidget")]
    public partial class ProfileWidgetController : CoreWidgetController
    {
        #region Fields

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return ProfileWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructor

        public ProfileWidgetController()
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
                var widgetModel = ProfileWidgetHelper.BindWidgetModel(instance, this.CorePrincipal());
                return PartialView(widgetModel);
            }

            return Content(HttpContext.Translate("Messages.SetupRegistrationForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Saves the user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [Authorize]
        public virtual ActionResult SaveUser(ProfileWidgetViewModel model, FormCollection collection)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProfileWidgetService>();
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            var widget = widgetService.Find(model.PageWidgetId);
            var userProfileService = ServiceLocator.Current.GetInstance<IUserProfileService>();

            if (widget != null)
            {
                if (widget.DisplayMode == ProfileWidgetDisplayMode.ProfileDetails)
                {
                    ModelState.Clear();
                }

                var userProfile = userProfileService.GetUserProfile(this.CorePrincipal());

                if (widget.DisplayMode != ProfileWidgetDisplayMode.CommonDetails)
                {
                    if (userProfile != null)
                    {
                        ProfileWidgetHelper.Validate(collection, ModelState, userProfile);
                    }
                }
              
                if (ModelState.IsValid)
                {
                    User user;
                    if (ProfileWidgetHelper.SaveUser(model, collection, userProfile, this.CorePrincipal(), widget, out user))
                    {
                        Success(HttpContext.Translate("Messages.UserUpdated", String.Empty));

                        if (widget.DisplayMode != ProfileWidgetDisplayMode.ProfileDetails)
                        {
                            authenticationHelper.LogoutUser();
                            authenticationHelper.LoginUser(user, true);
                        }
                    }
                }
                else
                {
                    ViewData[String.Format("FormCollection{0}", widget.Id)] = collection;
                    Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
                }

                model.Widget = widget;
                model.Profile = userProfile;
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
                var widget = new NHibernate.Models.ProfileWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IProfileWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new ProfileWidgetEditModel().MapFrom(widget));
            }

            return Content(HttpContext.Translate("Messages.SetupRegistrationForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(ProfileWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                model = ProfileWidgetHelper.SaveWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}
