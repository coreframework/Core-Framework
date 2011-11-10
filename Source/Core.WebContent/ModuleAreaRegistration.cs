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
            context.MapRoute(null, "admin/sections/load-data", new { controller = "Section", action = "LoadData", id = String.Empty });
            context.MapRoute(null, "admin/sections/new", new { controller = "Section", action = "New", id = String.Empty });
            context.MapRoute(null, "admin/sections/details/{sectionId}", new { controller = "Section", action = "Edit", sectionId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/sections/change-language", new { controller = "Section", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/sections/save", WebContentMVC.Section.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/sections/permissions/{sectionId}", new { controller = "Section", action = "ShowPermissions", area = AreaName, sectionId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/sections/apply-permissions", WebContentMVC.Section.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.WebContentCategories", "admin/web-content-categories", new { controller = "WebContentCategory", action = "Show", Area = AreaName, id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/web-content-categories/load-data", new { controller = "WebContentCategory", action = "LoadData", id = String.Empty });
            context.MapRoute(null, "admin/web-content-categories/new", new { controller = "WebContentCategory", action = "New", id = String.Empty });
            context.MapRoute(null, "admin/web-content-categories/details/{categoryId}", new { controller = "WebContentCategory", action = "Edit", categoryId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/web-content-categories/change-language", new { controller = "WebContentCategory", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/web-content-categories/save", WebContentMVC.WebContentCategory.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/web-content-categories/permissions/{categoryId}", new { controller = "WebContentCategory", action = "ShowPermissions", area = AreaName, categoryId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/web-content-categories/apply-permissions", WebContentMVC.WebContentCategory.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}