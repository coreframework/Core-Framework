// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadcrumbsBuilder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Framework.Mvc.Breadcrumbs
{
    /// <summary>
    /// IBreadcrumbsBuilder implementations.
    /// </summary>
    public class BreadcrumbsBuilder : IBreadcrumbsBuilder
    {
        /// <summary>
        /// Builds the breadcrumbs.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="items">The items.</param>
        public void BuildBreadcrumbs(Controller controller, IBreadcrumb[] items)
        {
            controller.ViewData.Add(new KeyValuePair<String, Object>("Breadcrumbs", items));
        }
    }
}
