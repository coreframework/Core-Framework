// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUploadModule.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Security;

namespace Framework.Mvc.Helpers
{
    /// <summary>
    /// Initialize accept language and authentication cookie value for swf-upload requests.
    /// </summary>
    public class FileUploadModule : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application.</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequestHandler;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Updates the cookie value.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        private static void UpdateCookie(String name, String value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie == null)
            {
                cookie = new HttpCookie(name);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddMinutes(10);
            HttpContext.Current.Request.Cookies.Set(cookie);
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Begins the request handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BeginRequestHandler(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Form[UploadHelper.AuthenticationCookieKey] != null)
            {
                UpdateCookie(FormsAuthentication.FormsCookieName, HttpContext.Current.Request.Form[UploadHelper.AuthenticationCookieKey]);
            }
        }

        #endregion
    }
}