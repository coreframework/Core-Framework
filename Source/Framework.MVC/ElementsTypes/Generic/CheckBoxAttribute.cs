// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Framework.Mvc.Extensions;

namespace Framework.Mvc.ElementsTypes.Generic
{
    /// <summary>
    /// Text box attribute.
    /// </summary>
    public class CheckBoxAttribute : CustomElementAttribute
    {
        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns element html code.</returns>
        public override string Render(HtmlHelper html, String name, String value, String values)
        {
            bool selected;
            Boolean.TryParse(value, out selected);

            return html.SimpleCheckBox(name, selected);
        }
    }
}
