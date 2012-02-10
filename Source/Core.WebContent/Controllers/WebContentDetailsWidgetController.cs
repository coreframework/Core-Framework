using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.WebContent.Helpers;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.Widgets;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "WebContentDetailsWidget")]
    public partial class WebContentDetailsWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return ContentDetailsWidget.Instance.Identifier;
            }
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
            object articleIdObject;
            RouteData.Values.TryGetValue("webContentId", out articleIdObject);
            if (instance != null && instance.InstanceId != null && articleIdObject != null && articleIdObject is String)
            {
                ICorePrincipal currentUser = this.CorePrincipal();
                var widgetModel = WebContentDetailsWidgetHelper.BindWidgetModel(instance, articleIdObject.ToString(), currentUser);
                if (widgetModel != null)
                {
                    return PartialView("WebContentWidget/DetailsMode", widgetModel);
                }
            }
//            throw new HttpException((int)HttpStatusCode.NotFound,
//                                                HttpContext.Translate("NotFound",
//                                                                      ResourceHelper.GetControllerScope(this)));
            return Content(String.Empty);
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
                var widget = new WebContentDetailsWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IWebContentDetailsWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new DetailsWidgetEditModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(DetailsWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                model = WebContentDetailsWidgetHelper.SaveArticleViewerWidget(model);
            }

            return PartialView("EditWidget", model);
        }
    }
}
