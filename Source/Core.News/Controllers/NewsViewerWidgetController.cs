using System;
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
            if (instance != null && instance.InstanceId !=null)
            {
                var articleService = ServiceLocator.Current.GetInstance<INewsArticleService>();
                var widget = NewsViewerWidgetHelper.BindWidgetModel(instance);
                if (widget != null)
                {
                    if(!Request.Url.ToString().Contains("update-widget-instance"))
                    {
                        widget.Url = Request.RawUrl;
                        var widgetService = ServiceLocator.Current.GetInstance<INewsArticleWidgetService>();
                        var basewidget = widgetService.Find(widget.Id);
                        basewidget = widget.MapTo(basewidget);
                        widgetService.Save(basewidget);
                    }
                    if (!String.IsNullOrEmpty(Request.Params[NewsConstants.Newsvidgetid]))
                    {

                        var ids = Request.Params[NewsConstants.Newsvidgetid].Split(',');
                        foreach (var id in ids)
                        {
                            if (widget.Id == int.Parse(id))
                            {
                                if (!String.IsNullOrEmpty(Request.Params[NewsConstants.Articleid + id]))
                                {

                                    if (!String.IsNullOrEmpty(Request.Params[NewsConstants.Articleid + id]))
                                    {
                                        var article =
                                            articleService.FindPublished(
                                                long.Parse(Request.Params[NewsConstants.Articleid + id]));
                                        article.WidgetId = widget.Id;
                                        return PartialView("ArticleWidget", article);
                                    }
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(Request.Params[NewsConstants.CurrentPage + id]))
                                    {
                                        widget.CurrentPage = int.Parse(Request.Params[NewsConstants.CurrentPage + id]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        widget.CurrentPage = 0;
                    }
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
                //widget.Categories = ServiceLocator.Current.GetInstance<INewsCategoryService>().GetAll().ToList(); 
                return PartialView(new NewsArticleWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(NewsArticleWidgetModel model)
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