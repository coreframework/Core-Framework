using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using Products.Helpers;
using Products.Models;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;
using Products.Widgets;

namespace Products.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "CategoryViewerWidget")]
    public partial class CategoryViewerWidgetController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return CategoryViewerWidget.Instance.Identifier; }
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
                TempData["isAjax"] = Request.IsAjaxRequest();
                int curPage = !String.IsNullOrEmpty(Request.Params["curPageCat" + instance.InstanceId]) ? Int32.Parse(Request.Params["curPageCat" + instance.InstanceId]) : 1;
                var model = CategoryViewerWidgetHelper.BindWidgetViewModel(instance, curPage > 0 ? curPage : 1);

                if (model != null)
                    return PartialView(model);
            }
            return Content(HttpContext.Translate("Setup", ResourceHelper.GetControllerScope(this)));
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
                var widget = new CategoryWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<ICategoryWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new CategoryWidgetModel().MapFrom(widget));
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(CategoryWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = CategoryViewerWidgetHelper.SaveContentViewerWidget(model);
            }
            return PartialView("EditWidget", model);
        }

        /// <summary>
        /// View Category with Products.
        /// </summary>
        /// <param name="id">The category id.</param>
        /// <returns></returns>
        public virtual ActionResult Category(long id)
        {
            var model = CategoryViewerWidgetHelper.BindModel(id);
            return View("Category", model);
        }


        #endregion
    }
}
