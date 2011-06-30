using System;
using System.Collections.Generic;
using System.Web;
using Core.Framework.MEF.Web;
using Core.Web.NHibernate.Contracts;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Handlers
{
    public class CssHandler : IHttpHandler
    {
        #region Constants

        private const String CommonPackage = "common";

        private const String SinglePackage = "package";

        private const String CssContentType = "text/css";

        #endregion

        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            String common = context.Request.QueryString[CommonPackage];
            String package = context.Request.QueryString[SinglePackage];
            context.Response.ContentType = CssContentType;

            context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
            //context.Response.Cache.SetCacheability(HttpCacheability.Public);

            if (!String.IsNullOrEmpty(common))
            {
                var ids = common.Split('_').ToList();
                var idsLong = new List<long>();
                ids.ForEach(t=> { long id; if (long.TryParse(t,out id)) idsLong.Add(id);});
                var plugins = ServiceLocator.Current.GetInstance<IPluginService>().FindPluginsByIds(idsLong);
                var file = AssetsExtensions.CssPluginPackHelper(context, Application.Plugins.Where(t=>plugins.Any( temp => temp.Identifier == t.Identifier)), common, null);
                if (!String.IsNullOrEmpty(file))
                {
                    context.Response.WriteFile(file);
                    return;
                }
            }
            if (!String.IsNullOrEmpty(package))
            {
                var file = AssetsHelper.BuildPluginCssPack(Application.Plugins.Where(t => t.Identifier == package).SingleOrDefault(),context.Request.PhysicalApplicationPath);
                if (!String.IsNullOrEmpty(file))
                {
                    context.Response.WriteFile(file);
                    return;
                }
            }
        }

        #endregion
    }
}
