using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.FormLogin
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "FormLogin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //widget routs
            context.MapRoute("LoginWidget.View", "login-widget/view", FormLoginMVC.LoginWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginWidget.PostForm", "login-widget/create-session", FormLoginMVC.LoginWidget.CreateUserSession(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginWidget.Logout", "login-widget/delete-session", FormLoginMVC.LoginWidget.DeleteUserSession(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("LoginWidget.EditWidget", "login-widget/edit", FormLoginMVC.LoginWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginWidget.UpdateWidget", "login-widget/update", FormLoginMVC.LoginWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}