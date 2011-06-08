using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.MVC.Routing;

namespace Core.Forms
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Forms"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.Forms", "admin/forms", FormsMVC.Forms.ShowAll(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/form-{formId}/permissions", FormsMVC.Forms.ShowPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/forms/apply-permissions", FormsMVC.Forms.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            
            /*    context.MapRoute("Admin.ViewContentPage", "admin/content-pages/view-{id}", new { controller = "ContentPage", action = "ShowById", id = "" });
            context.MapRoute("Admin.EditContentPage", "admin/content-pages/edit-{id}", new { controller = "ContentPage", action = "Edit", id = "" });
            context.MapRoute("Admin.NewContentPage", "admin/content-pages/new", new { controller = "ContentPage", action = "New", id = "" });
            context.MapRoute("Admin.RemoveContentPage", "admin/content-pages/remove-{id}", ContentPagesMVC.ContentPage.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });*/

            //widget routs
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "form-widget/update", FormsMVC.FormsBuilderWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}