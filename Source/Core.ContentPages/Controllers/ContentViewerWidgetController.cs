using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.ContentPages.Helpers;
using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Core.ContentPages.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Microsoft.Practices.ServiceLocation;

namespace Core.ContentPages.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ContentViewerWidget")]
    public partial class ContentViewerWidgetController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return ContentViewerWidget.Instance.Identifier; }
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
            if (instance!=null)
            {
                var widget = ContentViewerWidgetHelper.BindWidgetModel(instance);

                if (widget!=null )
                    return PartialView(widget);
            }
            return Content("Select existing web content");
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
                var widget = new ContentPageWidget();

                if(instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new ContentViewerWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(ContentViewerWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = ContentViewerWidgetHelper.SaveContentViewerWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        #endregion
    }
}
