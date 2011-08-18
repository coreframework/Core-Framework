// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadcrumbsExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Framework.MVC.Breadcrumbs;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Adds methods for breadcrumbs managing to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class BreadcrumbsExtensions
    {
        #region Fields

        private const String BreadcrumbsKey = "Breadcrumbs";

        #endregion

        /// <summary>
        /// Renders the breadcrumbs element.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <returns>Breadcrumbs string.</returns>
        public static MvcHtmlString Breadcrumbs(this HtmlHelper html)
        {
            var result = new StringBuilder();
            var breadcrumbs = html.ViewContext.ViewData[BreadcrumbsKey] as IBreadcrumb[];
            if (breadcrumbs != null)
            {
                result.Append(@"<div class=""breadcrumbs"">");
                result.Append(@"<ul>");

                foreach (var item in breadcrumbs)
                {
                    result.Append(BuildBreadcrumb(item, item != breadcrumbs.Last()));
                }

                result.Append(@"</ul>");
                result.Append(@"</div>");
            }
            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// Builds the breadcrumbs item.
        /// </summary>
        /// <param name="item">The breadcrumb item.</param>
        /// <param name="isClickable">if set to <c>true</c> [is clickable].</param>
        /// <returns>Breadcrumbs item string.</returns>
        public static String BuildBreadcrumb(IBreadcrumb item, bool isClickable)
        {
            var builder = new StringBuilder("<li>");
            if (!isClickable || String.IsNullOrEmpty(item.Url))
            {
                builder.Append(item.Text);
            }
            else
            {
                builder.AppendFormat(String.Format("<a href=\"{0}\">{1}</a>", item.Url, item.Text));
            }
            builder.Append("</li>");
            return builder.ToString();
        }
    }
}
