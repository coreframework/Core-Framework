using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.News.Helpers;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.News.Permissions.Operations;
using Framework.Core.Localization;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.News.Controllers
{
    /// <summary>
    /// Handles module requests.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "News")]
    [Permissions((int)NewsPluginOperations.ManageNews, typeof(NewsPlugin))]
    public partial class NewsController : CorePluginController
    {
        #region Fields

        private readonly INewsArticleService newsArticlesService;
        private readonly INewsArticleLocaleService newsArticlesLocaleService;
        private readonly INewsCategoryLocaleService categoryLocaleService;

        #endregion

        #region Properties

        public override string ControllerPluginIdentifier
        {
            get { return NewsPlugin.Instance.Identifier; }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsController"/> class.
        /// </summary>
        public NewsController()
        {
            newsArticlesService = ServiceLocator.Current.GetInstance<INewsArticleService>();
            newsArticlesLocaleService = ServiceLocator.Current.GetInstance<INewsArticleLocaleService>();
            categoryLocaleService = ServiceLocator.Current.GetInstance<INewsCategoryLocaleService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders content pages listing.
        /// </summary>
        /// <returns>List of news articles.</returns>
        
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
                                                                 Name = HttpContext.Translate("Status", ResourceHelper.GetControllerScope(this)), 
                                                                 Index = "newsArticle.StatusId",
                                                                 Width = 150
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
                DataUrl = Url.Action("DynamicGridData","News"),
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
            ICriteria searchCriteria = newsArticlesLocaleService.GetSearchCriteria(search);

            long totalRecords = newsArticlesLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var newsArticles = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<NewsArticleLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from articleLocale in newsArticles
                    select new
                    {
                        id = articleLocale.NewsArticle.Id,
                        cell = new[] {  articleLocale.NewsArticle.Title, ((NewsStatus)articleLocale.NewsArticle.StatusId).ToString(),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Categories","News", new { id = articleLocale.NewsArticle.Id }), HttpContext.Translate("Categories", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("Edit","News",new { id = articleLocale.NewsArticle.Id }),HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("Remove","News",new { id = articleLocale.NewsArticle.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows content page edit form.
        /// </summary>
        /// <param name="id">The content id.</param>
        /// <returns>Content page edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var article = newsArticlesService.Find(id ?? 0);
            if (article == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.Pagenotfound", ResourceHelper.GetControllerScope(this)));
            }
            var model = new NewsArticleLocaleViewModel().MapFrom(article);
            model.PublishingAccess =
                ServiceLocator.Current.GetInstance<IPermissionCommonService>().IsAllowed(
                    (Int32)NewsPluginOperations.PublishingNews, this.CorePrincipal(), typeof(NewsPlugin), null);

            return View("Edit", new NewsArticleLocaleViewModel().MapFrom(article));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long newsArticleId, String culture)
        {
            var newsArticle = newsArticlesService.Find(newsArticleId);
            if (newsArticle == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.Pagenotfound", ResourceHelper.GetControllerScope(this)));
            }
            var model = new NewsArticleLocaleViewModel().MapFrom(newsArticle);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<INewsArticleLocaleService>();
            var locale = localeService.GetLocale(newsArticleId, culture);
            if(locale != null)
            {
                model.Title = locale.Title;
                model.Content = locale.Content;
                model.Summary = locale.Summary;
                model.StatusId = locale.NewsArticle.StatusId;
                model.CreateDate = locale.NewsArticle.CreateDate;
                model.LastModifiedDate = locale.NewsArticle.LastModifiedDate;
            }
            model.PublishingAccess =
                ServiceLocator.Current.GetInstance<IPermissionCommonService>().IsAllowed(
                    (Int32)NewsPluginOperations.PublishingNews, this.CorePrincipal(), typeof(NewsPlugin), null);
            return PartialView("EditForm", model);
        }

        /// <summary>
        /// Updates content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <param name="newsArticleLocaleViewModel">The news article locale view model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, NewsArticleLocaleViewModel newsArticleLocaleViewModel)
        {
            newsArticleLocaleViewModel.Cultures = CultureHelper.GetAvailableCultures();
            if (ModelState.IsValid && id!=null)
            {
                var newsArticle = newsArticlesService.Find((long)id);
                var localeService = ServiceLocator.Current.GetInstance<INewsArticleLocaleService>();
                var newsArticleLocale = localeService.GetLocale(id.Value, newsArticleLocaleViewModel.SelectedCulture);
                if (newsArticleLocale == null)
                {
                    newsArticleLocale = new NewsArticleLocale { NewsArticle = newsArticle, Culture = newsArticleLocaleViewModel.SelectedCulture };
                }
                newsArticleLocale.Title = newsArticleLocaleViewModel.Title;
                newsArticleLocale.Summary = newsArticleLocaleViewModel.Summary;
                newsArticleLocale.Content = newsArticleLocaleViewModel.Content;
                newsArticleLocale.NewsArticle.LastModifiedDate = DateTime.Now;
                newsArticleLocale.NewsArticle.StatusId = newsArticleLocaleViewModel.StatusId;
                newsArticleLocale.NewsArticle.PublishDate = newsArticleLocaleViewModel.PublishDate;
                localeService.Save(newsArticleLocale);
                Success(HttpContext.Translate("Messages.SaveSuccess", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }

            Error(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
            return View("Edit", newsArticleLocaleViewModel);
        }

        /// <summary>
        /// Shows content page create form.
        /// </summary>
        /// <returns>Content page create form.</returns>
        public virtual ActionResult New()
        {
            var model = new NewsArticleViewModel{PublishDate = DateTime.Now};
            model.PublishingAccess =
                ServiceLocator.Current.GetInstance<IPermissionCommonService>().IsAllowed(
                    (Int32)NewsPluginOperations.PublishingNews, this.CorePrincipal(), typeof(NewsPlugin), null);
            return View("New", model);
        }

        /// <summary>
        /// Saves new content page.
        /// </summary>
        /// <param name="newsArticle">The news article.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(NewsArticleViewModel newsArticle)
        {
            if (ModelState.IsValid)
            {
                newsArticle.LastModifiedDate = DateTime.Now;
                newsArticle.CreateDate = DateTime.Now;
                newsArticlesService.Save(newsArticle.MapTo(new NewsArticle()));
                Success(HttpContext.Translate("Messages.SaveSuccess", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }

            Error(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
            return View("New", newsArticle);
        }

        /// <summary>
        /// Removes the specified content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <returns>List of content pages</returns>
        //[HttpPost]
        public virtual ActionResult Remove(long id)
        {
            var newsArticle = newsArticlesService.Find(id);
            if (newsArticle != null)
            {
                Success(HttpContext.Translate("Messages.RemoveSuccess", ResourceHelper.GetControllerScope(this)));
                newsArticlesService.Delete(newsArticle);
                return RedirectToAction("ShowAll");
            }

            Error((HttpContext.Translate("Messages.UnknownError", ResourceHelper.GetControllerScope(this))));
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
            var newsArticle = newsArticlesService.Find(id);
            if (newsArticle == null)
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
                DataUrl = Url.Action("NewsCategoriesDynamicGridData", "News"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("GridTitleCategory", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = newsArticle.Categories.Select(t => t.Id),
                Title = newsArticle.Title
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult NewsCategoriesDynamicGridData(int id, int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var user = newsArticlesService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
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
                        cell = new[] { categoryLocale.Title }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        public virtual JsonResult UpdateCategories(long id, IEnumerable<string> ids, IEnumerable<string> selids)
        {
            var newsArticle = newsArticlesService.Find(id);
            if (newsArticle == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (NewsArticleHelper.UpdateCategoriesToProductAssignment(newsArticle, ids, selids))
            {
                Success(HttpContext.Translate("Messages.CategoriesUpdated", ResourceHelper.GetControllerScope(this)));
                return Json(true);
            }

            return Json(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
        }

        #endregion
    }
}
