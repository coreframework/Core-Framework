using System.Collections.Generic;
using System.Linq;
using Core.Framework.Plugins.Web;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Omu.ValueInjecter;
using Products.Models;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.Helpers
{
    public class ProductViewerWidgetHelper
    {
        #region Methods

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ProductWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProductWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }

        /// <summary>
        /// Binds the widget view model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static ProductViewWidgetModel BindWidgetViewModel(ICoreWidgetInstance instance, int currentPage)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProductWidgetService>();
            var productService = ServiceLocator.Current.GetInstance<IProductService>();
           // var categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();

            var widget = widgetService.Find(instance.InstanceId ?? 0);
           
            if (widget != null && widget.Categories !=null)
            {
                var model = new ProductViewWidgetModel();

                model.Id = widget.Id;
                model.PageSize = widget.PageSize;
              
                model.CategoriesId = widget.Categories.Select(t=>t.Category.Id).ToArray();
               
                //IQueryable<Product> searchQuery = productService.GetSearchQuery("")
                //    .SelectMany(prod => prod.Categories, (prod, cat) => new { prod, cat })
                //    .Where(@t => model.CategoriesId.Contains(@t.cat.Id)).Distinct()
                //    .Select(@t => @t.prod);

                ICriteria searchQuery = productService.GetProductCriteria(model.CategoriesId);
                model.TotalItemsCount = productService.GetCount(searchQuery);
                if (model.TotalItemsCount < widget.PageSize * (currentPage - 1))
                    currentPage = 1;
                model.Products = searchQuery.SetFirstResult((currentPage - 1) * widget.PageSize).SetMaxResults(widget.PageSize).List<Product>(); //searchQuery.Skip().Take().ToList();
                model.CurrentPage = currentPage;
                return model;
            }
            return null;
        }

        /// <summary>
        /// Saves the content viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static ProductWidgetModel SaveContentViewerWidget(ProductWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProductWidgetService>();
            var productViewer = model.MapTo(new ProductWidget());
            //var noselected = model.Categories.Where(t => !model.CategoriesId.Contains(t.Id)).ToList();
            //foreach (var category in noselected)
            //{
            //    model.Categories.Remove(category);
            //}

            //foreach (var selid in model.CategoriesId)
            //{
            //    string selid1 = selid;
            //    if (!product.Categories.Any(t => t.Id.ToString() == selid1))
            //    {
            //        long selectedID;
            //        if (long.TryParse(selid1, out selectedID))
            //        {
            //            product.Categories.Add(categoryService.Find(selectedID));
            //        }
            //    }
            //}

            widgetService.Save(productViewer);
            return new ProductWidgetModel().MapFrom(productViewer);
        }

        /// <summary>
        /// Clones the product viewer widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneProductViewerWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProductWidgetService>();
            var sourceWidget = BindWidgetModel(instance);

            if (sourceWidget != null)
            {
                var targetWidget = (ProductWidget)new ProductWidget().InjectFrom<CloneEntityInjection>(sourceWidget);

                sourceWidget.Categories.AsParallel().ForAll(category =>
                {
                    var productCategory = (ProductWidgetToCategory)new ProductWidgetToCategory().InjectFrom<CloneEntityInjection>(category);
                    productCategory.ProductWidget = targetWidget;

                    targetWidget.AddCategory(productCategory);
                });

                if (widgetService.Save(targetWidget))
                {
                    return targetWidget.Id;
                }
            }
            return null;
        }

        #endregion
    }
}