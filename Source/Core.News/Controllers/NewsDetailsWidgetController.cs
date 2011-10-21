using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.News.Helpers;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using NewsDetailsWidget = Core.News.Widgets.NewsDetailsWidget;

namespace Core.News.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "NewsDetailsWidget")]
    public partial class NewsDetailsWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return NewsDetailsWidget.Instance.Identifier; }
        }

        #endregion


        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            object newsArticleIdObject;
            RouteData.Values.TryGetValue("newsId", out newsArticleIdObject);
            if (instance != null && instance.InstanceId != null && newsArticleIdObject != null && newsArticleIdObject is String)
            {
                var widgetModel = NewsDetailsWidgetHelper.BindWidgetModel(instance, newsArticleIdObject.ToString());
                if (widgetModel != null)
                {
                    return PartialView("ViewWidget", widgetModel);
                }
            }
            throw new HttpException((int)HttpStatusCode.NotFound,
                                                HttpContext.Translate("NotFound",
                                                                      ResourceHelper.GetControllerScope(this)));
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
                var widget = new Nhibernate.Models.NewsDetailsWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<INewsDetailsWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new NewsDetailsWidgetEditModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(NewsDetailsWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                model = NewsDetailsWidgetHelper.SaveContentViewerWidget(model);
            }

            return PartialView("EditWidget", model);
        }
    }
}
