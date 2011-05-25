using System;
using System.Web;
using Core.Web.Models;
using Framework.Core;

namespace Core.Web.Modules
{
    public class PageModeModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequestHandler;
        }

        private static void BeginRequestHandler(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            HttpCookie pageModeCookie = context.Request.Cookies.Get(Constants.PageModeCookieName);
            if(pageModeCookie != null && !String.IsNullOrEmpty(pageModeCookie.Value))
            {
                PageMode userPageMode = (PageMode)Enum.Parse(typeof (PageMode), pageModeCookie.Value);
                HttpContext.Current.Items.Add(typeof(PageMode), userPageMode);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(Constants.PageModeCookieName, PageMode.View.ToString()));
                HttpContext.Current.Items.Add(typeof(PageMode), PageMode.View);
            }
            
        }

        public void Dispose()
        {}
    }
}