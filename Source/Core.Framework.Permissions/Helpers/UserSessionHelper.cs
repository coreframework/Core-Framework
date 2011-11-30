using System;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.Permissions.Helpers
{
    /// <summary>
    /// Authentication process helper methods.
    /// </summary>
    public static class UserSessionHelper
    {
        /// <summary>
        /// Validates the specified login details.
        /// </summary>
        /// <param name="usernameOrEmail">The username or email.</param>
        /// <param name="password">The password.</param>
        /// <param name="modelState">State of the model.</param>
        /// <returns>User for authentication or <c>null</c>.</returns>
        public static BaseUser Validate(String usernameOrEmail, String password, ModelStateDictionary modelState)
        {
            var userService = ServiceLocator.Current.GetInstance<IBaseUserService>();
            BaseUser user = null;

            if (modelState.IsValid)
            {
                user = userService.FindByEmailOrUsername(usernameOrEmail);
                if (user == null || !userService.VerifyPassword(user, password))
                {
                    modelState.AddModelError(String.Empty, HttpContext.GetGlobalResourceObject("Messages", "InvalidUserCredentials") as String);
                }
            }

            return user;
        }
    }
}
