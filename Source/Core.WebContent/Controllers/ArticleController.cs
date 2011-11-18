using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.WebContent.Helpers;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.Permissions.Operations;
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
    [Export(typeof(IController)), ExportMetadata("Name", "Article")]
    [Permissions((int)WebContentPluginOperations.ManageArticles, typeof(WebContentPlugin))]
    public partial class ArticleController : CorePluginController
    {
        #region Fields

        private readonly IArticleService articleService;
        private readonly IArticleLocaleService articleLocaleService;
        private readonly IPermissionCommonService permissionService;
        private readonly IPermissionsHelper permissionsHelper;
        private readonly ICategoryService categoryService;
        private readonly IArticleFileService articleFileService;

        #endregion

        #region Constructor

        public ArticleController()
        {
            articleService = ServiceLocator.Current.GetInstance<IArticleService>();
            articleLocaleService = ServiceLocator.Current.GetInstance<IArticleLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();
            categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();
            articleFileService = ServiceLocator.Current.GetInstance<IArticleFileService>();
        }

        #endregion

        #region Actions

        [MvcSiteMapNode(Title = "$t:Titles.Articles", AreaName = "WebContent", ParentKey = "Home", Key = "WebContent.Article.Show")]
        public virtual ActionResult Show()
        {
            return View(BuildArticlesGrid());
        }

        [HttpPost]
        public virtual JsonResult LoadData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = articleLocaleService.GetSearchCriteria(search, this.CorePrincipal(), (Int32)ArticleOperations.View);
            long totalRecords = articleLocaleService.Count(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var articles = searchQuery.AddOrder(new Order(sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<ArticleLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from article in articles
                    select new
                    {
                        id = article.Id,
                        cell = new[] {  
                                        article.Title,
                                        ((WebContentCategoryLocale)article.Article.Category.CurrentLocale).Title,
                                        article.Article.Status.ToString(),
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","Article",new { articleId = article.Article.Id }),HttpContext.Translate("Details", ResourceHelper.GetControllerScope(this)))
                        }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "WebContent", ParentKey = "WebContent.Article.Show")]
        public virtual ActionResult New()
        {
            return View(new ArticleViewModel { AllowManage = true});
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(ArticleViewModel article)
        {
            ArticleHelper.ValidateArticle(article, ModelState);
            if (ModelState.IsValid)
            {
                var newArticle = article.MapTo(new Article
                                                   {
                                                       UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null,
                                                       CreateDate = DateTime.Now
                                                   });
                if (articleService.Save(newArticle))
                {
                    permissionService.SetupDefaultRolePermissions(OperationsHelper.GetOperations<ArticleOperations>(), typeof(Article), newArticle.Id);
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.Article.Edit(newArticle.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            }

            article.AllowManage = true;
            return View("New", article);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="articleId">The article id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "WebContent", ParentKey = "WebContent.Article.Show", Key = "WebContent.Article.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long articleId)
        {
            var article = articleService.Find(articleId);

            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.View, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(),
                                                            typeof(Article), article.Id, IsArticleOwner(article),
                                                            PermissionOperationLevel.Object);

            return View("Edit", new ArticleViewModel { AllowManage = allowManage }.MapFrom(article));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long articleId, String culture)
        {
            var article = articleService.Find(articleId);

            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.View, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(),
                                                        typeof(Article), article.Id, IsArticleOwner(article),
                                                        PermissionOperationLevel.Object);

            ArticleViewModel model = new ArticleViewModel { AllowManage = allowManage }.MapFrom(article);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IArticleLocaleService>();
            ArticleLocale locale = localeService.GetLocale(articleId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("ArticleDetails", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Save(ArticleViewModel model)
        {
            ArticleHelper.ValidateArticle(model, ModelState);
            if (ModelState.IsValid)
            {
                var article = articleService.Find(model.Id);

                if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (articleService.Save(model.MapTo(article)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<IArticleLocaleService>();
                    ArticleLocale locale = localeService.GetLocale(article.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new ArticleLocale { Article = article });

                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.Article.Edit(model.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            }

            model.AllowManage = true;

            return View("Edit", model);
        }

        [MvcSiteMapNode(Title = "$t:Titles.Permissions", AreaName = "WebContent", ParentKey = "WebContent.Article.Edit")]
        public virtual ActionResult ShowPermissions(long articleId)
        {
            var article = articleService.Find(articleId);

            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.Permissions, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Notfound", ResourceHelper.GetControllerScope(this)));
            }

            return View("ShowPermissions", permissionsHelper.BindPermissionsModel(article.Id, typeof(Article), false));
        }

        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionsModel model)
        {
            var article = articleService.Find(model.EntityId);

            if (article != null)
            {
                if (permissionService.IsAllowed((Int32)ArticleOperations.Permissions, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
                {
                    permissionsHelper.ApplyPermissions(model, typeof(Article));
                }
                if (permissionService.IsAllowed((Int32)ArticleOperations.Permissions, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
                {
                    Success(HttpContext.Translate("Messages.PermitionsSuccess", ResourceHelper.GetControllerScope(this)));
                    return Content(Url.Action("ShowPermissions", "Article", new { articleId = article.Id }));
                }
                Error(String.Format(HttpContext.Translate("Messages.PermitionsUnSuccess", ResourceHelper.GetControllerScope(this)), model.EntityId));
            }
            Error(String.Format(HttpContext.Translate("Messages.NotFoundEntity", ResourceHelper.GetControllerScope(this)), model.EntityId));

            return Content(Url.Action("Show"));
        }

        public virtual ActionResult SectionCategories(long sectionId, long? categoryId)
        {
            var categories = categoryService.GetAllowedSectionCategoriesByOperation(this.CorePrincipal(), (Int32)CategoryOperations.View, sectionId).ToList().Select(a => new SelectListItem
                                                                                                                                                                              {
                Text = ((WebContentCategoryLocale)a.CurrentLocale).Title,
                Value = a.Id.ToString(),
                Selected = (categoryId != null && a.Id == categoryId)? true : false

            });

            return Json(categories);
        }

        public virtual ActionResult Remove(long articleId)
        {
            var article = articleService.Find(articleId);
            if (article != null && permissionService.IsAllowed((Int32)CategoryOperations.Manage, this.CorePrincipal(), typeof(Article), articleId, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                articleService.Delete(article);
            }

            return RedirectToAction("Show");
        }

        #region ArticleFiles

        [MvcSiteMapNode(Title = "$t:Titles.ArticleFiles", AreaName = "WebContent", ParentKey = "WebContent.Article.Show", Key = "WebContent.Article.ShowFiles")]
        public virtual ActionResult ShowFiles(long articleId)
        {
            var article = articleService.Find(articleId);

            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.View, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = HttpContext.Translate("Title", ResourceHelper.GetControllerScope(this)), 
                                                                 Sortable = false,
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
                DataUrl = Url.Action("LoadFilesData", "Article", new { articleId = article.Id }),
                DefaultOrderColumn = "Title",
                GridTitle = HttpContext.Translate("Titles.ArticleFiles", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                IsRowNotClickable = true
            };

            bool allowManage = permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(),
                                                            typeof(Article), article.Id, IsArticleOwner(article),
                                                            PermissionOperationLevel.Object);

            ViewData["Article"] = new ArticleViewModel { Id = article.Id, AllowManage = allowManage };

            return View("ArticleFiles", model);
        }

        [HttpPost]
        public virtual JsonResult LoadFilesData(int articleId, int page, int rows, String search, String sidx, String sord)
        {
            var article = articleService.Find(articleId);

            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.View, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(),
                                                            typeof(Article), article.Id, IsArticleOwner(article),
                                                            PermissionOperationLevel.Object);

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<ArticleFile> searchQuery = articleFileService.GetSearchQuery(search);
            int totalRecords = articleFileService.GetCount(searchQuery);

            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var files = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from file in files
                    select new
                    {
                        id = file.Id,
                        cell = new[] {String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("EditFile", "Article", new {articleFileId = file.Id, articleId = file.Article.Id}), HttpUtility.HtmlEncode(file.Title)),
                            allowManage?String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("RemoveFile", "Article", new {articleFileId = file.Id})):String.Empty }
                    }).ToArray()
            };

            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "$t:Titles.NewFile", AreaName = "WebContent", ParentKey = "WebContent.Article.Show")]
        public virtual ActionResult NewFile(long articleId)
        {
            var article = articleService.Find(articleId);
            if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            return View("ArticleFileDetails", new ArticleFileViewModel { ArticleId = articleId, AllowManage = true});
        }

        [HttpGet]
     /*   [MvcSiteMapNode(Title = "$t:Titles.EditFile", AreaName = "WebContent", ParentKey = "WebContent.Article.Show")]
        [SiteMapTitle("Title")]*/
        public virtual ActionResult EditFile(long articleId, long articleFileId)
        {
            var articleFile = articleFileService.Find(articleFileId);

            if (articleFile == null || articleFile.Article == null || !permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(), typeof(Article), articleFile.Article.Id, IsArticleOwner(articleFile.Article), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            return View("ArticleFileDetails", new ArticleFileViewModel { ArticleId = articleId, AllowManage = true}.MapFrom(articleFile));
        }
       
        [HttpPost]
        public virtual ActionResult SaveFile(long articleId, ArticleFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = articleService.Find(articleId);
                if (article == null || !permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(), typeof(Article), article.Id, IsArticleOwner(article), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (articleFileService.Save(model.MapTo(new ArticleFile())))
                {
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.Article.ShowFiles(articleId));
                }
            }

            model.AllowManage = true;
            Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            return View("ArticleFileDetails", model);
        }

        public virtual ActionResult RemoveFile(long articleFileId)
        {
            var articleElement = articleFileService.Find(articleFileId);
            if (articleElement != null && permissionService.IsAllowed((Int32)ArticleOperations.Manage, this.CorePrincipal(), typeof(Article), articleElement.Article.Id, IsArticleOwner(articleElement.Article), PermissionOperationLevel.Object))
            {
                if (articleFileService.Delete(articleElement))
                {
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction("ShowFiles", new {articleId = articleElement.Article.Id});
                }
            }

            Error(HttpContext.Translate("Messages.UnknownError", String.Empty));
            return Content(String.Empty);
        }

        #endregion

        #endregion

        #region Helper Methods

        private bool IsArticleOwner(Article article)
        {
            return article != null && this.CorePrincipal() != null && article.UserId != null &&
                             article.UserId == this.CorePrincipal().PrincipalId;
        }

        public GridViewModel BuildArticlesGrid()
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
                                                                 Name = HttpContext.Translate("Category", ResourceHelper.GetControllerScope(this)), 
                                                                 Index = "Category.CurrentLocale.Title",
                                                                 Width = 400
                                                             },
                                                         new GridColumnViewModel
                                                            {
                                                                Name = HttpContext.Translate("Status", ResourceHelper.GetControllerScope(this)), 
                                                                Index = "Status",
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
                DataUrl = Url.Action("LoadData", "Article"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("Titles.Articles", String.Empty),
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
