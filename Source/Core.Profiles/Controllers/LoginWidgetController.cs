using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Authentication;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Profiles.Models;
using Core.Profiles.Widgets;
using Microsoft.Practices.ServiceLocation;
using Framework.Mvc.Extensions;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LoginWidget")]
    public partial class LoginWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return LoginWidget.Instance.Identifier;
            }
        }

        #endregion

        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            return PartialView(new LoginWidgetViewModel { PageWidgetId = instance.PageWidgetId ?? 0 });
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
            return Redirect(Url.Action("Index", "Home", new { area = ""}));
        }

    }
}
