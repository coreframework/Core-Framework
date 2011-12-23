using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.FormLogin.Models;
using Core.FormLogin.NHibernate.Contracts;
using Core.FormLogin.NHibernate.Models;
using Core.FormLogin.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Authentication;
using Core.Framework.Plugins.Web;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.FormLogin.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LoginWidget")]
    public partial class LoginWidgetController : CoreWidgetController
    {
        #region Fields

        private IFormLoginWidgetService formLoginWidgetService;

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return LoginWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructors

        public LoginWidgetController()
        {
            formLoginWidgetService = ServiceLocator.Current.GetInstance<IFormLoginWidgetService>();
        }

        #endregion

        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            bool showTitle = true;
            if (instance != null && instance.InstanceId != null)
            {
                var existingWidget = formLoginWidgetService.Find((long) instance.InstanceId);
                if(existingWidget != null)
                {
                    showTitle = existingWidget.ShowTitle;
                }
            }

            return PartialView(new LoginWidgetViewModel { PageWidgetId = instance.PageWidgetId ?? 0, ShowTitle = showTitle });
        }

        /// <summary>
        /// Creates new user session.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>Authentication result.</returns>
        public virtual ActionResult CreateUserSession(LoginWidgetViewModel model)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            if (ModelState.IsValid)
            {
                User user = userService.FindByEmailOrUsername(model.UsernameOrEmail);
                if (user == null || !userService.VerifyPassword(user, model.Password))
                {
                    Error(HttpContext.Translate("Messages.InvalidUserCredentials", String.Empty));
                }
                else
                {
                    authenticationHelper.LoginUser(user, model.RememberMe);
                    model.IsSuccessfulLogin = true;
                }
            }

            return PartialView("ViewWidget", model);
        }

        /// <summary>
        /// Close current authentication session.
        /// </summary>
        /// <returns>Redirects to root url.</returns>
        [HttpGet]
        public virtual ActionResult DeleteUserSession()
        {
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            authenticationHelper.LogoutUser();
            return Redirect(Url.Action("Index", "Home", new { area = "" }));
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            var widgetModel = new LoginWidgetEditModel();
            if (instance != null)
            {
                FormLoginWidget widget = null;
                if (instance.InstanceId != null)
                {
                    var existingWidget = formLoginWidgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }
                if(widget == null)
                {
                    widget = new FormLoginWidget();
                }

                widgetModel = widgetModel.MapFrom(widget);
            }

            return PartialView(widgetModel);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(LoginWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                var widget = new FormLoginWidget();
                if (model.Id > 0)
                {
                    widget = formLoginWidgetService.Find(model.Id);
                }
                widget = model.MapTo(widget);
                formLoginWidgetService.Save(widget);
                model.MapFrom(widget);
                Success(HttpContext.Translate("Messages.Success",
                                                                ResourceHelper.GetControllerScope(this)));
            }

            return PartialView("EditWidget", model);
        }
    }
}
