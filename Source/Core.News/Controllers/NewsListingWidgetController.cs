using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.News.Helpers;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using NewsListingWidget = Core.News.Widgets.NewsListingWidget;

namespace Core.News.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "NewsListingWidget")]
    public partial class NewsListingWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return NewsListingWidget.Instance.Identifier; }
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
            if (instance != null && instance.InstanceId != null)
            {
                int currentPage = 0;
                if (!String.IsNullOrEmpty(Request.Params[NewsConstants.Newsvidgetid]))
                {
                    var ids = Request.Params[NewsConstants.Newsvidgetid].Split(',');
                    foreach (var id in ids)
                    {
                        if (instance.InstanceId == int.Parse(id))
                        {

                            if (!String.IsNullOrEmpty(Request.Params[NewsConstants.CurrentPage + id]))
                            {
                                currentPage = int.Parse(Request.Params[NewsConstants.CurrentPage + id]);
                            }
                        }
                    }
                }
                var widgetModel = NewsListingWidgetHelper.BindWidgetModel(instance, currentPage);
                
                if (widgetModel != null)
                {
                    

                    return PartialView(widgetModel);
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
                var widget = new Nhibernate.Models.NewsListingWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<INewsListingWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new NewsListingWidgetEditModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(NewsListingWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                model = NewsListingWidgetHelper.SaveContentViewerWidget(model);
            }

            return PartialView("EditWidget", model);
        }

    }
}
