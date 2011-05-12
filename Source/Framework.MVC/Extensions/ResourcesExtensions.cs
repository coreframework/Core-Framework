// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using Castle.Core.Logging;

using Framework.MVC.Helpers;

using Microsoft.Practices.ServiceLocation;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality to provide views localization.
    /// </summary>
    public static class ResourcesExtensions
    {
        #region Fields

        private const String TranslationMissingTemplate = "Translation missing for \"{0}\" (scope - \"{1}\", culture - \"{2}\").";

        #endregion

        #region Extensions

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="httpContext">The http context instance that this method extends.</param>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        public static String Translate(this HttpContextBase httpContext, String key, String scope)
        {
            return ResourceHelper.GetResourceString(httpContext, key, scope, null, DefaultTranslationMissing);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="httpContext">The http context instance that this method extends.</param>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        public static String Translate(this HttpContextBase httpContext, String key, IEnumerable<String> scope)
        {
            return httpContext.Translate(key, ResourceHelper.Combine(scope));
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        public static String Translate(this HtmlHelper html, String key, String scope)
        {
            return html.ViewContext.HttpContext.Translate(key, scope);
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> and <paramref name="scope"/> specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="key">The resource key.</param>
        /// <param name="scope">The resource scope.</param>
        /// <returns>String localized for current thread culture.</returns>
        public static String Translate(this HtmlHelper html, String key, IEnumerable<String> scope)
        {
            return html.Translate(key, ResourceHelper.Combine(scope));
        }

        /// <summary>
        /// Gets global resource for <paramref name="key"/> specified using default scope ({AreaName}.Views.{ControllerName}.{ViewName}).
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="key">The resource key.</param>
        /// <returns>String localized for current thread culture.</returns>
        public static String Translate(this HtmlHelper html, String key)
        {
            var scope = String.Empty;
            if (key.StartsWith(ResourceHelper.ScopeSeparator))
            {
                key = key.Remove(0, ResourceHelper.ScopeSeparator.Length);
                scope = ResourceHelper.GetViewScope(html.ViewContext);
            }
            return html.Translate(key, scope);
        }

        /// <summary>
        /// Renders the specified partial view as an HTML-encoded string with specific localization scope.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="partialViewName">The name of the partial view to render.</param>
        /// <returns>The partial view that is rendered as an HTML-encoded string.</returns>
        public static MvcHtmlString PartialWithScope(this HtmlHelper html, String partialViewName)
        {
            var viewData = new ViewDataDictionary();
            viewData[ResourceHelper.ViewScopeKey] = ResourceHelper.GetPartialViewScope(html.ViewContext, partialViewName);
            return html.Partial(partialViewName, viewData);
        }

        #endregion

        #region Helper members

        private static String DefaultTranslationMissing(String resourceKey, String scope, String culture)
        {
            var message = String.Format(TranslationMissingTemplate, resourceKey, scope, culture);
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            logger.Warn(message);
            return message;
        }

        #endregion
    }
}