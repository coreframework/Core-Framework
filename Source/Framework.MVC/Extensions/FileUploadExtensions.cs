// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileUploadExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using Framework.Mvc.Helpers;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Adds methods for generating file upload HTML-markup to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class FileUploadExtensions
    {
        #region Extensions

        /// <summary>
        /// Returns a file input element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the model state key that is used to look up the validation errors.</param>
        /// <returns>An input element that has its type attribute set to "file".</returns>
        public static MvcHtmlString FileBox(this HtmlHelper html, String name)
        {
            return MvcHtmlString.Create(FileBoxHelper(html, name, null));
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper, the name of the form field, and the HTML attributes.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the model state key that is used to look up the validation errors.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <returns>An input element that has its type attribute set to "file".</returns>
        public static MvcHtmlString FileBox(this HtmlHelper html, String name, object htmlAttributes)
        {
            return MvcHtmlString.Create(FileBoxHelper(html, name, new RouteValueDictionary(htmlAttributes)));
        }

        /// <summary>
        /// Returns a file input element by using the specified HTML helper and model expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>An input element that has its type attribute set to "file".</returns>
        public static MvcHtmlString FileBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            String htmlFieldName = ModelMetadata.FromLambdaExpression(expression, html.ViewData).PropertyName;
            return MvcHtmlString.Create(FileBoxHelper(html, htmlFieldName, null));
        }

        /// <summary>
        /// Generates swfupload options for model.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>Upload options formated as json.</returns>
        public static MvcHtmlString UploadOptions(this HtmlHelper html)
        {
            var builder = new StringBuilder();
            if (html.ViewData.ModelMetadata.AdditionalValues.ContainsKey(UploadHelper.UploadOptionsKey))
            {
                var uploadOptions = html.ViewData.ModelMetadata.AdditionalValues[UploadHelper.UploadOptionsKey] as Dictionary<String, Object>;
                if (uploadOptions != null)
                {
                    foreach (var option in uploadOptions)
                    {
                        if (builder.Length > 0)
                        {
                            builder.AppendLine(",");
                        }
                        if (option.Value != null)
                        {
                            if (option.Value.GetType() == typeof(String))
                            {
                                builder.AppendFormat("{0}: \"{1}\"", option.Key, option.Value);
                            }
                            else
                            {
                                builder.AppendFormat("{0}: {1}", option.Key, option.Value.ToString().ToLowerInvariant());
                            }
                        }
                    }
                }
            }
            return MvcHtmlString.Create(builder.ToString());
        }

        #endregion

        #region Helper members

        private static String FileBoxHelper(HtmlHelper htmlHelper, String name, IDictionary<String, Object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "file", true);
            tagBuilder.MergeAttribute("name", name, true);
            if (htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes, true);
            }
            tagBuilder.GenerateId(name);

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion
    }
}