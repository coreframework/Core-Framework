using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "WebContentCategory")]
    public partial class WebContentCategoryController : CorePluginController
    {
        #region Fields

        private readonly ICategoryService categoryService;
        private readonly ICategoryLocaleService categoryLocaleService;
        private readonly IPermissionCommonService permissionService;
        private readonly IPermissionsHelper permissionsHelper;

        #endregion

        #region Constructor

        public WebContentCategoryController()
        {
            categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
            categoryLocaleService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();
        }

        #endregion

        #region Actions

        [MvcSiteMapNode(Title = "$t:Titles.Categories", AreaName = "WebContent", ParentKey = "Home", Key = "WebContent.Category.Show")]
        public virtual ActionResult Show()
        {
            return View(BuildCategoriesGrid());
        }

        [HttpPost]
        public virtual JsonResult LoadData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = categoryLocaleService.GetSearchCriteria(search, this.CorePrincipal(), (Int32)CategoryOperations.View);
            long totalRecords = categoryLocaleService.Count(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var categorys = searchQuery.AddOrder(new Order(sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<WebContentCategoryLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from category in categorys
                    select new
                    {
                        id = category.Id,
                        cell = new[] {  
                                        category.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","WebContentCategory",new { categoryId = category.Category.Id }),HttpContext.Translate("Details", ResourceHelper.GetControllerScope(this)))
                        }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "WebContent", ParentKey = "WebContent.Category.Show")]
        public virtual ActionResult New()
        {
            return View(new CategoryViewModel { AllowManage = true});
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = category.MapTo(new WebContentCategory { UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null });
                if (categoryService.Save(newCategory))
                {
                    permissionService.SetupDefaultRolePermissions(OperationsHelper.GetOperations<CategoryOperations>(), typeof(WebContentCategory), newCategory.Id);
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.WebContentCategory.Edit(newCategory.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            }

            category.AllowManage = true;
            return View("New", category);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "WebContent", ParentKey = "WebContent.Category.Show", Key = "WebContent.Category.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long categoryId)
        {
            var category = categoryService.Find(categoryId);

            if (category == null || !permissionService.IsAllowed((Int32)CategoryOperations.View, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)CategoryOperations.Manage, this.CorePrincipal(),
                                                            typeof(WebContentCategory), category.Id, IsCategoryOwner(category),
                                                            PermissionOperationLevel.Object);

            return View("Edit", new CategoryViewModel { AllowManage = allowManage }.MapFrom(category));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long categoryId, String culture)
        {
            var category = categoryService.Find(categoryId);

            if (category == null || !permissionService.IsAllowed((Int32)CategoryOperations.View, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)CategoryOperations.Manage, this.CorePrincipal(),
                                                        typeof(WebContentCategory), category.Id, IsCategoryOwner(category),
                                                        PermissionOperationLevel.Object);

            CategoryViewModel model = new CategoryViewModel { AllowManage = allowManage }.MapFrom(category);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
            WebContentCategoryLocale locale = localeService.GetLocale(categoryId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("CategoryDetails", model);
        }

        [HttpPost]
        public virtual ActionResult Save(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = categoryService.Find(model.Id);

                if (category == null || !permissionService.IsAllowed((Int32)CategoryOperations.Manage, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (categoryService.Save(model.MapTo(category)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<ICategoryLocaleService>();
                    WebContentCategoryLocale locale = localeService.GetLocale(category.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new WebContentCategoryLocale { Category = category });

                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.WebContentCategory.Edit(model.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            }

            model.AllowManage = true;

            return View("Edit", model);
        }

        [MvcSiteMapNode(Title = "$t:Titles.Permissions", AreaName = "WebContent", ParentKey = "WebContent.Category.Edit")]
        public virtual ActionResult ShowPermissions(long categoryId)
        {
            var category = categoryService.Find(categoryId);

            if (category == null || !permissionService.IsAllowed((Int32)CategoryOperations.Permissions, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Notfound", ResourceHelper.GetControllerScope(this)));
            }

            return View("ShowPermissions", permissionsHelper.BindPermissionsModel(category.Id, typeof(WebContentCategory), false));
        }

        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionsModel model)
        {
            var category = categoryService.Find(model.EntityId);

            if (category != null)
            {
                if (permissionService.IsAllowed((Int32)CategoryOperations.Permissions, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
                {
                    permissionsHelper.ApplyPermissions(model, typeof(WebContentCategory));
                }
                if (permissionService.IsAllowed((Int32)CategoryOperations.Permissions, this.CorePrincipal(), typeof(WebContentCategory), category.Id, IsCategoryOwner(category), PermissionOperationLevel.Object))
                {
                    Success(HttpContext.Translate("Messages.PermitionsSuccess", ResourceHelper.GetControllerScope(this)));
                    return Content(Url.Action("ShowPermissions", "WebContentCategory", new { categoryId = category.Id }));
                }
                Error(String.Format(HttpContext.Translate("Messages.PermitionsUnSuccess", ResourceHelper.GetControllerScope(this)), model.EntityId));
            }
            Error(String.Format(HttpContext.Translate("Messages.NotFoundEntity", ResourceHelper.GetControllerScope(this)), model.EntityId));

            return Content(Url.Action("Show"));
        }

        #endregion

        #region Helper Methods

        private bool IsCategoryOwner(WebContentCategory category)
        {
            return category != null && this.CorePrincipal() != null && category.UserId != null &&
                             category.UserId == this.CorePrincipal().PrincipalId;
        }

        public GridViewModel BuildCategoriesGrid()
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
                                                                 Name = HttpContext.Translate("Actions", ResourceHelper.GetControllerScope(this)),
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
                DataUrl = Url.Action("LoadData", "WebContentCategory"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("Titles.Categories", String.Empty),
                Columns = columns,
                IsRowNotClickable = true
            };

            return model;
        }

        #endregion

        public override string ControllerPluginIdentifier
        {
            get { return WebContentPlugin.Instance.Identifier; }
        }
    }
}
