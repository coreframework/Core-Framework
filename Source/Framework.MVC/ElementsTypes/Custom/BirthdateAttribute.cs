// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BirthdateAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;
using Framework.Mvc.ElementsTypes.Generic;

namespace Framework.Mvc.ElementsTypes.Custom
{
    /// <summary>
    /// Describes birtdate element type.
    /// </summary>
    public class BirthdateAttribute : DateAttribute
    {
        /// <summary>
        /// Validates the specified element.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        public override void Validate(ModelStateDictionary modelState, string name, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                DateTime date;
                if (!DateTime.TryParse(value, out date))
                {
                    modelState.AddModelError(name, @"The field format is not valid.");
                }
                else
                {
                    if (date.Date >= DateTime.Now.Date)
                    {
                        modelState.AddModelError(name, @"The field value should be less than current date.");
                    }
                }
            }
        }
    }
}
