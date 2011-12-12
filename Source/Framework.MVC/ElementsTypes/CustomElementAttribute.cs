// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomElementAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;

namespace Framework.Mvc.ElementsTypes
{
    /// <summary>
    /// Base element type class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class CustomElementAttribute : Attribute 
    {
        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns element html code.</returns>
        public abstract String Render(HtmlHelper html, String name, String value, String values);
    }
}
