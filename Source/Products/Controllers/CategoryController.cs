using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using Products.Models;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;
using Products.Permissions.Operations;

namespace Products.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "Category")]
    [Permissions((int)ProductsPluginOperations.ManageCategory, typeof(ProductPlugin))]
    public partial class CategoryController : CorePluginController
    {
        #region Fields
        
        private readonly ICategoryService categoryService;
        private readonly ICategoryLocaleService categoryLocaleService;

        #endregion

        #region Properties

        /// <summary>
        /// Controller Plugin Identifier
        /// </summary>
        public override String ControllerPluginIdentifier
        {
            get { return ProductPlugin.Instance.Identifier; }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        public CategoryController()
        {
            this.categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
            this.categoryLocaleService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders Categories listing.
        /// </summary>
        /// <returns>List of Categories.</returns>
        [MvcSiteMapNode(Title = "$t:Titles.Categories", AreaName = "Product", ParentKey = "Home", Key = "Categories.ShowAll")]
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
                DataUrl = Url.Action("DynamicGridData", "Category"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                IsRowNotClickable = true
            };
            return View("Index", model);
        }


        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchCriteria = categoryLocaleService.GetSearchCriteria(search);

            long totalRecords = categoryLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var categories = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<CategoryLocale>();
           
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
                                            Url.Action("ShowById","Category",new { id = categoryLocale.Category.Id }),HttpContext.Translate("View", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","Category",new { id = categoryLocale.Category.Id }),HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                            Url.Action("Remove","Category",new { id = categoryLocale.Category.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows Category details by id.
        /// </summary>
        /// <param name="id">The Category id.</param>
        /// <returns>Category details</returns>
        [MvcSiteMapNode(Title = "Show", AreaName = "Product", ParentKey = "Categories.ShowAll")]
        [SiteMapTitle("Title")]
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
        [MvcSiteMapNode(Title = "Edit", AreaName = "Product", ParentKey = "Categories.ShowAll")]
        [SiteMapTitle("Title")]
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
                var localeService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
                var categoryLocale = localeService.GetLocale((long) id, category.SelectedCulture);
                categoryLocale = categoryLocale ?? new CategoryLocale
                                         {
                                             Category =
                                                 category.MapTo(new Category { Id = (long)id }),
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
        [MvcSiteMapNode(Title = "New", AreaName = "Product", ParentKey = "Categories.ShowAll")]
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
                categoryService.Save(category.MapTo(new Category() {CreateDate = DateTime.Now}));
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
            ICategoryLocaleService localeService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
            CategoryLocale locale = localeService.GetLocale(categoryId, culture);
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
