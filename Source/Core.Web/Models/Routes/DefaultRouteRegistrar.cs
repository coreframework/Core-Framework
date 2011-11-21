using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Framework.MEF.Contracts.Web;
using Framework.Mvc.Routing;

namespace Core.Web.Models.Routes
{
    /// <summary>
    /// Registers the default MVC routes.
    /// </summary>
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 100)]
    public class DefaultRouteRegistrar : IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers any routes to be ignored by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.cssx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.jsx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
        }

        /// <summary>
        /// Registers any routes to be used by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("UploadImage", "upload/image", MVC.Upload.Image());
            routes.MapRoute("UploadFile", "upload/file", MVC.Upload.File()); 

            routes.MapRoute(null, "pages/changelayout/{pageId}/{layoutTemplateId}", MVC.Pages.ChangeLayout());
            routes.MapRoute(null, "pages/show-layout-setting", MVC.Pages.ShowLayoutSettingsForm());
            routes.MapRoute(null, "pages/update-layout-setting", MVC.Pages.UpdateLayoutSettingsForm());
            routes.MapRoute(null, "pages/show-page-lookandfeel", MVC.Pages.ShowPageLookAndFeel());
            routes.MapRoute(null, "pages/update-page-lookandfeel", MVC.Pages.UpdatePageLookAndFeel());
            routes.MapRoute(null, "pages/show-page-css", MVC.Pages.ShowPageCSS());
            routes.MapRoute(null, "pages/update-page-css", MVC.Pages.UpdatePageCSS());
            routes.MapRoute(null, "pages/show-widget-settings", MVC.Pages.ShowSettings());
            routes.MapRoute(null, "pages/show-widget-lookandfeel", MVC.Pages.ShowWidgetLookAndFeel());
            routes.MapRoute(null, "pages/show-page-permissions", MVC.Pages.ShowPagePermissions());
            routes.MapRoute(null, "pages/update-widget-lookandfeel", MVC.Pages.UpdateWidgetLookAndFeel());
            routes.MapRoute(null, "pages/show-widget-css", MVC.Pages.ShowWidgetCSS());
            routes.MapRoute(null, "pages/show-widget-permissions", MVC.Pages.ShowWidgetPermissions());
            routes.MapRoute(null, "pages/apply-widget", MVC.Pages.ApplyWidgetPermissions());
            routes.MapRoute(null, "pages/update-widget-css", MVC.Pages.UpdateWidgetCSS());
            routes.MapRoute(null, "pages/update-widget-instance", MVC.Pages.UpdatePageWidgetInstance());
            routes.MapRoute(null, "pages/update-widgets-positions", MVC.Pages.UpdateWidgetsPositions());
            routes.MapRoute(null, "pages/save-page-settings", MVC.Pages.SavePageCommonSettings());
            routes.MapRoute(null, "pages/update-page-settings", MVC.Pages.UpdatePageCommonSettings());
            routes.MapRoute(null, "pages/apply-permissions", MVC.Pages.ApplyPagePermissions());
            routes.MapRoute(null, "pages/show-page-settings/{pageId}", MVC.Pages.ShowPageCommonSettings());
            routes.MapRoute(null, "pages/page-change-language", MVC.Pages.ChangeLanguage(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            routes.MapRoute(null, "pages/add-page/{parentPageId}", MVC.Pages.CreateNewPage());
            routes.MapRoute(null, "pages/update-page-position", MVC.Pages.UpdatePagePosition());
            routes.MapRoute(null, "pages/add-widget/{pageId}/{widgetId}", MVC.Pages.AddWidget());
            routes.MapRoute(null, "pages/menu", MVC.Pages.Index());
            routes.MapRoute(null, "pages/show-change-layout-form/{pageId}", MVC.Pages.ShowChangeLayoutForm());
            routes.MapRoute(null, "pages/available-widgets", MVC.Pages.ShowAvailableWidgets());
            routes.MapRoute(null, "pages/remove-widget/{pageWidgetId}", MVC.Pages.RemovePageWidget());
            routes.MapRoute(null, "pages/remove/{pageId}", MVC.Pages.RemovePage());
            routes.MapRoute(null, "pages/unlink/{pageId}", MVC.Pages.Unlink());
            routes.MapRoute("Pages.ChangePageMode", "pages/change-page-mode", MVC.Pages.ChangePageMode(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            routes.MapRoute("Pages.Error", "error", MVC.Error.Index());
            routes.MapRoute("PlaceHolderWidget.View", "place-holder-widget/view", MVC.PlaceHolderWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            routes.MapRoute("PlaceHolderWidget.Replace", "place-holder-widget/replace", MVC.PlaceHolderWidget.ReplaceWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            routes.MapRoute("Pages.Show", "pages/{url}", MVC.Pages.Show(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            routes.MapRoute("PageTemplates.Show", "page-templates/{url}", MVC.PageTemplates.Show(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = String.Empty });
            routes.MapRoute("Login", "users/sign-in", MVC.Users.NewUserSession());

          
        }

        #endregion
    }
}