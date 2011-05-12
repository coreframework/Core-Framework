// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Adds methods for generating buttons HTML-markup to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class ButtonExtensions
    {
        #region Fields

        /// <summary>
        /// Indicates hidden submit button.
        /// </summary>
        public const String HiddenSubmitCssClass = "hidden-submit";

        /// <summary>
        /// Indicates link buttons.
        /// </summary>
        public const String ButtonCssClass = "button";

        /// <summary>
        /// Indicates link buttons that submits form.
        /// </summary>
        public const String SubmitButtonCssClass = "submit-button";

        /// <summary>
        /// Empty javascript action (javascript:void(0);).
        /// </summary>
        public const String EmptyHref = "javascript:void(0);";

        #endregion

        #region Extensions

        /// <summary>
        /// Renders an HTML submit element.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The submit text value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup submit element.</returns>
        public static MvcHtmlString Submit(this HtmlHelper html, String text, Object htmlAttributes)
        {
            return SubmitHelper(text, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Renders an HTML submit element.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The submit text value.</param>
        /// <returns>HTML markup submit element.</returns>
        public static MvcHtmlString Submit(this HtmlHelper html, String text)
        {
            return SubmitHelper(text, null);
        }

        /// <summary>
        /// Renders an HTML submit element with <see cref="HiddenSubmitCssClass"/> CSS-class.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>
        /// HTML submit element with <see cref="HiddenSubmitCssClass"/> CSS-class.
        /// </returns>
        /// <remarks>
        /// Hidden submit used for form submiting on enter press.
        /// </remarks>
        public static MvcHtmlString HiddenSubmit(this HtmlHelper html)
        {
            return SubmitHelper(String.Empty, new RouteValueDictionary(new { @class = HiddenSubmitCssClass }));
        }

        /// <summary>
        /// Renders an HTML submit element with <see cref="HiddenSubmitCssClass"/> CSS-class.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The submit text value.</param>
        /// <returns>
        /// HTML submit element with <see cref="HiddenSubmitCssClass"/> CSS-class.
        /// </returns>
        /// <remarks>
        /// Hidden submit used for form submiting on enter press.
        /// </remarks>
        public static MvcHtmlString HiddenSubmit(this HtmlHelper html, String text)
        {
            return SubmitHelper(text, new RouteValueDictionary(new { @class = HiddenSubmitCssClass }));
        }

        /// <summary>
        /// Renders an HTML link element styled as button.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <returns>HTML button-styled link.</returns>
        public static MvcHtmlString LinkButton(this HtmlHelper html, String text)
        {
            return LinkButtonHelper(text, String.Empty, null, false);
        }

        /// <summary>
        /// Renders an HTML link element styled as button.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML button-styled link.</returns>
        public static MvcHtmlString LinkButton(this HtmlHelper html, String text, Object htmlAttributes)
        {
            return LinkButtonHelper(text, String.Empty, new RouteValueDictionary(htmlAttributes), false);
        }

        /// <summary>
        /// Renders an HTML link element styled as button with CSS-class specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="href">The link href.</param>
        /// <returns>
        /// HTML button-styled link with CSS-class specified.
        /// </returns>
        public static MvcHtmlString LinkButton(this HtmlHelper html, String text, String href)
        {
            return LinkButtonHelper(text, href, null, false);
        }

        /// <summary>
        /// Renders an HTML link element styled as button with CSS-class specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="href">The link href.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// HTML button-styled link with CSS-class specified.
        /// </returns>
        public static MvcHtmlString LinkButton(this HtmlHelper html, String text, String href, Object htmlAttributes)
        {
            return LinkButtonHelper(text, href, new RouteValueDictionary(htmlAttributes), false);
        }

        /// <summary>
        /// Renders an HTML link element styled as button.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <returns>HTML button-styled link.</returns>
        public static MvcHtmlString LinkSubmitButton(this HtmlHelper html, String text)
        {
            return LinkButtonHelper(text, String.Empty, null, true);
        }

        /// <summary>
        /// Renders an HTML link element styled as button.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML button-styled link.</returns>
        public static MvcHtmlString LinkSubmitButton(this HtmlHelper html, String text, Object htmlAttributes)
        {
            return LinkButtonHelper(text, String.Empty, new RouteValueDictionary(htmlAttributes), true);
        }

        /// <summary>
        /// Renders an HTML link element styled as button with CSS-class specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="href">The link href.</param>
        /// <returns>
        /// HTML button-styled link with CSS-class specified.
        /// </returns>
        public static MvcHtmlString LinkSubmitButton(this HtmlHelper html, String text, String href)
        {
            return LinkButtonHelper(text, href, null, true);
        }

        /// <summary>
        /// Renders an HTML link element styled as button with CSS-class specified.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="text">The button text.</param>
        /// <param name="href">The link href.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// HTML button-styled link with CSS-class specified.
        /// </returns>
        public static MvcHtmlString LinkSubmitButton(this HtmlHelper html, String text, String href, Object htmlAttributes)
        {
            return LinkButtonHelper(text, href, new RouteValueDictionary(htmlAttributes), true);
        }

        #endregion

        #region Helper methods

        private static MvcHtmlString SubmitHelper(String text, RouteValueDictionary htmlAttributes)
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "submit";
            builder.Attributes["value"] = text;
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes);
            }
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        private static MvcHtmlString LinkButtonHelper(String text, String href, RouteValueDictionary htmlAttributes, bool isSubmit)
        {
            var builder = new TagBuilder("a");
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes);
            }
            if (!String.IsNullOrEmpty(href))
            {
                builder.Attributes["href"] = href;
            }
            else
            {
                builder.Attributes["href"] = EmptyHref;
            }
            builder.AddCssClass(ButtonCssClass);
            if (isSubmit)
            {
                builder.AddCssClass(SubmitButtonCssClass);
            }
            builder.SetInnerText(text);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        #endregion
    }
}