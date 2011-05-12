using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Authentication process helper methods.
    /// </summary>
    public static class UserSessionHelper
    {
        /// <summary>
        /// Validates the specified login details.
        /// </summary>
        /// <param name="login">The login details.</param>
        /// <param name="modelState">State of the model.</param>
        /// <returns>User for authentication or <c>null</c>.</returns>
        public static User Validate(LoginViewModel login, ModelStateDictionary modelState)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            User user = null;

            if (modelState.IsValid)
            {
                user = userService.FindByEmailOrUsername(login.UsernameOrEmail);
                if (user == null || !userService.VerifyPassword(user, login.Password))
                {
                    modelState.AddModelError(String.Empty, HttpContext.GetGlobalResourceObject("Messages", "InvalidUserCredentials") as String);
                }
            }

            return user;
        }
    }
}