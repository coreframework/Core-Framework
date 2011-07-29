using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.News.Helpers;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Core.News.Widgets;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "NewsViewerWidget")]
    public partial class NewsViewerWidgetController : CoreWidgetController
    {
        #region

        private const string Newsvidgetid = "newsvidgetid";
        private const string Articleid = "articleid";

        #endregion

        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return NewsViewerWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
                var articleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                var widget = NewsViewerWidgetHelper.BindWidgetModel(instance);

                if (widget != null)
                {
                    if (!String.IsNullOrEmpty(Request.Params[Newsvidgetid]))
                    {
                        var ids = Request.Params[Newsvidgetid].Split(',');
                        foreach (var id in ids)
                        {
                            if (widget.Id == int.Parse(id))
                            {
                                if (!String.IsNullOrEmpty(Request.Params[Articleid + id]))
                                {
                                    var article = articleService.FindPublished(long.Parse(Request.Params[Articleid + id]));
                                    article.WidgetId = widget.Id;
                                    return PartialView("ArticleWidget", article);
                                }
                            }
                        }
                    }

                    widget.NewsArticles =
                        new List<NewsArticle>(articleService.FindPublished());
                    return PartialView(widget);
                }
            }
            return Content(HttpContext.Translate("Messages.Nonews", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
                var widget = new NewsArticleWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new NewsArticleViewerWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(NewsArticleViewerWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = NewsViewerWidgetHelper.SaveContentViewerWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}