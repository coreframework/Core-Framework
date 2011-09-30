// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Breadcrumb.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace Framework.Mvc.Breadcrumbs
{
    /// <summary>
    /// Provides IBreadcrumb implementation.
    /// </summary>
    public class Breadcrumb : IBreadcrumb
    {
        /// <summary>
        /// Gets or sets the breadcrumb URL.
        /// </summary>
        /// <value>The breadcrumb URL.</value>
        public String Url
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the breadcrumb title.
        /// </summary>
        /// <value>The breadcrumb title.</value>
        public String Text
        {
            get; set;
        }
    }
}
