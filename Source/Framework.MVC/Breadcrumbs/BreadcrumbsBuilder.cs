// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadcrumbsBuilder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Web.Mvc;

namespace Framework.MVC.Breadcrumbs
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
            controller.ViewData.Add(new KeyValuePair<string, object>("Breadcrumbs", items));
        }
    }
}
