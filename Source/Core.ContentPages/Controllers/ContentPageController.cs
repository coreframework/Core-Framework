using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Models;
using Core.ContentPages.Permissions.Operations;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
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
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders content pages listing.
        /// </summary>
        /// <returns>List of content pages.</returns>
        
        public virtual ActionResult ShowAll()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Title", 
                                                                 Index = "Title",
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 30,
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
                DefaultOrderColumn = "Title",
                GridTitle = "Users",
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
            IQueryable<ContentPage> searchQuery = contentPageService.GetSearchQuery(search);
            int totalRecords = contentPageService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var contentPages = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from contentPage in contentPages
                    select new
                    {
                        id = contentPage.Id,
                        cell = new[] {  contentPage.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("ShowById","ContentPage",new { id = contentPage.Id }),"View"),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","ContentPage",new { id = contentPage.Id }),"Edit"),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                            Url.Action("Remove","ContentPage",new { id = contentPage.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows content page details by id.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <returns>Content page details</returns>
        public virtual ActionResult ShowById(long? id)
        {
            var contentPage = contentPageService.Find(id ?? 0);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Show", contentPage);
        }

        /// <summary>
        /// Shows content page edit form.
        /// </summary>
        /// <param name="id">The content id.</param>
        /// <returns>Content page edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var contentPage = contentPageService.Find(id ?? 0);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Edit", new ContentPageViewModel().MapFrom(contentPage));
        }

        /// <summary>
        /// Updates content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <param name="contentPage">The content page model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, ContentPageViewModel contentPageModel)
        {
            if (ModelState.IsValid && id!=null)
            {
                ContentPage contentPage = contentPageService.Find((long) id);
                contentPageService.Save(contentPageModel.MapTo(contentPage));
                return RedirectToAction("ShowAll");
            }

            return View("Admin/Edit", contentPageModel);
        }

        /// <summary>
        /// Shows content page create form.
        /// </summary>
        /// <returns>Content page create form.</returns>
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
                return RedirectToAction("ShowAll");
            }

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
                contentPageService.Delete(contentPage);
            }

            return RedirectToAction("ShowAll");
        }

        #endregion
    }
}
