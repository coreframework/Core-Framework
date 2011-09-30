using System;
using System.Web.Mvc;
using Core.Web.Helpers;
using Core.Web.Models;
using Core.Web.NHibernate.Helpers;
using Framework.Mvc.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    /// <summary>
    /// Handles user sessions requests.
    /// </summary>
    public partial class UsersController : FrameworkController
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>        
        public UsersController()
        {
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders create new session view.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>Create new session view.</returns>
        public virtual ActionResult NewUserSession(String returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Creates new user session.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>Authentication result.</returns>
        public virtual ActionResult CreateUserSession(LoginViewModel model)
        {
            var user = UserSessionHelper.Validate(model, ModelState);
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            if (ModelState.IsValid)
            {
                authenticationHelper.LoginUser(user, model.RememberMe);
                var returnUrl = model.ReturnUrl;
                if (String.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = Url.Action(MVC.Home.Index());
                }
                return new RedirectResult(returnUrl);
            }

            return View("NewUserSession", model);
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
            return Redirect(Url.Action(MVC.Home.Index()));
        }

        #endregion
    }
}
