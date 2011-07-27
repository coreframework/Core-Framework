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
    [Export(typeof(IController)), ExportMetadata("Name", "ProductViewerWidget")]
    public partial class ProductViewerWidgetController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
        {
            get { return ProductViewerWidget.Instance.Identifier; }
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
            TempData[ProductConstants.IsAjaxPageQueryRequestParam] = Request.IsAjaxRequest();
            if (instance != null)
            {
                long id;
                if (String.IsNullOrEmpty(Request.QueryString[ProductConstants.ProductIdQueryRequestParam + instance.InstanceId]))
                {
                    int curPage = !String.IsNullOrEmpty(Request.QueryString[ProductConstants.CurrentPageQueryRequestParam + instance.InstanceId]) ? Int32.Parse(Request.QueryString[ProductConstants.CurrentPageQueryRequestParam + instance.InstanceId]) : 1;
                    var model = ProductViewerWidgetHelper.BindWidgetViewModel(instance, curPage > 0 ? curPage : 1);

                    if (model != null)
                        return PartialView(model);
                }
                else
                    if (Int64.TryParse(Request.QueryString[ProductConstants.ProductIdQueryRequestParam + instance.InstanceId], out id))
                    {

                        var productService = ServiceLocator.Current.GetInstance<IProductService>();
                        var prod = productService.Find(id);
                        if (prod != null)
                        {
                            TempData[ProductConstants.ProductWidgetIdQueryRequestParam] = instance.InstanceId;
                            return PartialView("ViewProduct", prod);
                        }
                    }
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
                var widget = new ProductWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IProductWidgetService>();
                    var exWidget = widgetService.Find((long)instance.InstanceId);

                    if (exWidget != null)
                        widget = exWidget;
                }
                return PartialView(new ProductWidgetModel().MapFrom(widget));
            }
            return Content(String.Empty);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(ProductWidgetModel model)
        {
            if (ModelState.IsValid)
            {
                model = ProductViewerWidgetHelper.SaveContentViewerWidget(model);
            }
            return PartialView("EditWidget", model);
        }

        #endregion
    }
}
