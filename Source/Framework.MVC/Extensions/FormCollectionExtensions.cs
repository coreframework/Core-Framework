// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormCollectionExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Extends functionality of <see cref="FormCollection"/> class.
    /// </summary>
    public static class FormCollectionExtensions
    {
        /// <summary>
        /// Parse int value posted throught http-form.
        /// </summary>
        /// <param name="form">The form collection instance that this method extends.</param>
        /// <param name="key">The posted value key.</param>
        /// <returns>Parse int value or <see cref="int"/> default value.</returns>
        public static int IntValue(this FormCollection form, String key)
        {
            int value = default(int);
            var stringValue = form[key];
            if (!String.IsNullOrEmpty(stringValue))
            {
                int.TryParse(stringValue, out value);
            }
            return value;
        }

        /// <summary>
        /// Parse boolean value posted throught http-form.
        /// </summary>
        /// <param name="form">The form collection instance that this method extends.</param>
        /// <param name="key">The posted value key.</param>
        /// <returns>Parse boolean value or <see cref="bool"/> default value.</returns>
        public static bool BooleanValue(this FormCollection form, String key)
        {
            bool value = default(bool);
            var stringValue = form[key];
            if (!String.IsNullOrEmpty(stringValue))
            {
                bool.TryParse(stringValue, out value);
            }
            return value;
        }
    }
}