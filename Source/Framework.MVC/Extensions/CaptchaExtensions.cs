// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptchaExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// <copyright file="CaptchaExtensions.cs" company="Managed Fusion, LLC">
//  Copyright (C) 2007-2008 Nicholas Berardi, Managed Fusion, LLC (nick@managedfusion.com)
// </copyright>
// <author>Nicholas Berardi</author>
// <author_email>nick@managedfusion.com</author_email>
// <company>Managed Fusion, LLC</company>
// <product>Url Rewriter and Reverse Proxy</product>
// <license>Microsoft Public License (Ms-PL)</license>
// <agreement>
// This software, as defined above in <product />, is copyrighted by the <author /> and the <company /> 
// and is licensed for use under <license />, all defined above.
// 
// This copyright notice may not be removed and if this <product /> or any parts of it are used any other
// packaged software, attribution needs to be given to the author, <author />.  This can be in the form of a textual
// message at program startup or in documentation (online or textual) provided with the packaged software.
// </agreement>
// <product_url>http://www.managedfusion.com/products/url-rewriter/</product_url>
// <license_url>http://www.managedfusion.com/products/url-rewriter/license.aspx</license_url>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Framework.MVC.Captcha;

namespace Framework.MVC.Extensions
{
    /// <summary>
    /// Contains extensions for captcha control.
    /// </summary>
    public static class CaptchaExtensions
    {
        /// <summary>
        /// Captchas the text box.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of textbox.</param>
        /// <returns>Captcha text box.</returns>
        public static string CaptchaTextBox(this HtmlHelper helper, string name)
        {
            return
                String.Format(
                    @"<input type=""text"" id=""{0}"" name=""{0}"" value="""" maxlength=""{1}"" autocomplete=""off"" />",
                    name,
                    Captcha.CaptchaImage.TextLength);
        }

        /// <summary>
        /// Generates the captcha image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <returns>
        /// Returns the <see cref="Uri"/> for the generated <see cref="CaptchaImage"/>.
        /// </returns>
        public static string CaptchaImage(this HtmlHelper helper, int height, int width)
        {
            var image = new CaptchaImage
                            {
                                Height = height,
                                Width = width,
                            };

            HttpRuntime.Cache.Add(
                image.UniqueId,
                image,
                null,
                DateTime.Now.AddSeconds(Captcha.CaptchaImage.CacheTimeOut),
                Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable,
                null);

            var stringBuilder = new StringBuilder(256);
            stringBuilder.Append("<input type=\"hidden\" name=\"captcha-guid\" value=\"");
            stringBuilder.Append(image.UniqueId);
            stringBuilder.Append("\" />");
            stringBuilder.AppendLine();
            stringBuilder.Append("<img src=\"");
            stringBuilder.Append("core/captcha.ashx?guid=" + image.UniqueId);
            stringBuilder.Append("\" alt=\"CAPTCHA\" width=\"");
            stringBuilder.Append(width);
            stringBuilder.Append("\" height=\"");
            stringBuilder.Append(height);
            stringBuilder.Append("\" />");

            return stringBuilder.ToString();
        }
    }
}
