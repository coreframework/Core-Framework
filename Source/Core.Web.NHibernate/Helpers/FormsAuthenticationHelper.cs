﻿using System.Web.Security;
using Core.Framework.Permissions.Models;

namespace Core.Web.NHibernate.Helpers
{
    public class FormsAuthenticationHelper : IAuthenticationHelper
    {
        #region IAuthenticationHelper members

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="account">The account.</param><param name="remember"><c>true</c> to create a persistent cookie (one that is saved across browser sessions); otherwise, <c>false</c>.</param>
        public void LoginUser(ICorePrincipal account, bool remember)
        {
            FormsAuthentication.SetAuthCookie(account.Identity.Name, remember);
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public void LogoutUser()
        {
            FormsAuthentication.SignOut();
        }

        #endregion
    }
}
