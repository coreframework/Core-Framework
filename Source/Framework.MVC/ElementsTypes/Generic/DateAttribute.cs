// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Web.Mvc;

namespace Framework.Mvc.ElementsTypes.Generic
{
    /// <summary>
    /// Date picker attribute.
    /// </summary>
    public class DateAttribute : TextBoxAttribute, IValidatableElement
    {
        #region Fields

        private const String DateFormat = "mm/dd/yy";

        private const String ImageUrl = "";

        #endregion

        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns html code.</returns>
        public override string Render(HtmlHelper html, String name, String value, String values)
        {
            var builder = new StringBuilder();
            if (!String.IsNullOrEmpty(value))
            {
                DateTime date;
                if (DateTime.TryParse(value, out date))
                {
                    value = date.ToString(DateFormat);
                }
            }

            builder.Append(base.Render(html, name, value, values));
            builder.Append("<script type=\"text/javascript\">$(document).ready(function() {$('#" + name + "').datepicker({ buttonImage: '" + ImageUrl + "', dateFormat: '" + DateFormat + "' }); });</script>");

            return builder.ToString();
        }

        /// <summary>
        /// Validates the specified element.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        public virtual void Validate(ModelStateDictionary modelState, string name, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                DateTime date;
                if (!DateTime.TryParse(value, out date))
                {
                    modelState.AddModelError(name, @"Error");
                }
            }
        }
    }
}
