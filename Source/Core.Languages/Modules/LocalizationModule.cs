using System;
using System.Globalization;
using System.Threading;
using System.Web;
using Core.Framework.Plugins.Modules;
using Framework.Core;

namespace Core.Languages.Modules
{
    public class LocalizationModule : IPluginHttpModule
    {
        public void OnBeginRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            HttpCookie cultureCookie = context.Request.Cookies[Constants.CultureCookieName];
            if (cultureCookie != null && !String.IsNullOrEmpty(cultureCookie.Value))
            {
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo(cultureCookie.Value);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }
    }
}