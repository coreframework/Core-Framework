// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IT4MVCActionResult.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Routing;

namespace Framework.Mvc.T4MVC
{
    /// <summary>
    /// IT4MVCActionResult inteface.
    /// </summary>
    public interface IT4MVCActionResult
    {
        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The mvc area.</value>
        String Area { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        String Controller { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        String Action { get; set; }

        /// <summary>
        /// Gets or sets the route value dictionary.
        /// </summary>
        /// <value>The route value dictionary.</value>
        RouteValueDictionary RouteValueDictionary { get; set; }
    }   
}
