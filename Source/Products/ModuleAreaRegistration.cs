using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Products
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "Product"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //product
            context.MapRoute("Admin.Product", "admin/products", new { controller = "Product", action = "ShowAll", id = String.Empty } , new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Product.DynamicGridData", "admin/products/DynamicGridData", new { controller = "Product", action = "DynamicGridData", id = String.Empty });
            context.MapRoute("Admin.ViewProduct", "admin/products/view-{id}", new { controller = "Product", action = "ShowById", id = String.Empty });
            context.MapRoute("Admin.EditProduct", "admin/products/edit-{id}", new { controller = "Product", action = "Edit", id = String.Empty }); 
            context.MapRoute("Admin.NewProduct", "admin/products/new", new { controller = "Product", action = "New", id = String.Empty });
            context.MapRoute("Admin.RemoveProduct", "admin/products/remove-{id}", ProductMVC.Product.Remove());
            context.MapRoute("Admin.Product.Categories", "admin/products/categories/{id}", new { controller = "Product", action = "Categories", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Product.CategoriesDynamicGridData", "admin/products/{id}/categories/ProductCategoriesDynamicGridData", ProductMVC.Product.ProductCategoriesDynamicGridData());
            context.MapRoute("Admin.Product.Update.Categories", "admin/products/{id}/categories/UpdateCategories", ProductMVC.Product.UpdateCategories());
            context.MapRoute("Admin.ChangeProductLanguage", "admin/products/change-language", new { controller = "Product", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });


            //category
            context.MapRoute("Admin.Category", "admin/categories", new { controller = "Category", action = "ShowAll", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Category.DynamicGridData", "admin/categories/DynamicGridData", new { controller = "Category", action = "DynamicGridData", id = String.Empty });
            context.MapRoute("Admin.ViewCategory", "admin/categories/view-{id}", new { controller = "Category", action = "ShowById", id = String.Empty });
            context.MapRoute("Admin.EditCategory", "admin/categories/edit-{id}", new { controller = "Category", action = "Edit", id = String.Empty });
            context.MapRoute("Admin.NewCategory", "admin/categories/new", new { controller = "Category", action = "New", id = String.Empty });
            context.MapRoute("Admin.RemoveCategory", "admin/categories/remove-{id}", ProductMVC.Category.Remove());
            context.MapRoute("Admin.ChangeCategoryLanguage", "admin/categories/change-language", new { controller = "Category", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            //widget product routs
            context.MapRoute(null, String.Empty, ProductMVC.ProductViewerWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, ProductMVC.ProductViewerWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "product-widget/update", ProductMVC.ProductViewerWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            //widget category routs
            //context.MapRoute(null, String.Empty, ProductMVC.CategoryViewerWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            //context.MapRoute(null, String.Empty, ProductMVC.CategoryViewerWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            //context.MapRoute(null, "category-widget/update", ProductMVC.CategoryViewerWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            //context.MapRoute("Product.Category", "category-{id}", ProductMVC.CategoryViewerWidget.Category());
         
        }
    }
}