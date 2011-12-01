using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.Profiles
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String  AreaName
        {
            get { return "Profiles"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //widget routs
            context.MapRoute("LoginWidget.View", "login-widget/view", ProfilesMVC.LoginWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginWidget.PostForm", "login-widget/create-session", ProfilesMVC.LoginWidget.CreateUserSession(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginWidget.Logout", "login-widget/delete-session", ProfilesMVC.LoginWidget.DeleteUserSession(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });

            context.MapRoute("Admin.ProfileTypes", "admin/profiles", new { controller = "ProfileType", action = "Show", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/profiles/load-data", new { controller = "ProfileType", action = "LoadData", id = String.Empty });
            context.MapRoute(null, "admin/profiles/new", new { controller = "ProfileType", action = "New", id = String.Empty });
            context.MapRoute(null, "admin/profiles/details/{profileTypeId}", new { controller = "ProfileType", action = "Edit", profileTypeId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/profiles/change-language", new { controller = "ProfileType", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/profiles/save", ProfilesMVC.ProfileType.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/profiles/remove/{profileTypeId}", ProfilesMVC.ProfileType.Remove());

        }
    }
}