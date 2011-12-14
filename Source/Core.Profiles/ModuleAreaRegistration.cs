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
            context.MapRoute("RegistrationWidget.View", "registration-widget/view", ProfilesMVC.RegistrationWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("RegistrationWidget.Register", "registration-widget/register", ProfilesMVC.RegistrationWidget.RegisterUser(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("RegistrationWidget.Edit", "registration-widget/edit", ProfilesMVC.RegistrationWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("RegistrationWidget.Update", "registration-widget/update", ProfilesMVC.RegistrationWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("ProfileWidget.View", "profile-widget/view", ProfilesMVC.ProfileWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("ProfileWidget.Register", "profile-widget/save", ProfilesMVC.ProfileWidget.SaveUser(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("ProfileWidget.Edit", "profile-widget/edit", ProfilesMVC.ProfileWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("ProfileWidget.Update", "profile-widget/update", ProfilesMVC.ProfileWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.ProfileTypes", "admin/profiles", new { controller = "ProfileType", action = "Show", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/profiles/load-data", new { controller = "ProfileType", action = "LoadData", id = String.Empty });
            context.MapRoute(null, "admin/profiles/new", new { controller = "ProfileType", action = "New", id = String.Empty });
            context.MapRoute(null, "admin/profiles/details/{profileTypeId}", new { controller = "ProfileType", action = "Edit", profileTypeId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/profiles/change-language", new { controller = "ProfileType", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/profiles/save", ProfilesMVC.ProfileType.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/profiles/remove/{profileTypeId}", ProfilesMVC.ProfileType.Remove());

            context.MapRoute(null, "admin/profiles/{profileTypeId}/elements", new { controller = "ProfileElement", action = "Show", area = AreaName, profileTypeId = UrlParameter.Optional, parentController = "ProfileType" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/profiles/{profileTypeId}/load-data", new { controller = "ProfileElement", action = "LoadData", id = String.Empty, area = AreaName });
            context.MapRoute(null, "admin/profiles/elements/update-positions", ProfilesMVC.ProfileElement.UpdateProfileElementPosition());

            context.MapRoute(null, "admin/profiles/{profileTypeId}/headers/new", new { controller = "ProfileHeader", action = "New", area = AreaName, profileTypeId = UrlParameter.Optional, parentController = "ProfileType" });
            context.MapRoute(null, "admin/profiles/{profileTypeId}/headers/remove", new { controller = "ProfileHeader", action = "Remove", area = AreaName, profileTypeId = UrlParameter.Optional });
            context.MapRoute(null, "admin/profiles/{profileTypeId}/headers/details/{profileHeaderId}", new { controller = "ProfileHeader", action = "Edit", area = AreaName, profileTypeId = UrlParameter.Optional, profileHeaderId = UrlParameter.Optional, parentController = "ProfileType" });
            context.MapRoute("Admin.ChangeProfileHeaderLanguage", "admin/profiles/header/change-language", new { controller = "ProfileHeader", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.SaveProfileHeader", "admin/profiles/{profileTypeId}/headers/save", new { controller = "ProfileHeader", action = "Save", id = String.Empty, parentController = "ProfileType" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, "admin/profiles/{profileTypeId}/elements/new", new { controller = "ProfileElement", action = "New", area = AreaName, profileTypeId = UrlParameter.Optional, parentController = "ProfileType" });
            context.MapRoute(null, "admin/profiles/{profileTypeId}/elements/remove", new { controller = "ProfileElement", action = "Remove", area = AreaName, profileTypeId = UrlParameter.Optional });
            context.MapRoute(null, "admin/profiles/{profileTypeId}/elements/details/{profileElementId}", new { controller = "ProfileElement", action = "Edit", area = AreaName, profileTypeId = UrlParameter.Optional, profileElementId = UrlParameter.Optional, parentController = "ProfileType" });
            context.MapRoute("Admin.ChangeProfileElementLanguage", "admin/profiles/element/change-language", new { controller = "ProfileElement", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.SaveProfileElement", "admin/profiles/{profileTypeId}/elements/save", new { controller = "ProfileElement", action = "Save", id = String.Empty, parentController = "ProfileType" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}