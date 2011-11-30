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
            context.MapRoute(null, String.Empty, ProfilesMVC.LoginWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}