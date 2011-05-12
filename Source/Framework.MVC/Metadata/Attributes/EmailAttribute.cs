// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.MVC.Metadata.Attributes
{
    /// <summary>
    /// Email validation attribute.
    /// </summary>
    public class EmailAttribute : RegularExpressionAttribute
    {
        private const String Expression = @"^(?:[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+\.)*[\w\!\#\$\%\&\'\*\+\-\/\=\?\^\`\{\|\}\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!\.)){0,61}[a-zA-Z0-9]?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\[(?:(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\.){3}(?:[01]?\d{1,2}|2[0-4]\d|25[0-5])\]))$";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAttribute"/> class.
        /// </summary>
        public EmailAttribute() : base(Expression)
        {
        }
    }
}