using Core.Framework.Plugins.Web;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Microsoft.Practices.ServiceLocation;

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
        public static NewsArticleWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
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
        public static NewsArticleViewerWidgetModel SaveContentViewerWidget(NewsArticleViewerWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();
            var newsViewer = model.MapTo(new NewsArticleWidget());
            widgetService.Save(newsViewer);
            return new NewsArticleViewerWidgetModel().MapFrom(newsViewer);
        }
        
        public static string GetBackUrl(string url, long widgetId, long articleId)
        {
            url = url.Replace("&articleid" + widgetId + "=" + articleId, "");
            url = url.Replace("?newsvidgetid=" + widgetId, "");
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
                url = url.Replace("&newsvidgetid=" + widgetId, "");
            }

            return url;
        }
        
        #endregion
    }
}