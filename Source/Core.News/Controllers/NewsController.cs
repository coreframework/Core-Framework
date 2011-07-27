using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.News.Models;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.News.Permissions.Operations;
using Framework.Core.Localization;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using INewsService = Core.News.Nhibernate.Contracts.INewsArticleService;
using System.Linq.Dynamic;

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
        
        private readonly INewsService newsArticlesService;

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
            newsArticlesService = ServiceLocator.Current.GetInstance<INewsService>();
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
                                                                 Name = "Title", 
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
                DataUrl = Url.Action("DynamicGridData","News"),
                DefaultOrderColumn = "Id",
                GridTitle = "News",
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
            IQueryable<NewsArticle> searchQuery = newsArticlesService.GetSearchQuery(search);
            int totalRecords = newsArticlesService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var newsArticles = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from article in newsArticles
                    select new
                    {
                        id = article.Id,
                        cell = new[] {  article.Title, 

                                        String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("Edit","News",new { id = article.Id }),"Edit"),
                                        String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("Remove","News",new { id = article.Id }))}
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
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Edit", new NewsArticleLocaleViewModel().MapFrom(article));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long newsArticleId, String culture)
        {
            var newsArticle = newsArticlesService.Find(newsArticleId);
            if (newsArticle == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }
            var model = new NewsArticleLocaleViewModel().MapFrom(newsArticle);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<INewsArticleLocaleService>();
            var locale = localeService.GetLocale(newsArticleId, culture);
            if(locale != null)
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
        /// <param name="contentPage">The content page model.</param>
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
                newsArticleLocale.Content = newsArticleLocaleViewModel.Content;
                localeService.Save(newsArticleLocale);
                Success(HttpContext.Translate("Messages.SaveSuccess", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }

            Error(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
            return View("Admin/Edit", newsArticleLocaleViewModel);
        }

        /// <summary>
        /// Shows content page create form.
        /// </summary>
        /// <returns>Content page create form.</returns>
        public virtual ActionResult New()
        {
            return View("Admin/New", new NewsArticleViewModel());
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
                newsArticlesService.Save(newsArticle.MapTo(new NewsArticle()));
                Success(HttpContext.Translate("Messages.SaveSuccess", ResourceHelper.GetControllerScope(this)));
                return RedirectToAction("ShowAll");
            }

            Error(HttpContext.Translate("Messages.ValidationError", ResourceHelper.GetControllerScope(this)));
            return View("Admin/New", newsArticle);
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
                Success("Sucessfully remove news article.");
                newsArticlesService.Delete(newsArticle);
                return RedirectToAction("ShowAll");
            }

            Error((HttpContext.Translate("Messages.UnknownError", ResourceHelper.GetControllerScope(this))));
            return RedirectToAction("ShowAll");
        }

        #endregion
    }
}
