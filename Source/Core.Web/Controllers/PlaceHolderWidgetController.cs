using System;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.Widgets;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    public partial class PlaceHolderWidgetController : CoreWidgetController
    {
        #region Fields

        private IPageWidgetService pageWidgetService;

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return PlaceHolderWidget.Instance.Identifier; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceHolderWidgetController"/> class.
        /// </summary>
        public PlaceHolderWidgetController()
        {
            pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
        }

        #endregion

        #region Actions

        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if(instance.PageWidgetId.HasValue)
            {
                var pageWidget = pageWidgetService.Find(instance.PageWidgetId.Value);

                return PartialView(new PlaceHolderWidgetViewModel().MapFrom(pageWidget));    
            }

            return Content(String.Empty);
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
