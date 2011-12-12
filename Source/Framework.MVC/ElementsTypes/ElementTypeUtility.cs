// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementTypeUtility.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Web.Mvc;

namespace Framework.Mvc.ElementsTypes
{
    /// <summary>
    /// Contains helpers classes for working with elements.
    /// </summary>
    public static class ElementTypeUtility
    {
        /// <summary>
        /// Renders the element by the type.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns html code by element type.</returns>
        public static String RenderElementType(HtmlHelper html, ElementType elementType, String name, String value, String values)
        {
            var elementBuilder = elementType.GetType().GetField(elementType.ToString()).GetCustomAttributes(typeof(CustomElementAttribute), true).FirstOrDefault() as CustomElementAttribute;
            return elementBuilder != null ? elementBuilder.Render(html, name, value, values) : String.Empty;
        }

        /// <summary>
        /// Validates the element.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="modelState">State of the model.</param>
        public static void ValidateElement(ElementType elementType, String name, String value, ModelStateDictionary modelState)
        {
            var element = elementType.GetType().GetField(elementType.ToString()).GetCustomAttributes(typeof(CustomElementAttribute), true).FirstOrDefault() as CustomElementAttribute;

            if (element != null && element is IValidatableElement)
            {
                (element as IValidatableElement).Validate(modelState, name, value);
            }
        }
    }
}
