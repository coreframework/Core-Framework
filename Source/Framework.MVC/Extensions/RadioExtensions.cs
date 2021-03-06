﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Framework.Core.DomainModel;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality for radio buttons.
    /// </summary>
    public static class RadioExtensions
    {
        /// <summary>
        /// Generate a radion group element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The radio group element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML radio group whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString RadioListFor<TEnum>(this HtmlHelper html, String name, TEnum selectedValue, Object htmlAttributes)
            where TEnum : struct
        {
            return html.RadioListFor(name, selectedValue, new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Generate a radion group element for enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="name">The radio group element name.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>
        /// An HTML radio group whose options reflects for enums for each property in the object that is represented by the expression.
        /// </returns>
        public static MvcHtmlString RadioListFor<TEnum>(this HtmlHelper html, String name, TEnum selectedValue, IDictionary<String, Object> htmlAttributes)
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
                    if (description != null)
                    {
                        text = html.Translate(description.Description);
                    }
                    items.Add(new SelectListItem { Text = text, Value = value.ToString(), Selected = value.Equals(selectedValue) });
                }
            }
            StringBuilder builder = new StringBuilder();
            foreach (var radiobutton in items)
            {
                IDictionary<String, Object> buttonHtmlAttributes = new Dictionary<String, Object>(htmlAttributes)
                                                                       {
                                                                           { "id", radiobutton.Value }
                                                                       };
                builder.Append(html.RadioButton(name, radiobutton.Value, radiobutton.Selected, buttonHtmlAttributes).ToHtmlString());
                builder.Append(html.Label(radiobutton.Value, radiobutton.Text));
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        /// <summary>
        /// Redders radio buttons list.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="groupName">The name of the group.</param>
        /// <param name="items">The items.</param>
        /// <param name="selected">The selected object.</param>
        /// <returns>An HTML radio group.</returns>
        public static MvcHtmlString RadioList(this HtmlHelper html, String groupName, IDictionary<String, String> items, String selected)
        {
            var builder = new StringBuilder();

            foreach (var item in items)
            {
                if (String.IsNullOrEmpty(selected))
                {
                    selected = item.Key;
                }
                String checkedString = item.Key.Equals(selected) ? "checked='checked'" : String.Empty;
                builder.Append(String.Format("<div><input type='radio' name='{0}' id='{1}' value='{1}' {3}/>{2}</div>", groupName, item.Key, item.Value, checkedString));
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
