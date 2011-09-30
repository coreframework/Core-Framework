using System;
using Core.Framework.Plugins.Web;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

namespace Core.News.Helpers
{
    public static class NewsViewerWidgetHelper
    {
        #region Methods

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static NewsArticleViewerWidgetModel BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();

            var widget = widgetService.Find(instance.InstanceId ?? 0);
            if (widget!=null)
                return new NewsArticleViewerWidgetModel().MapFrom(widget);

            return null;
        }

        public static NewsArticleWidget BindEditWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }

        /// <summary>
        /// Saves the content viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static NewsArticleWidgetModel SaveContentViewerWidget(NewsArticleWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();
            var newsViewer = model.MapTo(new NewsArticleWidget());
            widgetService.Save(newsViewer);
            newsViewer = model.MapTo(newsViewer);
            if (String.IsNullOrEmpty(newsViewer.Url))
                newsViewer.Url = String.Empty;
            widgetService.Save(newsViewer);
            return new NewsArticleWidgetModel().MapFrom(newsViewer);
        }

        /// <summary>
        /// Gets the back URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="widgetId">The widget id.</param>
        /// <param name="articleId">The article id.</param>
        /// <returns></returns>
        public static String GetBackUrl(String url, long widgetId, long articleId)
        {
            url = url.Replace("&" + NewsConstants.Articleid + widgetId + "=" + articleId, String.Empty);
            if (!url.Contains(NewsConstants.CurrentPage + widgetId))
            {
                url = url.Replace("?" + NewsConstants.Newsvidgetid + "=" + widgetId, String.Empty);
                if (!url.Contains("?"))
                {
                    var index = url.IndexOf('&');
                    if (index > 0)
                    {
                        var tempUrl = url.ToCharArray();
                        tempUrl[index] = '?';
                        url = new String(tempUrl);
                    }
                }
                else
                {
                    url = url.Replace("?" + NewsConstants.Newsvidgetid + "=" + widgetId, String.Empty);
                }
            }

            return url;
        }

        /// <summary>
        /// Clones the news widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneNewsWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();

            var widget = BindWidgetModel(instance);

            if (widget != null)
            {
                var clone = (NewsArticleWidget)new NewsArticleWidget().InjectFrom<CloneEntityInjection>(widget);

                if (widgetService.Save(clone))
                {
                    return clone.Id;
                }
            }
            return null;
        }
        
        #endregion
    }
}