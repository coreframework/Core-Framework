using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.WebContent
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String  AreaName
        {
            get { return "WebContent"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.WebContentSections", "admin/sections", new { controller = "Section", action = "Show", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.WebContentSections.DynamicGridData", "admin/sections/load-data", new { controller = "Section", action = "LoadData", id = String.Empty });
            context.MapRoute("Admin.WebContentSections.New", "admin/sections/new", new { controller = "Section", action = "New", id = String.Empty });
            /*  context.MapRoute("Admin.ContentPages.DynamicGridData", "admin/content-pages/DynamicGridData", new { controller = "ContentPage", action = "DynamicGridData", id = String.Empty });
              context.MapRoute("Admin.ViewContentPage", "admin/content-page/view-{id}", new { controller = "ContentPage", action = "ShowById", id = String.Empty });
              context.MapRoute("Admin.EditContentPage", "admin/content-page/edit-{id}", new { controller = "ContentPage", action = "Edit", id = String.Empty });
              context.MapRoute("Admin.ChangeContentPageLanguage", "admin/content-page/change-language", new { controller = "ContentPage", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
              context.MapRoute("Admin.NewContentPage", "admin/content-page/new", new { controller = "ContentPage", action = "New", id = String.Empty });
              context.MapRoute("Admin.RemoveContentPage", "admin/content-page/remove-{id}", ContentPagesMVC.ContentPage.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });*/
        }
    }
}