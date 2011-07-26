// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Framework.Core.Extensions;
using Framework.MVC.Helpers;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Adds methods for generating labels HTML-markup to <see cref="HtmlHelper"/>.
    /// </summary>
    public static class LabelExtensions
    {
        #region Fields

        /// <summary>
        /// Key for displayAsterix option (determines whether asterix should be rendered for required fields).
        /// </summary>
        public const String DisplayAsterixKey = "displayAsterix";

        /// <summary>
        /// Key for asterix option (specifies asterix symbol).
        /// </summary>
        public const String AsterixKey = "asterix";

        /// <summary>
        /// Key for css class option.
        /// </summary>
        public const String CssClassKey = "cssClass";

        /// <summary>
        /// CSS-class for required field labels.
        /// </summary>
        public const String RequiredCssClass = "required";

        /// <summary>
        /// CSS-class for invalid field's labels.
        /// </summary>
        public const String LabelErrorCssClass = "label-validation-error";

        /// <summary>
        /// Areas constant.
        /// </summary>
        public const String Areas = "Areas";

        /// <summary>
        /// Default extension options.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <item>
        ///         <term>displayAsterix</term>
        ///         <description>true</description>
        ///     </item>
        ///     <item>
        ///         <term>asterix</term>
        ///         <description>*</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public static readonly RouteValueDictionary Defaults = new RouteValueDictionary
                                                                   {
                                                                       { DisplayAsterixKey, true },
                                                                       { AsterixKey, "*" },
                                                                   };

        #endregion

        #region Extensions

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="forName">Name of input related to.</param>
        /// <param name="labelText">The label text.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString CustomLabel(this HtmlHelper html, String forName, String labelText)
        {
            return CustomLabel(html, forName, labelText, (object)null);
        }

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="forName">Name of input related to.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString CustomLabel(this HtmlHelper html, String forName, String labelText, Object htmlAttributes)
        {
            return CustomLabel(html, forName, labelText, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="forName">Name of input related to.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString CustomLabel(this HtmlHelper html, String forName, String labelText, IDictionary<String, object> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("label");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("for", HtmlMarkupHelper.GenerateId(forName), true);
            tagBuilder.SetInnerText(labelText);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The label text.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString LabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, String labelText)
        {
            return LabelFor(html, expression, labelText, (object)null);
        }

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString LabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, String labelText, Object htmlAttributes)
        {
            return LabelFor(html, expression, labelText, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Labels the specified HTML helper.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>HTML markup containing label specified.</returns>
        public static MvcHtmlString LabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, String labelText, IDictionary<String, Object> htmlAttributes)
        {
            string inputName = ExpressionHelper.GetExpressionText(expression);
            return html.CustomLabel(inputName, labelText, htmlAttributes);
        }

        /// <summary>
        /// Translate property name to human readable name. Uses resources to find localized property name.
        /// If there are no resources found, <see cref="PropertyName"/> translation will be used.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="propertyAccessor">The property accessor.</param>
        /// <returns>Human readable property name.</returns>
        public static String DisplayNameFor<TModel>(this HtmlHelper html, Expression<Func<TModel, Object>> propertyAccessor)
        {
            return DisplayNameFor(html.ViewContext.HttpContext, typeof(TModel), PropertyName.For(propertyAccessor));
        }

        /// <summary>
        /// Translate property name to human readable name. Uses resources to find localized property name.
        /// If there are no resources found, <see cref="PropertyName"/> translation will be used.
        /// </summary>
        /// <param name="context">The http context instance that this method extends.</param>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="propertyName">Name of the proprty.</param>
        /// <returns>Human readable property name.</returns>
        public static String DisplayNameFor(this HttpContextBase context, Type modelType, String propertyName)
        {
            var result = ResourceHelper.TranslatePropertyName(context, modelType, propertyName);

            if (String.IsNullOrEmpty(result))
            {
                result = propertyName.Humanize();
            }
            return result;
        }

        /// <summary>
        /// Renders an HTML label element and the property name of the property that is represented by the specified expression and options.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="options">Extension options, <see cref="Defaults"/> for more details.</param>
        /// <returns>
        /// HTML label element and the property name of the property that is represented by the specified expression and options.
        /// </returns>
        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression,  Object options)
        {            
            return CustomLabel(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), new RouteValueDictionary(options));
        }

        /// <summary>
        /// Renders an HTML label element and the property name of the property that is represented by the specified expression using <see cref="Defaults"/> options.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// HTML label element and the property name of the property that is represented by the specified expression using <see cref="Defaults"/> options.
        /// </returns>
        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return CustomLabelFor(html, expression, null);
        }

        /// <summary>
        /// Renders an HTML label element and the property name of the property that is represented by the model.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="options">Extension options, <see cref="Defaults"/> for more details.</param>
        /// <returns>
        /// An HTML label element and the property name of the property that is represented by the model.
        /// </returns>
        public static MvcHtmlString CustomLabelForModel(this HtmlHelper html, Object options)
        {
            return CustomLabel(html, html.ViewData.ModelMetadata, new RouteValueDictionary(options));
        }

        /// <summary>
        /// Renders an HTML label element and the property name of the property that is represented by the model.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <returns>
        /// An HTML label element and the property name of the property that is represented by the model.
        /// </returns>
        public static MvcHtmlString CustomLabelForModel(this HtmlHelper html)
        {
            return CustomLabel(html, html.ViewData.ModelMetadata, null);
        }

        /// <summary>
        /// Localizeds the label for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="propertyAccessor">The property accessor.</param>
        /// <returns>An HTML label element and the property name of the property that is represented by the model.</returns>
        public static MvcHtmlString LocalizedLabelFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, Object>> propertyAccessor)
        {
            return LocalizedLabelFor(html, propertyAccessor, null);
        }

        /// <summary>
        /// Localizeds the label for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="propertyAccessor">The property accessor.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML label element and the property name of the property that is represented by the model.
        /// </returns>
        public static MvcHtmlString LocalizedLabelFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, Object>> propertyAccessor, Object htmlAttributes)
        {
            var labelText = ResourceHelper.TranslatePropertyName(html.ViewContext.HttpContext, typeof(TModel), PropertyName.For(propertyAccessor));

            if (String.IsNullOrEmpty(labelText))
            {
                labelText = PropertyName.For(propertyAccessor).Humanize();
            }
               
            var builder = new TagBuilder("label");
            builder.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(String.Empty));

            var routeValues = new RouteValueDictionary(htmlAttributes);

            Object cssClass;
            if (routeValues.TryGetValue(CssClassKey, out cssClass))
            {
                builder.AddCssClass(cssClass as String);
            }

            builder.SetInnerText(labelText);
            MvcHtmlString result = MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));

            return result;
        }

        #endregion

        #region Helper methods

        private static MvcHtmlString CustomLabel(HtmlHelper html, ModelMetadata metadata, RouteValueDictionary options)
        {
            var result = MvcHtmlString.Empty;
            options = Defaults.Merge(options);
            var text = GetLabelText(metadata, options);            
            if (!String.IsNullOrEmpty(text))
            {
                var builder = new TagBuilder("label");
                builder.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(String.Empty));

                Object cssClass;
                if (options.TryGetValue(CssClassKey, out cssClass))
                {
                    builder.AddCssClass(cssClass as String);
                }

                if (metadata.IsRequired)
                {
                    builder.AddCssClass(RequiredCssClass);
                }

                ModelState state;
                if (html.ViewData.ModelState.TryGetValue(metadata.PropertyName, out state) && state.Errors.Any())
                {
                    builder.AddCssClass(LabelErrorCssClass);
                }

                builder.SetInnerText(GetLabelText(metadata, options));
                result = MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
            }
            return result;            
        }

        private static String GetLabelText(ModelMetadata metadata, RouteValueDictionary options)
        {
            var text = new StringBuilder(metadata.DisplayName ?? metadata.PropertyName);
            if (metadata.IsRequired && (bool)options[DisplayAsterixKey])
            {
                text.AppendFormat(" {0}", options[AsterixKey]);
            }
            return text.ToString();
        }

        #endregion
    }
}