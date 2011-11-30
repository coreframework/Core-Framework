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
        }
    }
}