// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBreadcrumbsBuilder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Web.Mvc;

namespace Framework.Mvc.Breadcrumbs
{
    /// <summary>
    /// Provides methods to buld breadcrumbs.
    /// </summary>
    public interface IBreadcrumbsBuilder
    {
        /// <summary>
        /// Builds the breadcrumbs.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="items">The items.</param>
        void BuildBreadcrumbs(Controller controller, IBreadcrumb[] items);
    }
}
