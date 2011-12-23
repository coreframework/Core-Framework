using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.OpenIDLogin
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "OpenIDLogin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //widget routs
            context.MapRoute("OpenIDLoginWidget.View", "openid-login-widget/view", OpenIDLoginMVC.OpenIDLoginWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("OpenIDLoginWidget.PostForm", "openid-login-widget/create-session", OpenIDLoginMVC.OpenIDLoginWidget.CreateUserSession());
            context.MapRoute("OpenIDLoginWidget.Logout", "openid-login-widget/delete-session", OpenIDLoginMVC.OpenIDLoginWidget.DeleteUserSession(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("OpenIDLoginWidget.EditWidget", "openid-login-widget/edit", OpenIDLoginMVC.OpenIDLoginWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("OpenIDLoginWidget.UpdateWidget", "openid-login-widget/update", OpenIDLoginMVC.OpenIDLoginWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("OpenIDLoginWidget.XRDS", "openid-login-widget/xrds", OpenIDLoginMVC.OpenIDLoginWidget.Xrds());
        }
    }
}