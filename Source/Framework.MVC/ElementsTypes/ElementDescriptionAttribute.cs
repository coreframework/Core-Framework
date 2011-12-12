// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementDescriptionAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace Framework.Mvc.ElementsTypes
{
    /// <summary>
    /// Attribute to define element type properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ElementDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is required enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is required enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequiredEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is values enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is values enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsValuesEnabled { get; set; }
    }
}
