using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.ContentPages
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String  AreaName
        {
            get { return "ContentPage"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.ContentPages", "admin/content-pages", new { controller = "ContentPage", action = "ShowAll", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.ContentPages.DynamicGridData", "admin/content-pages/DynamicGridData", new { controller = "ContentPage", action = "DynamicGridData", id = String.Empty });
            context.MapRoute("Admin.ViewContentPage", "admin/content-page/view-{id}", new { controller = "ContentPage", action = "ShowById", id = String.Empty });
            context.MapRoute("Admin.EditContentPage", "admin/content-page/edit-{id}", new { controller = "ContentPage", action = "Edit", id = String.Empty });
            context.MapRoute("Admin.ChangeContentPageLanguage", "admin/content-page/change-language", new { controller = "ContentPage", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.NewContentPage", "admin/content-page/new", new { controller = "ContentPage", action = "New", id = String.Empty });
            context.MapRoute("Admin.RemoveContentPage", "admin/content-page/remove-{id}", ContentPagesMVC.ContentPage.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            
            //widget routs
            context.MapRoute(null, String.Empty, ContentPagesMVC.ContentViewerWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, ContentPagesMVC.ContentViewerWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "content-widget/update", ContentPagesMVC.ContentViewerWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, "content-widget-details", ContentPagesMVC.ContentDetailsWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}