﻿using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Web.Areas.Admin.Helpers
{
    public class RouteLink : IMenuItem
    {
        #region Fields

        private readonly String title;
        private readonly String routeName;
        
        #endregion

        #region Constructors

        public RouteLink(String title, String routeName)
        {
            this.title = title;
            this.routeName = routeName;
        }

        #endregion

        #region IMenuItem members

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        public String Title
        {
            get
            {
                return title;
            }
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <returns>Menu item link.</returns>
        public String GetUrl(UrlHelper url)
        {
            return url.RouteUrl(routeName);
        }

        /// <summary>
        /// Determines whether this item corresponds to current page.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///     <c>true</c> if this item corresponds to current page; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCurrent(RequestContext context)
        {
            return RouteTable.Routes[routeName].Equals(context.RouteData.Route);
        }

        #endregion
    }
}