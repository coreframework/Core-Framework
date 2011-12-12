// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonsAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Framework.Mvc.Extensions;

namespace Framework.Mvc.ElementsTypes.Generic
{
    /// <summary>
    /// Text box attribute.
    /// </summary>
    public class RadioButtonsAttribute : CustomElementAttribute
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
            return html.RadioList(name, ParseValues(values, name), value).ToString();
        }

        /// <summary>
        /// Parses the values.
        /// </summary>
        /// <param name="values">The element values.</param>
        /// <param name="name">The element name.</param>
        /// <returns>Returns the dictionary of values.</returns>
        protected static Dictionary<String, String> ParseValues(String values, String name)
        {
            var result = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(values))
            {
                var valuesArray = values.Trim().Split(ValueSeparator);
                for (int i = 0; i < valuesArray.Length; i++)
                {
                    if (!String.IsNullOrEmpty(valuesArray[i]) && !result.ContainsKey(valuesArray[i]))
                    {
                        result.Add(String.Format("{0}_{1}", name, i), valuesArray[i]);
                    }
                }
            }
            return result;
        } 
    }
}
