// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatableElement.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Web.Mvc;

namespace Framework.Mvc.ElementsTypes
{
    /// <summary>
    /// Describe validatable element.
    /// </summary>
    public interface IValidatableElement
    {
        /// <summary>
        /// Validates the specified element.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        void Validate(ModelStateDictionary modelState, String name, String value);
    }
}
