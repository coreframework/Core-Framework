// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBreadcrumb.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace Framework.Mvc.Breadcrumbs
{
    /// <summary>
    /// Describes the breadcrumb's item.
    /// </summary>
    public interface IBreadcrumb
    {
        /// <summary>
        /// Gets or sets the breadcrumb URL.
        /// </summary>
        /// <value>The breadcrumb URL.</value>
        String Url { get; set; }

        /// <summary>
        /// Gets or sets the breadcrumb title.
        /// </summary>
        /// <value>The breadcrumb title.</value>
        String Text { get; set; }
    }
}
