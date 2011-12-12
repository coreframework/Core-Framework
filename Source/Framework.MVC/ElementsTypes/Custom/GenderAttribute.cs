// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenderAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;
using Framework.Mvc.ElementsTypes.Generic;
using Framework.Mvc.ElementsTypes.Static;

namespace Framework.Mvc.ElementsTypes.Custom
{
    /// <summary>
    /// Email element type attribute.
    /// </summary>
    public class GenderAttribute : SelectBoxAttribute
    {
        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns element html code.</returns>
        public override string Render(HtmlHelper html, string name, string value, string values)
        {
            Gender selectedValue;
            Enum.TryParse(value, out selectedValue);
            return Extensions.SelectExtensions.DropDownListFor(html, name, selectedValue, null).ToString();
        }
    }
}
