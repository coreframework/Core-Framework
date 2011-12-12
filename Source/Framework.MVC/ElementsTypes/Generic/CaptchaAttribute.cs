// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptchaAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Web.Mvc;
using Framework.Mvc.Extensions;

namespace Framework.Mvc.ElementsTypes.Generic
{
    /// <summary>
    /// Captcha attribute.
    /// </summary>
    public class CaptchaAttribute : CustomElementAttribute
    {
        /// <summary>
        /// Captcha default height.
        /// </summary>
        public static readonly Int32 CaptchaDefaultHeight = 40;

        /// <summary>
        /// Captcha default width.
        /// </summary>
        public static readonly Int32 CaptchaDefaultWidth = 120;

        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="name">The element name.</param>
        /// <param name="value">The element value.</param>
        /// <param name="values">The element values.</param>
        /// <returns>Returns element html code.</returns>
        public override string Render(HtmlHelper html, String name, String value, String values)
        {
            var builder = new StringBuilder();
            builder.Append(html.CaptchaImage(CaptchaDefaultHeight, CaptchaDefaultWidth));
            builder.Append("<br/>");
            builder.Append(html.CaptchaTextBox(name));

            return builder.ToString();
        }
    }
}
