// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenderAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
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
            return html.DropDownList(name, ParseValues(Gender.Female + "," + Gender.Male, value)).ToString();
        }
    }
}
