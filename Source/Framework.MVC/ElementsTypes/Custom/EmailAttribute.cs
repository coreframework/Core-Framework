// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Framework.Core.Helpers.Regex;
using Framework.Mvc.ElementsTypes.Generic;

namespace Framework.Mvc.ElementsTypes.Custom
{
    /// <summary>
    /// Email element type attribute.
    /// </summary>
    public class EmailAttribute : TextBoxAttribute, IValidatableElement
    {
        /// <summary>
        /// Validates the specified element.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        public void Validate(ModelStateDictionary modelState, string name, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return;
            }

            if (!Regex.IsMatch(value, RegexValidationConfig.GetPattern(RegexTemplates.Email)))
            {
                modelState.AddModelError(name, @"Email error");
            }
        }
    }
}
