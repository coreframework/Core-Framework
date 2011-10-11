using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Mvc.Controllers;
using Framework.Mvc.Grids;
using Framework.Mvc.Grids.JqGrid;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles pages administration operation requests.
    /// </summary>
    [Permissions((int)BaseEntityOperations.Manage, typeof(Page))]
    public partial class PageController : FrameworkController
    {
        #region Fields

        private readonly IPageService pageService;
        private readonly IPageLocaleService pageLocaleService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageController"/> class.
        /// </summary>
        public PageController()
        {
            pageService = ServiceLocator.Current.GetInstance<IPageService>();
            pageLocaleService = ServiceLocator.Current.GetInstance<IPageLocaleService>();
        }

        #endregion

        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel {
                                                                 Name = Translate("Models.PageLocale.Title"),
                                                                 Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.PageLocale.Url"),
                                                                 Index = "Url"
                                                             },
                                                        new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.PageLocale.InMainMenu"),
                                                                 Index = "Url"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Actions.Actions"),
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {Name = "Id", Sortable = false, Hidden = true}
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.Page.DynamicGridData()),
                DefaultOrderColumn = "Title",
                GridTitle = Translate(".Pages"),
                Columns = columns
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchCriteria = pageLocaleService.GetSearchCriteria(search);
            long totalRecords = pageLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var pages = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<PageLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (pages.Select(pageLocale => new
                {
                    id = JqGridConstants.NotClickableId,
                    cell = new[]
                                                                       {
                                                                            pageLocale.Title, 
                                                                            pageLocale.Page.Url,
                                                                            pageLocale.Page.HideInMainMenu ? Translate("Boolean.False") : Translate("Boolean.True"),
                                                                            String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.Pages.Show(pageLocale.Page.Url)), Translate("Actions.View"))
                                                                       }
                }).ToArray())
            };
            return Json(jsonData);
        }
    }
}