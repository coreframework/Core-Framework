using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.News.Models;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Core.News.Permissions.Operations;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.News.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "NewsCategory")]
    [Permissions((int)NewsPluginOperations.ManageCategories, typeof(NewsPlugin))]
    public partial class NewsCategoryController : CorePluginController
    {
        #region Fields
        
        private readonly INewsCategoryService categoryService;

        private readonly INewsCategoryLocaleService categoryLocaleService;

        #endregion

        #region Properties

        /// <summary>
        /// Controller Plugin Identifier
        /// </summary>
        public override string ControllerPluginIdentifier
        {
            get { return NewsPlugin.Instance.Identifier; }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCategoryController"/> class.
        /// </summary>
        public NewsCategoryController()
        {
            this.categoryService = ServiceLocator.Current.GetInstance<INewsCategoryService>();
            this.categoryLocaleService = ServiceLocator.Current.GetInstance<INewsCategoryLocaleService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders Categories listing.
        /// </summary>
        /// <returns>List of Categories.</returns>
        public virtual ActionResult ShowAll()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = HttpContext.Translate("Title", ResourceHelper.GetControllerScope(this)), 
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
                DataUrl = Url.Action("DynamicGridData", "NewsCategory"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                IsRowNotClickable = true
            };
            return View("Index", model);
        }


        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            ICriteria searchCriteria = categoryLocaleService.GetSearchCriteria(search);

            long totalRecords = categoryLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var categories = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<NewsCategoryLocale>();
           
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from categoryLocale in categories
                    select new
                    {
                        id = categoryLocale.Category.Id,
                        cell = new[] {  categoryLocale.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("ShowById","NewsCategory",new { id = categoryLocale.Category.Id }),HttpContext.Translate("View", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","NewsCategory",new { id = categoryLocale.Category.Id }),HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                            Url.Action("Remove","NewsCategory",new { id = categoryLocale.Category.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows Category details by id.
        /// </summary>
        /// <param name="id">The Category id.</param>
        /// <returns>Category details</returns>
        public virtual ActionResult ShowById(long? id)
        {
            var category = categoryService.Find(id ?? 0);
            if (category == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }
            return View("Show", new CategoryLocaleViewModel().MapFrom(category));
        }

        /// <summary>
        /// Shows Category edit form.
        /// </summary>
        /// <param name="id">The Category id.</param>
        /// <returns>Category edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var category = categoryService.Find(id ?? 0);
            if (category == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("Edit", new CategoryLocaleViewModel().MapFrom(category));
        }

        /// <summary>
        /// Updates Category.
        /// </summary>
        /// <param name="id">The Category id.</param>
        /// <param name="category">The Category model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, CategoryLocaleViewModel category)
        {
            if (ModelState.IsValid && id!=null)
            {
                var localeService = ServiceLocator.Current.GetInstance<INewsCategoryLocaleService>();
                var categoryLocale = localeService.GetLocale((long) id, category.SelectedCulture);
                categoryLocale = categoryLocale ?? new NewsCategoryLocale
                                         {
                                             Category =
                                                 category.MapTo(new NewsCategory { Id = (long)id }),
                                             Culture = category.SelectedCulture,
                                         };
                categoryLocale.Title = category.Title;
                categoryLocale.Description = category.Description;
                localeService.Save(categoryLocale);
               
                Success(HttpContext.Translate("SuccessUpdate", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }
            Error(HttpContext.Translate("Error", ResourceHelper.GetControllerScope(this)));
            return View("Edit", category);
        }

        /// <summary>
        /// Shows Category create form.
        /// </summary>
        /// <returns>Category create form.</returns>
        public virtual ActionResult New()
        {
            return View("New", new CategoryViewModel());
        }

        /// <summary>
        /// Saves new Category.
        /// </summary>
        /// <param name="category">The Category.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Save(category.MapTo(new NewsCategory() {CreateDate = DateTime.Now}));
                Success(HttpContext.Translate("SuccessCreate", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }
            Error(HttpContext.Translate("Error", ResourceHelper.GetControllerScope(this)));
            return View("New", category);
        }


        /// <summary>
        /// Removes the specified Category.
        /// </summary>
        /// <param name="id">The Category id.</param>
        /// <returns>List of Category</returns>
        public virtual ActionResult Remove(long id)
        {
            var category = categoryService.Find(id);
            if (category != null)
            {
                categoryService.Delete(category);
                Success(HttpContext.Translate("SuccessDelete", ResourceHelper.GetControllerScope(this)));
            }

            return RedirectToAction("ShowAll");
        }


        /// <summary>
        /// Change Language
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="culture">Selected culture</param>
        /// <param name="isShow">Is show</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ChangeLanguage(long categoryId, String culture, bool isShow)
        {
            var category = categoryService.Find(categoryId);
            if (category == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }
            CategoryLocaleViewModel model = new CategoryLocaleViewModel().MapFrom(category);
            model.SelectedCulture = culture;
            INewsCategoryLocaleService localeService = ServiceLocator.Current.GetInstance<INewsCategoryLocaleService>();
            NewsCategoryLocale locale = localeService.GetLocale(categoryId, culture);
            if (locale != null)
            {
                model.Title = locale.Title;
                model.Description = locale.Description;
            }
            return PartialView(isShow ? "ShowForm" : "EditForm", model);
        }
        #endregion
    }
}
