// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

namespace Framework.Mvc.Extensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality for checkbox.
    /// </summary>
    public static class CheckBoxExtensions
    {
        /// <summary>
        /// Renders the check box.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The name of the control.</param>
        /// <param name="isChecked">if set to <c>true</c> [is checked].</param>
        /// <returns>Generates checkbox.</returns>
        public static String SimpleCheckBox(this HtmlHelper html, String name, bool isChecked)
        {
            return String.Format(@"<input type=""checkbox"" id=""{0}"" name=""{0}"" {1} value=""true"" />", name, isChecked ? "checked=\"checked\"" : String.Empty);
        }
    }
}
