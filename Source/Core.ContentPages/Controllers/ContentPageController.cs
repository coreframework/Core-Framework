using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Core.ContentPages.Permissions.Operations;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using IContentPageService = Core.ContentPages.NHibernate.Contracts.IContentPageService;
using System.Linq.Dynamic;

namespace Core.ContentPages.Controllers
{
    /// <summary>
    /// Handles module requests.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "ContentPage")]
    [Permissions((int)ContentPagePluginOperations.ManageContentPages, typeof(ContentPagePlugin))]
    public partial class ContentPageController : CorePluginController
    {
        #region Fields

        private readonly IContentPageService contentPageService;

        private readonly IContentPageLocaleService contentPageLocaleService;

        #endregion

        #region Properties

        public override string ControllerPluginIdentifier
        {
            get { return ContentPagePlugin.Instance.Identifier; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageController"/> class.
        /// </summary>
        public ContentPageController()
        {
            this.contentPageService = ServiceLocator.Current.GetInstance<IContentPageService>();
            this.contentPageLocaleService = ServiceLocator.Current.GetInstance<IContentPageLocaleService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders content pages listing.
        /// </summary>
        /// <returns>List of content pages.</returns>

        [MvcSiteMapNode(Title = "$t:Titles.ContentPages", AreaName = "ContentPage", ParentKey = "Home", Key = "ContentPage.ShowAll")]
        public virtual ActionResult ShowAll()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = HttpContext.Translate("Title", ResourceHelper.GetControllerScope(this)), 
                                                                 Index = "Title",
                                                                 Width = 400
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 20,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                  Width = 10,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Id", 
                                                                 Sortable = false, 
                                                                 Hidden = true
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action("DynamicGridData","ContentPage"),
                DefaultOrderColumn = "Id",
                GridTitle = "Content Pages",
                Columns = columns,
                IsRowNotClickable = true
            };

            return View("Admin/Index", model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchCriteria = contentPageLocaleService.GetSearchCriteria(search);

            long totalRecords = contentPageLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var contentPages = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<ContentPageLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from contentPage in contentPages
                    select new
                    {
                        id = contentPage.ContentPage.Id,
                        cell = new[] {  contentPage.Title, 

                                        String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("Edit","ContentPage",new { id = contentPage.ContentPage.Id }),HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("Remove","ContentPage",new { id = contentPage.ContentPage.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows content page edit form.
        /// </summary>
        /// <param name="id">The content id.</param>n
        /// <returns>Content page edit view</returns>
        [MvcSiteMapNode(Title = "Edit", AreaName = "ContentPage", ParentKey = "ContentPage.ShowAll")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long? id)
        {
            var contentPage = contentPageService.Find(id ?? 0);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Edit", new ContentPageLocaleViewModel().MapFrom(contentPage));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long contentPageId, String culture)
        {
            var contentPage = contentPageService.Find(contentPageId);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }
            ContentPageLocaleViewModel model = new ContentPageLocaleViewModel().MapFrom(contentPage);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<IContentPageLocaleService>();
            ContentPageLocale locale = localeService.GetLocale(contentPageId, culture);
            if (locale != null)
            {
                model.Title = locale.Title;
                model.Content = locale.Content;
            }

            return PartialView("Admin/EditForm", model);
        }

        /// <summary>
        /// Updates content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <param name="contentPageModel">The content page model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, ContentPageLocaleViewModel contentPageModel)
        {
            if (ModelState.IsValid && id != null)
            {
                ContentPage contentPage = contentPageService.Find((long)id);
                IContentPageLocaleService localeService = ServiceLocator.Current.GetInstance<IContentPageLocaleService>();
                ContentPageLocale contentPageLocale = localeService.GetLocale(id.Value, contentPageModel.SelectedCulture);
                if (contentPageLocale == null)
                {
                    contentPageLocale = new ContentPageLocale { ContentPage = contentPage, Culture = contentPageModel.SelectedCulture };
                }
                contentPageLocale.Title = contentPageModel.Title;
                contentPageLocale.Content = contentPageModel.Content;
                localeService.Save(contentPageLocale);
                Success("Sucessfully save content page.");
                return RedirectToAction("ShowAll");
            }

            Error("Validation errors occurred while processing this form. Please take a moment to review the form and correct any input errors before continuing.");
            return View("Admin/Edit", contentPageModel);
        }

        /// <summary>
        /// Shows content page create form.
        /// </summary>
        /// <returns>Content page create form.</returns>
        [MvcSiteMapNode(Title = "New", AreaName = "ContentPage", ParentKey = "ContentPage.ShowAll")]
        public virtual ActionResult New()
        {
            return View("Admin/New", new ContentPageViewModel());
        }

        /// <summary>
        /// Saves new content page.
        /// </summary>
        /// <param name="contentPage">The content page.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(ContentPageViewModel contentPage)
        {
            if (ModelState.IsValid)
            {
                contentPageService.Save(contentPage.MapTo(new ContentPage()));
                Success("Sucessfully save content page.");
                return RedirectToAction("ShowAll");
            }

            Error("Validation errors occurred while processing this form. Please take a moment to review the form and correct any input errors before continuing.");
            return View("Admin/New", contentPage);
        }


        /// <summary>
        /// Removes the specified content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <returns>List of content pages</returns>
        //[HttpPost]
        public virtual ActionResult Remove(long id)
        {
            var contentPage = contentPageService.Find(id);
            if (contentPage != null)
            {
                Success("Sucessfully remove content page.");
                contentPageService.Delete(contentPage);
                return RedirectToAction("ShowAll");
            }

            Error("Some error has been occured. Please try again.");
            return RedirectToAction("ShowAll");
        }

        #endregion
    }
}
