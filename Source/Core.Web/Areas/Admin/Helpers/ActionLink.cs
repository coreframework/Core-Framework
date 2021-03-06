// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionLink.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Web.Areas.Admin.Helpers
{
    /// <summary>
    /// Menu item that refers to controller action.
    /// </summary>
    /// <typeparam name="T">Controller type.</typeparam>
    public class ActionLink<T> : IMenuItem
            where T : IController
    {
        #region Fields

        private readonly String title;

        private readonly String imageUrl;

        private readonly Expression<Action<T>> action;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionLink&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="action">The action accessor.</param>
        public ActionLink(String title, Expression<Action<T>> action)
        {
            this.title = title;
            this.action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionLink&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="action">The action accessor.</param>
        public ActionLink(String title, String imageUrl, Expression<Action<T>> action)
        {
            this.title = title;
            this.action = action;
            this.imageUrl = imageUrl;
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
        /// Gets the image.
        /// </summary>
        /// <value>The image.</value>
        public String Image
        {
            get 
            {
                return imageUrl;
            }
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>Menu item link.</returns>
        public String GetUrl(UrlHelper urlHelper)
        {
            return urlHelper.Action(action);
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>
        /// Menu item image url.
        /// </returns>
        public String GetImageUrl(UrlHelper urlHelper)
        {
            return urlHelper.Content(Image);
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
            var areaName = ExpressionsHelper.GetAreaName<T>();
            var controllerName = ExpressionsHelper.GetControllerName<T>();

            var currentAreaName = context.RouteData.Values["area"] as String ?? String.Empty;
            var currentControllerName = context.RouteData.Values["controller"] as String ?? String.Empty;

            return NullOrEquals(areaName, currentAreaName) && NullOrEquals(controllerName, currentControllerName);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Check 
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        private static bool NullOrEquals(String left, String right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left != null)
            {
                return left.Equals(right);
            }
            
            return false;
        }

        #endregion
    }
}