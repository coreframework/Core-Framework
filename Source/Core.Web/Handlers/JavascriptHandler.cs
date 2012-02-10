using System;
using System.IO;
using System.Linq;
using System.Web;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Contracts;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Handlers
{
    public class JavascriptHandler : IHttpHandler
    {
        #region Constants

        private const String JsContentType = "application/javascript";

        private const String PageIdParam = "pageId";

        private const String PluginIdParam = "id";

        private const String OuterPluginTypeName = "outer";

        private const String ScriptsPath = "Scripts";

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
            String plugin = context.Request.QueryString[PluginIdParam];
            long pageId;
            if (!String.IsNullOrEmpty(plugin) && Int64.TryParse(context.Request.QueryString[PageIdParam], out pageId))
            {
                var pageService = ServiceLocator.Current.GetInstance<IPageService>();

                var activePage = pageService.Find(pageId);

                if (activePage != null)
                {
                    if (activePage.Widgets.Where(widget=>widget.Widget.Plugin.Identifier==plugin).Count()==1)
                    {
                        ICorePlugin currentPlugin = Application.Plugins.FirstOrDefault(t => t.Identifier == plugin);

                        if (currentPlugin!=null)
                        {
                            context.Response.ContentType = JsContentType;
                            context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
                            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));

                            var path = AssetsHelper.GetPluginInnerJsPath(currentPlugin);

                            if (File.Exists(path))
                            {
                                context.Response.WriteFile(path);
                            }

                            var externalScripts = AssetsHelper.GetPluginJsPackFiles(currentPlugin.JsPack, Path.Combine(currentPlugin.PluginDirectory, currentPlugin.CssJsConfigPath),
                                                                                   OuterPluginTypeName);

                            foreach (var fullPath in
                                externalScripts.Select(externalScript => Path.Combine(context.Request.PhysicalApplicationPath, String.Format("{0}\\{1}", ScriptsPath, externalScript))).Where(File.Exists))
                            {
                                context.Response.WriteFile(fullPath);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}