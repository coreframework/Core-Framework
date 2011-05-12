// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Framework.Core.Infrastructure;
using Framework.MVC.Controllers;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Adds methods for generating validation messages HTML-markup to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class ValidationExtensions
    {
        #region Fields

        /// <summary>
        /// Tooltip validation message CSS-class.
        /// </summary>
        public const String TooltipCssClass = "tooltip";

        /// <summary>
        /// Path to image tooltip.
        /// </summary>
        public const String DefaultToolTipImage = "~/Content/images/icons/tooltip.png";

        #endregion

        #region Extensions

        /// <summary>
        /// Renders the HTML markup for a validation-error message for the property that is represented by the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>
        /// HTML markup for a validation-error message for the property that is represented by the model.
        /// </returns>
        public static MvcHtmlString ValidationMessageForModel<TModel>(this HtmlHelper<TModel> html)
        {
            return html.ValidationMessageFor(model => model);
        }

        /// <summary>
        /// Renders the HTML markup for a validation-error message displayed as dynamic tooltip.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="iconPath">The relative path for icon.</param>
        /// <returns>HTML markup for a validation-error message displayed as dynamic tooltip.</returns>
        public static MvcHtmlString ValidationTooltipForModel<TModel>(this HtmlHelper<TModel> html, String iconPath)
        {
            return ValidationTooltip(html, html.ViewData.ModelMetadata, iconPath);
        }

        /// <summary>
        /// Renders the HTML markup for a validation-error message displayed as dynamic tooltip.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>HTML markup for a validation-error message displayed as dynamic tooltip.</returns>
        public static MvcHtmlString ValidationTooltipForModel<TModel>(this HtmlHelper<TModel> html)
        {
            return ValidationTooltip(html, html.ViewData.ModelMetadata, DefaultToolTipImage);
        }

        /// <summary>
        /// Renders messages from controller messages queue.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>
        /// HTML markup for messages queue.
        /// </returns>
        public static MvcHtmlString Messages(this HtmlHelper html)
        {
            var result = new StringBuilder();
            var messages = html.ViewContext.TempData[FrameworkController.MessagesKey] as Queue<Message>;
            if (messages != null)
            {
                while (messages.Any())
                {
                    var message = messages.Dequeue();
                    var builder = new TagBuilder("div");
                    builder.AddCssClass(message.MessageType.ToString().ToLowerInvariant());
                    builder.SetInnerText(message.Text);
                    result.Append(builder.ToString(TagRenderMode.Normal));
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }

        #endregion

        #region Helpers methods

        private static MvcHtmlString ValidationTooltip(HtmlHelper html, ModelMetadata metadata, String imagePath)
        {
            var result = MvcHtmlString.Empty;
            ModelState state;
            if (html.ViewData.ModelState.TryGetValue(metadata.PropertyName, out state) && state.Errors.Any())
            {
                var image = new TagBuilder("image");
                if (VirtualPathUtility.IsAppRelative(imagePath))
                {
                    imagePath = VirtualPathUtility.ToAbsolute(imagePath);
                }
                image.Attributes.Add("src", imagePath);
                image.Attributes.Add("alt", state.Errors.First().ErrorMessage);

                var wrapper = new TagBuilder("span");
                wrapper.AddCssClass(TooltipCssClass);
                wrapper.InnerHtml = image.ToString(TagRenderMode.SelfClosing);
                result = MvcHtmlString.Create(wrapper.ToString(TagRenderMode.Normal));
            }

            return result;
        }

        #endregion
    }
}