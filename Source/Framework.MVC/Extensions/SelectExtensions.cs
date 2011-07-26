// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Linq;
using System.Web.UI;
using Framework.Core.DomainModel;
using Framework.MVC.Helpers;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality for dropdown list generation.
    /// </summary>
    public static class SelectExtensions
    {
        /// <summary>
        /// Generate a select element for each property in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return html.DropDownListFor(expression, new RouteValueDictionary());
        }

        /// <summary>
        /// Generate a select element for each property in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
        {
            return html.DropDownListFor(expression, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Generate a select element for each property in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<String, Object> htmlAttributes)
        {          
            var items = new List<SelectListItem>();
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if (metadata != null && metadata.ModelType.IsEnum)
            {
                foreach (var value in Enum.GetValues(metadata.ModelType))
                {
                    var exclude = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(ExcludeItemAttribute), false).FirstOrDefault();
                    if (exclude == null)
                    {
                        var text = Enum.GetName(metadata.ModelType, value);
                        var description = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                        if (description != null)
                        {
                            text = html.Translate(description.Description);
                        }
                        items.Add(new SelectListItem { Text = text, Value = ((int)value).ToString(), Selected = value.Equals(metadata.Model) });
                    }
                }
            }
           
            return html.DropDownListFor(expression, items, null, htmlAttributes);
        }

        /// <summary>
        /// Generate a select element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The select element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TEnum>(this HtmlHelper html, String name, TEnum selectedValue, Object htmlAttributes)
         where TEnum : struct
        {
            return html.DropDownListFor(name, selectedValue, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Generate a select element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The select element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TEnum>(this HtmlHelper html, String name, TEnum? selectedValue, Object htmlAttributes)
            where TEnum : struct
        {
            return html.DropDownListFor(name, selectedValue, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Generate a select element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The select element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TEnum>(this HtmlHelper html, String name, TEnum selectedValue, IDictionary<String, Object> htmlAttributes)
          where TEnum : struct
            {
                var items = new List<SelectListItem>();
                foreach (var value in Enum.GetValues(typeof(TEnum)))
                {
                    var exclude = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(ExcludeItemAttribute), false).FirstOrDefault();
                    if (exclude == null)
                    {
                        var text = Enum.GetName(typeof(TEnum), value);
                        var description = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                        text = description != null ?
                        html.Translate(description.Description) :
                        new HttpContextWrapper(HttpContext.Current).DisplayNameFor(typeof(TEnum), text);
                        items.Add(new SelectListItem { Text = text, Value = value.ToString() });
                    }
                }

                return html.DropDownList(name, items, null, htmlAttributes);
            }

        /// <summary>
        /// Generate a select element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The select element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML select element whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString DropDownListFor<TEnum>(this HtmlHelper html, String name, TEnum? selectedValue, IDictionary<String, Object> htmlAttributes)
            where TEnum : struct 
        {          
            var items = new List<SelectListItem>();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var exclude = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(ExcludeItemAttribute), false).FirstOrDefault();
                if (exclude == null)
                {
                    var text = Enum.GetName(typeof(TEnum), value);
                    var description = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                    text = description != null ? 
                        html.Translate(description.Description) :
                        new HttpContextWrapper(HttpContext.Current).DisplayNameFor(typeof(TEnum), text);

                    items.Add(new SelectListItem { Text = text, Value = value.ToString() });
                }
            }

            return html.DropDownList(name, items, null, htmlAttributes);
        }

        /// <summary>
        /// Options the group drop down list.
        /// </summary>
        /// <typeparam name="T">The main type of the property.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TProperty1">The type of the property1.</typeparam>
        /// <typeparam name="TProperty2">The type of the property2.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name of dropdown list.</param>
        /// <param name="collection">The collection of elements.</param>
        /// <param name="optionGroupProperty">The option group property.</param>
        /// <param name="optionValueProperty">The option value property.</param>
        /// <param name="optionTextProperty">The option text property.</param>
        /// <param name="isSelectedCondition">The is selected condition.</param>
        /// <param name="showEmptyValue">if set to <c>true</c> [show empty value].</param>
        /// <returns>
        /// An HTML select element with option groups.
        /// </returns>
        public static string OptionGroupDropDownList<T, TProperty, TProperty1, TProperty2>(this HtmlHelper htmlHelper, string name, List<T> collection, Func<T, TProperty> optionGroupProperty, Func<T, TProperty1> optionValueProperty, Func<T, TProperty2> optionTextProperty, Func<T, bool> isSelectedCondition, bool showEmptyValue)
        {
            IEnumerable<IGrouping<TProperty, T>> groupedCollection = collection.GroupBy(optionGroupProperty);

            using (var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, name);
                writer.RenderBeginTag(HtmlTextWriterTag.Select);

                if (showEmptyValue)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, String.Empty);
                    writer.RenderBeginTag(HtmlTextWriterTag.Option);
                    writer.Write(String.Empty);
                    writer.RenderEndTag();
                }

                foreach (var optionGroup in groupedCollection)
                {
                    TProperty optionGroupLabel = optionGroup.Key;
                    writer.AddAttribute("label", optionGroupLabel.ToString());
                    writer.RenderBeginTag("optgroup");

                    foreach (T option in optionGroup)
                    {
                        string optionValue = optionValueProperty.Invoke(option).ToString();
                        string optionText = optionTextProperty.Invoke(option).ToString();

                        bool isOptionSelected = isSelectedCondition.Invoke(option);

                        if (isOptionSelected)
                        {
                             writer.AddAttribute(HtmlTextWriterAttribute.Selected, "true");
                        }
                           
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, optionValue);
                        writer.RenderBeginTag(HtmlTextWriterTag.Option);
                        writer.Write(optionText);
                        writer.RenderEndTag();
                    }
                    writer.RenderEndTag();
                }

                writer.RenderEndTag();

               return stringWriter.ToString();
            }
        }  
    }
}