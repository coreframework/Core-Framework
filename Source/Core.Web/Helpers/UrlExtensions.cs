// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Extends <see cref="UrlHelper"/> functionality.
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Generate site root url.
        /// </summary>
        /// <param name="url">The URL helper instance that this method extends.</param>
        /// <returns>Site root url.</returns>
        public static String RootUrl(this UrlHelper url)
        {
            return url.RouteUrl("Root");
        }

        /// <summary>
        /// Encodes url for SEO.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="unencodedUrl">The unencoded URL.</param>
        /// <returns>Returnes formated string.</returns>
        public static String EncodeForSEO(this UrlHelper helper, String unencodedUrl)
        {
            return helper.Encode(unencodedUrl.Replace(' ', '-'));
        }

    }
}