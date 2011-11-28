using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Core.WebContent.Helpers;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "WebContentWidget")]
    public partial class WebContentWidgetController : CoreWidgetController
    {
        #region Fields

        private readonly ICategoryService categoryService;
        private readonly IArticleService articleService;

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return ContentWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructor

        public WebContentWidgetController()
        {
            categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
            articleService = ServiceLocator.Current.GetInstance<IArticleService>();
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
                var widget = WebContentWidgetHelper.BindWidgetModel(instance);
                if (widget != null)
                {
                    if (widget.Article != null)
                    {
                        return PartialView("WebContentWidget/DetailsMode", new WidgetDetailsModel(widget.Article, true));
                    }

                    return PartialView("ListingMode", WebContentWidgetHelper.BindListingModel(widget, 1));
                }
            }
            return Content(HttpContext.Translate("Messages.Setup", ResourceHelper.GetControllerScope(this)));
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
                var widget = new WebContentWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IWebContentWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new WebContentWidgetViewModel().MapFrom(widget));
            }

            return Content(HttpContext.Translate("Messages.SetupForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(WebContentWidgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = WebContentWidgetHelper.SaveWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        [HttpPost]
        public virtual ActionResult LoadCategories(String categoriesId, long sectionId)
        {
            string[] selectedCategories = categoriesId.Trim().Split(',');
            var categories = categoryService.GetPublishedCategories(this.CorePrincipal(), (Int32)CategoryOperations.AddToWidget, sectionId).ToList().Select(item => new SelectListItem
            {
                Text = ((WebContentCategoryLocale)item.CurrentLocale).Title,
                Value = item.Id.ToString(),
                Selected = (selectedCategories.Contains(item.Id.ToString())) ? true : false
            });

            return Json(categories);
        }

        [HttpPost]
        public virtual ActionResult LoadArticles(String categoriesId, long? articleId)
        {
            String[] selectedCategories = categoriesId.Trim().Split(',');
            var categories = new List<long>();

            foreach (var selectedCategory in selectedCategories)
            {
                long current;
                if (Int64.TryParse(selectedCategory, out current))
                {
                    categories.Add(current);
                }
            }

            var articles = articleService.GetPublishedArticles(this.CorePrincipal(), (Int32)ArticleOperations.AddToWidget, categories).ToList().Select(item => new SelectListItem
            {
                Text = ((ArticleLocale)item.CurrentLocale).Title,
                Value = item.Id.ToString(),
                Selected = (articleId != null && articleId == item.Id) ? true : false
            });

            return Json(articles);
        }

        #endregion
    }
}
