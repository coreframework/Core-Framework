using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.MVC.Routing;

namespace Core.Languages
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Languages"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.Languages", "admin/languages", new { controller = "Languages", action = "ShowAll", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.ViewLanguage", "admin/languages/view-{id}", new { controller = "Languages", action = "ShowById", id = "" });
            context.MapRoute("Admin.EditLanguage", "admin/languages/edit-{id}", new { controller = "Languages", action = "Edit", id = "" });
            context.MapRoute("Admin.NewLanguage", "admin/languages/new", new { controller = "Languages", action = "New", id = "" });
            context.MapRoute("Admin.RemoveLanguage", "admin/languages/remove-{id}", LanguagesMVC.Languages.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            //widget routs
            context.MapRoute(null, String.Empty, LanguagesMVC.LanguageSelectorWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "language-selector-widget/change-language", LanguagesMVC.LanguageSelectorWidget.ChangeLanguage(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}