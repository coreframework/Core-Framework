using System;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Web.Models;
using Core.Web.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;

namespace Core.Web.Controllers
{
    public partial class PlaceHolderWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return PlaceHolderWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            return Content(HttpContext.Translate("PlaceHolder", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ReplaceWidget(PlaceHolderWidgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                //model = BreadcrumbsWidgetHelper.SaveBreadcrumbsWidget(model);
            }

            return Content(String.Empty);// PartialView("EditWidget", model);
        }

        #endregion

    }
}
