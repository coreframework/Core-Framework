using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using Products.Helpers;
using Products.Models;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;
using Products.Permissions.Operations;
using System.Linq.Dynamic;

namespace Products.Controllers
{
    /// <summary>
    /// Handles module requests.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "Product")]
    [Permissions((int)ProductsPluginOperations.ManageProducts, typeof(ProductPlugin))]
    public partial class ProductController : CorePluginController
    {
        #region Fields

        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        #endregion

        #region Properties
        /// <summary>
        /// Controller Plugin Identifier
        /// </summary>
        public override string ControllerPluginIdentifier
        {
            get { return ProductPlugin.Instance.Identifier; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        public ProductController()
        {
            productService = ServiceLocator.Current.GetInstance<IProductService>();
            categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders products listing.
        /// </summary>
        /// <returns>List of products.</returns>

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
                                                                 Name = HttpContext.Translate("CategoriesTitle", ResourceHelper.GetControllerScope(this)),
                                                                 Width = 150,
                                                                 Sortable = false
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
                DataUrl = Url.Action("DynamicGridData", "Product"),
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
            IQueryable<Product> searchQuery = productService.GetSearchQuery(search);
            int totalRecords = productService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var products = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from product in products
                    select new
                    {
                        id = product.Id,
                        cell = new[] {  product.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Categories","Product", new { id = product.Id }),
                                            HttpContext.Translate("Categories", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("ShowById","Product",new { id = product.Id }),HttpContext.Translate("View", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","Product",new { id = product.Id }),HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                            Url.Action("Remove","Product",new { id = product.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows product details by id.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns>Product details</returns>
        public virtual ActionResult ShowById(long? id)
        {
            var product = productService.Find(id ?? 0);
            if (product == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("Show", new ProductLocaleViewModel().MapFrom(product));
        }

        /// <summary>
        /// Shows product edit form.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns>Product edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var product = productService.Find(id ?? 0);
            if (product == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("Edit", new ProductLocaleViewModel().MapFrom(product));
        }

        /// <summary>
        /// Updates product.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <param name="product">The product model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, ProductLocaleViewModel productModel)
        {
            if (ModelState.IsValid && id != null)
            {
                var localeService = ServiceLocator.Current.GetInstance<IProductLocaleService>();
                var productLocale = localeService.GetLocale((long)id, productModel.SelectedCulture);
                var product = productService.Find((long)id);
               
                productLocale = productLocale ?? new ProductLocale
                {
                    Product = productModel.MapTo(product),
                    Culture = productModel.SelectedCulture
                };

                productLocale.Title = productModel.Title;
                productLocale.Description = productModel.Description;

                localeService.Save(productLocale);

                productService.Save(productModel.MapTo(product));

                Success(HttpContext.Translate("SuccessUpdate", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }
            Error(HttpContext.Translate("Error", ResourceHelper.GetControllerScope(this)));
            return View("Edit", productModel);
        }

        /// <summary>
        /// Shows product create form.
        /// </summary>
        /// <returns>Product create form.</returns>
        public virtual ActionResult New()
        {
            return View("New", new ProductViewModel());
        }

        /// <summary>
        /// Saves new product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                productService.Save(product.MapTo(new Product(){CreateDate = DateTime.Now}));
                Success(HttpContext.Translate("SuccessCreate", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }
            Error(HttpContext.Translate("Error", ResourceHelper.GetControllerScope(this)));
            return View("New", product);
        }


        /// <summary>
        /// Removes the specified product.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns>List of product</returns>
        public virtual ActionResult Remove(long id)
        {
            var product = productService.Find(id);
            if (product != null)
            {
                productService.Delete(product);
                Success(HttpContext.Translate("SuccessDelete", ResourceHelper.GetControllerScope(this)));
            }

            return RedirectToAction("ShowAll");
        }

        /// <summary>
        /// Categories.
        /// </summary>
        /// <param name="id">The product id.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Categories(long id)
        {
            var product = productService.Find(id);
            if (product == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = HttpContext.Translate("TitleCategory", ResourceHelper.GetControllerScope(this)), 
                                                                 Index = "Title",
                                                                 Width = 1100
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
                DataUrl = Url.Action("ProductCategoriesDynamicGridData", "Product"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("GridTitleCategory", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = product.Categories.Select(t => t.Id),
                Title = product.Title
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ProductCategoriesDynamicGridData(int id, int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var user = productService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            var categories = categoryService.GetCategories(search); //categoryService.GetSearchQuery(search);
            int totalRecords = categories.Count();//categoryService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            categories = sord == "asc"
                             ? categories.OrderBy(cat => cat.Title).Skip(pageIndex * pageSize).Take(pageSize).ToList()
                             : categories.OrderByDescending(cat => cat.Title).Skip(pageIndex * pageSize).Take(pageSize).
                                   ToList();
            //var categories = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from category in categories
                    select new
                    {
                        id = category.Id,
                        cell = new[] { category.Title }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        public virtual JsonResult UpdateCategories(long id, IEnumerable<string> ids, IEnumerable<string> selids)
        {
            var product = productService.Find(id);
            if (product == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (ProductHelper.UpdateCategoriesToProductAssignment(product, ids, selids))
            {
                Success(HttpContext.Translate("Messages.CategoriesUpdated", ResourceHelper.GetControllerScope(this)));
                return Json(true);
            }

            return Json(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Change Language
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="culture">Selected culture</param>
        /// <param name="isShow">Is show</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ChangeLanguage(long productId, String culture, bool isShow)
        {
            var product = productService.Find(productId);
            if (product == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("NotFound", ResourceHelper.GetControllerScope(this)));
            }
            var model = new ProductLocaleViewModel().MapFrom(product);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<IProductLocaleService>();
            var locale = localeService.GetLocale(productId, culture);
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
