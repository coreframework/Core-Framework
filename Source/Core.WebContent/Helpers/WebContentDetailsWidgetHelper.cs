using System;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Helpers
{
    public static class WebContentDetailsWidgetHelper
    {
        public const String DetailsPageUrl = "web-content/details/{webContentId}";

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="articleIdentifier">The article identifier.</param>
        /// <returns></returns>
        public static WidgetDetailsModel BindWidgetModel(ICoreWidgetInstance instance, String articleIdentifier, ICorePrincipal user)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentDetailsWidgetService>();
            var widget = widgetService.Find(instance.InstanceId ?? 0);
            if (widget != null)
            {
                Article article = null;
                switch (widget.LinkMode)
                {
                    case WebContentDetailsLinkMode.Id:
                        {
                            long newsArticleId;
                            if (long.TryParse(articleIdentifier, out newsArticleId))
                            {
                                var articleService = ServiceLocator.Current.GetInstance<IArticleService>();
                                article = articleService.FindPublished(user, newsArticleId);
                            }
                            break;
                        }
                    case WebContentDetailsLinkMode.Url:
                        {
                            var articleService = ServiceLocator.Current.GetInstance<IArticleService>();
                            article = articleService.FindPublished(user, articleIdentifier);
                            break;
                        }
                }
                article = article ?? new Article();
                
                return new WidgetDetailsModel(article, true);
            }

            return null;
        }

        /// <summary>
        /// Saves the article viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static DetailsWidgetEditModel SaveArticleViewerWidget(DetailsWidgetEditModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentDetailsWidgetService>();
            var articleViewer = model.MapTo(new WebContentDetailsWidget());
            widgetService.Save(articleViewer);

            return new DetailsWidgetEditModel().MapFrom(articleViewer);
        }
    }
}