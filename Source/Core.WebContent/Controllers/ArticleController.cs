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
    [Export(typeof(IController)), ExportMetadata("Name", "Article")]
    public partial class ArticleController : CorePluginController
    {
        #region Fields

        private readonly IArticleService articleService;
        private readonly IArticleLocaleService articleLocaleService;
        private readonly IPermissionCommonService permissionService;
        private readonly IPermissionsHelper permissionsHelper;

        #endregion

        #region Constructor

        public ArticleController()
        {
            articleService = ServiceLocator.Current.GetInstance<IArticleService>();
            articleLocaleService = ServiceLocator.Current.GetInstance<IArticleLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();
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
                                        ((SectionLocale)article.Article.Section.CurrentLocale).Title,
                                        article.Article.CreateDate.ToString(),
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
            if (ModelState.IsValid)
            {
                var newArticle = article.MapTo(new Article { UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null });
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

        [HttpPost]
        public virtual ActionResult Save(ArticleViewModel model)
        {
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
                                                                 Name = HttpContext.Translate("Section", ResourceHelper.GetControllerScope(this)), 
                                                                 Index = "Section.CurrentLocale.Title",
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
