using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.News.Controllers;
using Framework.MVC.Routing;

namespace Core.News
{//todo rewrite actions
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "News"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.News", "admin/news", new { controller = "News", action = "ShowAll", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.News.DynamicGridData", "admin/news/DynamicGridData", new { controller = "News", action = "DynamicGridData", id = "" });
            context.MapRoute("Admin.ViewNewsArticle", "admin/news/view-{id}", new { controller = "News", action = "ShowById", id = "" });
            context.MapRoute("Admin.EditNewsArticle", "admin/news/edit-{id}", new { controller = "News", action = "Edit", id = "" });
            context.MapRoute("Admin.ChangeNewsLanguage", "admin/news/change-language", new { controller = "News", action = "ChangeLanguage", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.NewNewsArticle", "admin/news/new", new { controller = "News", action = "New", id = "" });
            context.MapRoute("Admin.RemoveNewsArticle", "admin/news/remove-{id}", NewsMVC.News.Remove(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.News.Categories", "admin/news/categories/{id}", new { controller = "News", action = "Categories", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.News.CategoriesDynamicGridData", "admin/news/{id}/categories/ProductCategoriesDynamicGridData", NewsMVC.News.NewsCategoriesDynamicGridData());
            context.MapRoute("Admin.News.Update.Categories", "admin/news/categories/{id}/updatenewscategories", NewsMVC.News.UpdateCategories());

            //category
            context.MapRoute("Admin.NewsCategory", "admin/newscategories", new { controller = "NewsCategory", action = "ShowAll", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.NewsCategory.DynamicGridData", "admin/newscategories/DynamicGridData", new { controller = "NewsCategory", action = "DynamicGridData", id = "" });
            context.MapRoute("Admin.ViewNewsCategory", "admin/newscategories/view-{id}", new { controller = "NewsCategory", action = "ShowById", id = "" });
            context.MapRoute("Admin.EditNewsCategory", "admin/newscategories/edit-{id}", new { controller = "NewsCategory", action = "Edit", id = "" });
            context.MapRoute("Admin.NewNewsCategory", "admin/newscategories/new", new { controller = "NewsCategory", action = "New", id = "" });
            context.MapRoute("Admin.RemoveNewsCategory", "admin/newscategories/remove-{id}", NewsMVC.NewsCategory.Remove());
            context.MapRoute("Admin.ChangeNewsCategoryLanguage", "admin/newscategories/change-language", new { controller = "NewsCategory", action = "ChangeLanguage", id = "" }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            
            //widget routs
            context.MapRoute(null, String.Empty, NewsMVC.NewsViewerWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, NewsMVC.NewsViewerWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "news-widget/update", NewsMVC.NewsViewerWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}