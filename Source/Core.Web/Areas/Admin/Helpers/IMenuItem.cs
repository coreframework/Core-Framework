// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuItem.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Web.Areas.Admin.Helpers
{
    /// <summary>
    /// Specifies interface for menu entry.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get; }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <value>The image.</value>
        String Image { get; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>Menu item link.</returns>
        String GetUrl(UrlHelper urlHelper);

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>Menu item image url.</returns>
        String GetImageUrl(UrlHelper urlHelper);

        /// <summary>
        /// Determines whether this item corresponds to current page.
        /// </summary>
        /// <param name="context">Request context.</param>
        /// <returns>
        ///     <c>true</c> if this item corresponds to current page; otherwise, <c>false</c>.
        /// </returns>
        bool IsCurrent(RequestContext context);
    }
}