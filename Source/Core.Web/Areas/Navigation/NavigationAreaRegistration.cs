using System;
using System.Web.Mvc;
using Framework.MVC.Routing;

namespace Core.Web.Areas.Navigation
{
    public class NavigationAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(null, String.Empty, MVC.Navigation.SiteMap.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, MVC.Navigation.SiteMap.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "navigation/update-site-map", MVC.Navigation.SiteMap.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, String.Empty, MVC.Navigation.ListMenu.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, MVC.Navigation.ListMenu.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "navigation/update-list-menu", MVC.Navigation.ListMenu.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, String.Empty, MVC.Navigation.Breadcrumbs.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, MVC.Navigation.Breadcrumbs.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "navigation/update-breadcrumbs", MVC.Navigation.Breadcrumbs.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }

        public override string AreaName
        {
            get { return "Navigation"; }
        }
    }
}