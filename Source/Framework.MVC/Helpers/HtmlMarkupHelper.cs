// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlMarkupHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;

namespace Framework.MVC.Helpers
{
    /// <summary>
    /// Provides helper method for HTML markup generation.
    /// </summary>
    public class HtmlMarkupHelper
    {
        /// <summary>
        /// Generate HTML element id using name.
        /// </summary>
        /// <param name="name">HTML element name.</param>
        /// <returns>Generated HTML element id.</returns>
        public static String GenerateId(String name)
        {
            var builder = new TagBuilder("input");
            builder.GenerateId(name);
            return builder.Attributes["id"];
        }
    }
}