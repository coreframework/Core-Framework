using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.LoginWorkflow
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "LoginWorkflow"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //widget routs
            context.MapRoute("LoginHolderWidget.View", "login-holder-widget/view", new { controller = "LoginHolderWidget", action = "ViewWidget", area = AreaName }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginHolderWidget.EditWidget", "login-holder-widget/edit", new { controller = "LoginHolderWidget", action = "EditWidget", area = AreaName }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("LoginHolderWidget.UpdateWidget", "login-holder-widget/update", new { controller = "LoginHolderWidget", action = "UpdateWidget", area = AreaName }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}