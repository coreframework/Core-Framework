// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequiredAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web;
using Framework.MVC.Helpers;

namespace Framework.MVC.Metadata.Attributes
{
    /// <summary>
    /// sdfsdfsdf sdfsdfsdf sdfsdfsdfsdf.
    /// </summary>
    public class LocalizedRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            var ttt = ErrorMessageString;
            return base.FormatErrorMessage(name);
        }
    }
}
