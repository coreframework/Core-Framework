using System.Linq;
using Core.Framework.Plugins.Web;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Helpers
{
    public static class WebContentWidgetHelper
    {
        #region Helper Methods

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static WebContentWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentWidgetService>();
            return widgetService.Find(instance.InstanceId ?? 0);
        }

        /// <summary>
        /// Binds the listing model.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static WidgetListingModel BindListingModel(WebContentWidget widget, int currentPage)
        {
            var articleService = ServiceLocator.Current.GetInstance<IArticleService>();
            var categories = widget.Categories.Select(selectedCategory => selectedCategory.Category.Id).ToList();

            var articleCriteria = articleService.GetArticlesCriteria(categories);
            
            var totalItems = articleService.Count(articleCriteria);
            if (totalItems < widget.ItemsNumber * (currentPage - 1))
            {
                 currentPage = 1;
            }

            var result = new WidgetListingModel
                             {
                                 TotalItemsCount = totalItems,
                                 Articles =
                                     articleCriteria.SetFirstResult((currentPage - 1) * widget.ItemsNumber).SetMaxResults(widget.ItemsNumber).List<Article>(),
                                 CurrentPage = currentPage
                             };
            return result;
        }

        /// <summary>
        /// Saves the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static WebContentWidgetViewModel SaveWidget(WebContentWidgetViewModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentWidgetService>();
            var widget = new WebContentWidget();
            if (model.Id > 0)
            {
                widget = widgetService.Find(model.Id);
            }

            var viewModel = model.MapTo(widget);

            if (widget != null)
            {
                widgetService.Save(viewModel);
            }

            return new WebContentWidgetViewModel().MapFrom(viewModel);
        }

        #endregion
    }
}