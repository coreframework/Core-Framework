// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectBoxAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Framework.Mvc.ElementsTypes.Generic
{
    /// <summary>
    /// Text box attribute.
    /// </summary>
    public class SelectBoxAttribute : CustomElementAttribute
    {
        private const char ValueSeparator = ',';

        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns element html code.</returns>
        public override String Render(HtmlHelper html, String name, String value, String values)
        {
            return html.DropDownList(name, ParseValues(values, value)).ToString();
        }

        /// <summary>
        /// Parses the values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns>Returns select box values.</returns>
        protected static IEnumerable<SelectListItem> ParseValues(String values, String selectedValue)
        {
            var result = new List<SelectListItem> { new SelectListItem { Text = @"Please select", Value = String.Empty } };

            if (!String.IsNullOrEmpty(values))
            {
                var valuesArray = values.Trim().Split(ValueSeparator);
                result.AddRange(from item in valuesArray
                                where !String.IsNullOrEmpty(item)
                                select new SelectListItem { Text = item.Trim(), Value = item.Trim(), Selected = item.Equals(selectedValue) });
            }
            return result;
        } 
    }
}
