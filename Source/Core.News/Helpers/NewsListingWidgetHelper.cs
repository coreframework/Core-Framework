using Core.Framework.Plugins.Web;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Helpers
{
    public static class NewsListingWidgetHelper
    {
        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns></returns>
        public static NewsListingWidgetViewModel BindWidgetModel(ICoreWidgetInstance instance, int currentPage)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsListingWidgetService>();

            var widget = widgetService.Find(instance.InstanceId ?? 0);
            if (widget != null)
            {
                var model = new NewsListingWidgetViewModel()
                                {
                                    CurrentPage = currentPage
                                };
                return model.MapFrom(widget);
            }

            return null;
        }

        /// <summary>
        /// Saves the content viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static NewsListingWidgetEditModel SaveContentViewerWidget(NewsListingWidgetEditModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsListingWidgetService>();
            var newsViewer = model.MapTo(new NewsListingWidget());
            widgetService.Save(newsViewer);
            
            return new NewsListingWidgetEditModel().MapFrom(newsViewer);
        }
    }
}