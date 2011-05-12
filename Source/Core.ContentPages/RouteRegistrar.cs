using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Framework.MEF.Contracts.Web;
using Framework.MVC.Routing;

namespace Core.ContentPages
{
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 50)]
    public class RouteRegistrar : IRouteRegistrar
    {
        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
          
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Admin.ContentPages", "admin/content-pages", new { controller = "ContentPage", action = "ShowAll", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            routes.MapRoute("Admin.ViewContentPage", "admin/content-pages/view-{id}", new { controller = "ContentPage", action = "ShowById", id = "" });
            routes.MapRoute("Admin.EditContentPage", "admin/content-pages/edit-{id}", new { controller = "ContentPage", action = "Edit", id = "" });
            routes.MapRoute("Admin.NewContentPage", "admin/content-pages/new", new { controller = "ContentPage", action = "New", id = "" });
            
         //   routes.MapRoute("Admin.RemoveContentPage", "admin/content-pages", new { controller = "ContentPage", action = "Remove", id = "" });

            routes.MapRoute("Admin.RemoveContentPage", "admin/content-pages/remove-{id}", ContentPagesMVC.ContentPage.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}
