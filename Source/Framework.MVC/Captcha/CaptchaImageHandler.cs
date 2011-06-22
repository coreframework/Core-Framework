// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptchaImageHandler.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
//  Copyright (C) 2007-2008 Nicholas Berardi, Managed Fusion, LLC (nick@managedfusion.com)
//  
//  <author>Nicholas Berardi</author>
//  <author_email>nick@managedfusion.com</author_email>
//  <company>Managed Fusion, LLC</company>
//  <product>ASP.NET MVC CAPTCHA</product>
//  <license>Microsoft Public License (Ms-PL)</license>
//  <agreement>
//  This software, as defined above in <product />, is copyrighted by the <author /> and the <company /> 
//  and is licensed for use under <license />, all defined above.
//  
//  This copyright notice may not be removed and if this <product /> or any parts of it are used any other
//  packaged software, attribution needs to be given to the author, <author />.  This can be in the form of a textual
//  message at program startup or in documentation (online or textual) provided with the packaged software.
//  </agreement>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace Framework.MVC.Captcha
{
    /// <summary>
    /// Captcha image stream HttpModule. Retrieves CAPTCHA objects from cache, renders them to memory, 
    /// and streams them to the browser.
    /// </summary>
    /// <seealso href="http://www.codinghorror.com">Original By Jeff Atwood</seealso>
    public class CaptchaImageHandler : IHttpHandler
    {
        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <value>Is instance is reusable.</value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            // get the unique GUID of the captcha; this must be passed in via the querystring
            string guid = context.Request.QueryString["guid"];
            CaptchaImage ci = CaptchaImage.GetCachedCaptcha(guid);

            if (String.IsNullOrEmpty(guid) || ci == null)
            {
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "Not Found";
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            // write the image to the HTTP output stream as an array of bytes
            using (Bitmap b = ci.RenderImage())
            {
                b.Save(context.Response.OutputStream, ImageFormat.Gif);
            }

            context.Response.ContentType = "image/png";
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "OK";
            context.ApplicationInstance.CompleteRequest();
        }

        #endregion
    }
}
