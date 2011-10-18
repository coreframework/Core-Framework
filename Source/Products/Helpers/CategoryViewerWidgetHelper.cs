using System;
using System.Linq;
using Core.Framework.Plugins.Web;
using Microsoft.Practices.ServiceLocation;
using Products.Models;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.Helpers
{
    public static class CategoryViewerWidgetHelper
    {
        #region Methods

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static CategoryWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<ICategoryWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }

        /// <summary>
        /// Binds the widget view model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static CategoryViewWidgetModel BindWidgetViewModel(ICoreWidgetInstance instance, int currentPage)
        {
            var widgetService = ServiceLocator.Current.GetInstance<ICategoryWidgetService>();
            var categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();

            var widget = widgetService.Find(instance.InstanceId ?? 0);
           
            if (widget != null)
            {
                var model = new CategoryViewWidgetModel {Id = widget.Id, PageSize = widget.PageSize};

                IQueryable<Category> searchQuery = categoryService.GetSearchQuery(String.Empty);
                model.TotalItemsCount = categoryService.GetCount(searchQuery);
                model.Categories = widget.PageSize > 0 ? searchQuery.Skip((currentPage - 1) * widget.PageSize).Take(widget.PageSize).ToList() : searchQuery.ToList();
                model.CurrentPage = currentPage;
                return model;
            }
            return null;
        }

        /// <summary>
        /// Saves the categories viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static CategoryWidgetModel SaveContentViewerWidget(CategoryWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<ICategoryWidgetService>();
            var categoryViewer = model.MapTo(new CategoryWidget());
            widgetService.Save(categoryViewer);
            return new CategoryWidgetModel().MapFrom(categoryViewer);
        }


        /// <summary>
        /// Binds the category model.
        /// </summary>
        /// <param name="id">The category id.</param>
        public static CategoryModel BindModel(long id)
        {
            var categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
            var category = categoryService.Find(id);
            if(category != null)
            {
                var productService = ServiceLocator.Current.GetInstance<IProductService>();

                var categoryModel = new CategoryModel
                                        {
                                            Id = category.Id,
                                            Title = category.Title,
                                            Description = category.Description,
                                            Products = productService.GetProductsByCategory(category).ToList()
                                        };
                return categoryModel;
            }

          
            return null;
        }

        #endregion
    }
}