// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpVerbConstraint.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.MVC.Routing
{
    /// <summary>
    /// Allows you to define which HTTP verbs are permitted when determining whether an HTTP request matches a route. 
    /// This implementation supports both native HTTP verbs and the X-HTTP-Method-Override hidden element submitted as part of an HTTP POST.
    /// </summary>
    public class HttpVerbConstraint : IRouteConstraint
    {
        #region Fields

        private const String HttpVerbFormKey = "X-HTTP-Method-Override";

        private readonly HttpVerbs verbs;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpVerbConstraint"/> class.
        /// </summary>
        /// <param name="routeVerbs">The route verbs.</param>
        public HttpVerbConstraint(HttpVerbs routeVerbs)
        {
            verbs = routeVerbs;
        }

        #endregion

        #region IRouteConstraint members

        /// <summary>
        /// Determines whether the URL parameter contains a valid value for this constraint.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated.</param>
        /// <returns>
        /// true if the URL parameter contains a valid value; otherwise, false.
        /// </returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var isMatch = true;

            if (routeDirection == RouteDirection.IncomingRequest)
            {
                var httpMethod = httpContext.Request.HttpMethod;
                var httpMethodOverride = httpContext.Request.Form[HttpVerbFormKey];
                if (!String.IsNullOrEmpty(httpMethodOverride))
                {
                    httpMethod = httpMethodOverride;
                }

                isMatch = verbs.ToString().ToUpperInvariant().Equals(httpMethod);
            }

            return isMatch;
        }

        #endregion
    }
}