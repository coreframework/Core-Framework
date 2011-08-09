using Core.Framework.Plugins.Web;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

namespace Core.News.Helpers
{
    public class NewsViewerWidgetHelper
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

            return new NewsArticleViewerWidgetModel().MapFrom(widgetService.Find(instance.InstanceId ?? 0));
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
            return new NewsArticleWidgetModel().MapFrom(newsViewer);
        }

        /// <summary>
        /// Gets the back URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="widgetId">The widget id.</param>
        /// <param name="articleId">The article id.</param>
        /// <returns></returns>
        public static string GetBackUrl(string url, long widgetId, long articleId)
        {
            url = url.Replace("&" + NewsConstants.Articleid + widgetId + "=" + articleId, "");
            if (!url.Contains(NewsConstants.CurrentPage + widgetId))
            {
                url = url.Replace("?" + NewsConstants.Newsvidgetid + "=" + widgetId, "");
                if (!url.Contains("?"))
                {
                    var index = url.IndexOf("&");
                    if (index > 0)
                    {
                        var tempUrl = url.ToCharArray();
                        tempUrl[index] = '?';
                        url = new string(tempUrl);
                    }
                }
                else
                {
                    url = url.Replace("?" + NewsConstants.Newsvidgetid + "=" + widgetId, "");
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