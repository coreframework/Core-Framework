using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.Languages
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "Languages"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.Languages", "admin/languages", new { controller = "Languages", action = "ShowAll", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Languages.DynamicGridData", "admin/languages/DynamicGridData", new { controller = "Languages", action = "DynamicGridData", id = String.Empty });
            context.MapRoute("Admin.ViewLanguage", "admin/languages/view-{id}", new { controller = "Languages", action = "ShowById", id = String.Empty });
            context.MapRoute("Admin.EditLanguage", "admin/languages/edit-{id}", new { controller = "Languages", action = "Edit", id = String.Empty });
            context.MapRoute("Admin.NewLanguage", "admin/languages/new", new { controller = "Languages", action = "New", id = String.Empty });
            context.MapRoute("Admin.Languages.Create", "admin/languages", new { controller = "Languages", action = "Create", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Put) });
            context.MapRoute("Admin.SetAsDefault", "admin/languages/set-as-default-{id}", LanguagesMVC.Languages.SetAsDefault(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.RemoveLanguage", "admin/languages/remove-{id}", LanguagesMVC.Languages.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });

            //widget routs
            context.MapRoute(null, String.Empty, LanguagesMVC.LanguageSelectorWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "language-selector-widget/change-language", LanguagesMVC.LanguageSelectorWidget.ChangeLanguage(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}