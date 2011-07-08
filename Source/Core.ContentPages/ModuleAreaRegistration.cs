using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.MVC.Routing;

namespace Core.ContentPages
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "ContentPage"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.ContentPages", "admin/content-pages", new { controller = "ContentPage", action = "ShowAll", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.ContentPages.DynamicGridData", "admin/content-pages/DynamicGridData", new { controller = "ContentPage", action = "DynamicGridData", id = "" });
            context.MapRoute("Admin.ViewContentPage", "admin/content-pages/view-{id}", new { controller = "ContentPage", action = "ShowById", id = "" });
            context.MapRoute("Admin.EditContentPage", "admin/content-pages/edit-{id}", new { controller = "ContentPage", action = "Edit", id = "" });
            context.MapRoute("Admin.NewContentPage", "admin/content-pages/new", new { controller = "ContentPage", action = "New", id = "" });
            context.MapRoute("Admin.RemoveContentPage", "admin/content-pages/remove-{id}", ContentPagesMVC.ContentPage.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            
            //widget routs
            context.MapRoute(null, String.Empty, ContentPagesMVC.ContentViewerWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, ContentPagesMVC.ContentViewerWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "content-widget/update", ContentPagesMVC.ContentViewerWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}