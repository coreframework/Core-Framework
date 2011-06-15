using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Forms.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "FormsBuilderWidget")]
    public partial class FormsBuilderWidgetController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get
            {
                return FormsBuilderWidget.Instance.Identifier;
            }
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
          /*  if (instance != null)
            {
                var widget = ContentViewerWidgetHelper.BindWidgetModel(instance);

                if (widget != null)
                    return PartialView(widget);
            }
            return Content("Select existing web content");*/
            return Content("View widget");
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
           /* if (instance != null)
            {
                var widget = new ContentPageWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new ContentViewerWidgetModel().MapFrom(widget));
            }*/

            return Content("Edit widget");
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget()
        {
           /* if (ModelState.IsValid)
            {
                model = ContentViewerWidgetHelper.SaveContentViewerWidget(model);
            }

            return PartialView("EditWidget", model);*/
            return Content("Update");
        }

        #endregion
    }
}
