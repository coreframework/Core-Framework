// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Extends functionality of <see cref="UrlHelper"/>.
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Returns absolute url for specified <paramref name="relativeUrl"/> or empty string if <paramref name="relativeUrl"/> is null or empty.
        /// </summary>
        /// <param name="url">The URL helper instance that this method extends.</param>
        /// <param name="relativeUrl">Application relative url.</param>
        /// <returns>Absolute url for specified <paramref name="relativeUrl"/> or empty string if <paramref name="relativeUrl"/> is null or empty.</returns>
        public static String AbsoluteUrl(this UrlHelper url, String relativeUrl)
        {
            var result = String.Empty;
            if (!String.IsNullOrEmpty(relativeUrl))
            {
                result = relativeUrl;
            }
            if (!String.IsNullOrEmpty(result) && VirtualPathUtility.IsAppRelative(result))
            {
                result = VirtualPathUtility.ToAbsolute(result);
            }
            return result;
        }
    }
}