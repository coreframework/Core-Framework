using System;
using Core.Framework.Plugins.Web;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Helpers
{
    public static class NewsDetailsWidgetHelper
    {
        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="newsArticleIdentifier">The news article identifier.</param>
        /// <returns></returns>
        public static NewsDetailsWidgetViewModel BindWidgetModel(ICoreWidgetInstance instance, String newsArticleIdentifier)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsDetailsWidgetService>();
            var widget = widgetService.Find(instance.InstanceId ?? 0);
            if (widget != null)
            {
                NewsArticle newsArticle = null;
                switch (widget.LinkMode)
                {
                    case  NewsDetailsLinkMode.Id:
                        {
                            long newsArticleId;
                            if (long.TryParse(newsArticleIdentifier, out newsArticleId))
                            {
                                var articleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                                newsArticle = articleService.FindPublished(newsArticleId);
                            }
                            break;
                        }
                    case NewsDetailsLinkMode.Url:
                        {
                            var articleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                            newsArticle = articleService.FindPublished(newsArticleIdentifier);
                            break;
                        }
                }
                if (newsArticle != null)
                {
                    return new NewsDetailsWidgetViewModel().MapFrom(newsArticle);
                }
            }

            return null;
        }

        /// <summary>
        /// Saves the content viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static NewsDetailsWidgetEditModel SaveContentViewerWidget(NewsDetailsWidgetEditModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsDetailsWidgetService>();
            var newsViewer = model.MapTo(new NewsDetailsWidget());
            widgetService.Save(newsViewer);

            return new NewsDetailsWidgetEditModel().MapFrom(newsViewer);
        }
    }
}