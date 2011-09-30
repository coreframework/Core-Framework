// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoutesExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Adds methods for retrieving current area name, controller name and action name from route data.
    /// </summary>
    public static class RoutesExtensions
    {
        #region Fields

        private const String AreaNameKey = "area";

        private const String ControllerNameKey = "controller";

        private const String ActionNameKey = "action";

        #endregion

        #region Extensions

        /// <summary>
        /// Gets area name from <paramref name="routeData"/>.
        /// </summary>
        /// <param name="routeData">The route data instance that this method extends.</param>
        /// <returns>Area name or <c>null</c> for default area.</returns>
        public static String AreaName(this RouteData routeData)
        {
            object areaName = routeData.Values[AreaNameKey];
            if (String.IsNullOrEmpty(areaName as String))
            {
                routeData.DataTokens.TryGetValue(AreaNameKey, out areaName);
            }
            return areaName as String;
        }

        /// <summary>
        /// Gets controller name from <paramref name="routeData"/>.
        /// </summary>
        /// <param name="routeData">The route data instance that this method extends.</param>
        /// <returns>Controller name.</returns>
        public static String ControllerName(this RouteData routeData)
        {
            return routeData.Values[ControllerNameKey] as String;
        }

        /// <summary>
        /// Gets action name from <paramref name="routeData"/>.
        /// </summary>
        /// <param name="routeData">The route data instance that this method extends.</param>
        /// <returns>Action name.</returns>
        public static String ActionName(this RouteData routeData)
        {
            return routeData.Values[ActionNameKey] as String;
        }

        /// <summary>
        /// Gets area name from <paramref name="controller"/> route data.
        /// </summary>
        /// <param name="controller">The controller instance that this method extends.</param>
        /// <returns>Area name or <c>null</c> for default area.</returns>
        public static String AreaName(this Controller controller)
        {
            return controller.RouteData.AreaName();
        }

        /// <summary>
        /// Gets controller name from <paramref name="controller"/> route data.
        /// </summary>
        /// <param name="controller">The controller instance that this method extends.</param>
        /// <returns>Controller name.</returns>
        public static String ControllerName(this Controller controller)
        {
            return controller.RouteData.ControllerName();
        }

        /// <summary>
        /// Gets action name from <paramref name="controller"/> route data.
        /// </summary>
        /// <param name="controller">The controller instance that this method extends.</param>
        /// <returns>Action name.</returns>
        public static String ActionName(this Controller controller)
        {
            return controller.RouteData.ActionName();
        }

        #endregion
    }
}