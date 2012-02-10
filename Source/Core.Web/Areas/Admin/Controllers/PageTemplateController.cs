using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Core.Web.Helpers.Layouts;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Mvc.Controllers;
using Framework.Mvc.Grids;
using Framework.Mvc.Grids.JqGrid;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(PageTemplate))]
    public partial class PageTemplateController : FrameworkController
    {
        #region Fields

        private readonly IPageService pageService;
        private readonly IPageLocaleService pageLocaleService;
        private readonly IPermissionCommonService permissionService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageController"/> class.
        /// </summary>
        public PageTemplateController()
        {
            pageService = ServiceLocator.Current.GetInstance<IPageService>();
            pageLocaleService = ServiceLocator.Current.GetInstance<IPageLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
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
                                                                 Index = "page.Url"
                                                             },
                                                        new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.PageLocale.InMainMenu"),
                                                                 Index = "page.InMainMenu"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Actions.Actions"),
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 10,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {Name = "Id", Sortable = false, Hidden = true}
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.PageTemplate.DynamicGridData()),
                DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.PageTemplate.Edit())),
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
            ICriteria searchCriteria = pageLocaleService.GetSearchCriteria(search, true);
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
                    id = pageLocale.Page.Id,
                    cell = new[]
                                                                       {
                                                                            pageLocale.Title, 
                                                                            pageLocale.Page.Url,
                                                                            pageLocale.Page.HideInMainMenu ? Translate("Boolean.False") : Translate("Boolean.True"),
                                                                            String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.PageTemplates.Show(pageLocale.Page.Url)), Translate("Actions.View")),
                                                                            String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action(MVC.Admin.PageTemplate.Remove(pageLocale.Page.Id)))
                                                                       }
                }).ToArray())
            };
            return Json(jsonData);
        }

        [HttpGet]
        public virtual ActionResult New()
        {
            return View(new PageTemplateViewModel().MapFrom(new PageTemplate()));
        }

        [HttpPut]
        public virtual ActionResult Create(PageTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pageTemplate = new Page { IsTemplate = true };
                pageTemplate.PageLayout = new PageLayout
                {
                    LayoutTemplate = LayoutHelper.DefaultLayoutTemplate,
                    Page = pageTemplate
                };

                if (this.CorePrincipal() != null)
                {
                    pageTemplate.User = new User
                                    {
                                        Id = this.CorePrincipal().PrincipalId
                                    };
                }

                pageTemplate = model.MapTo(pageTemplate);

                if (pageService.Save(pageTemplate))
                {
                    permissionService.SetupDefaultRolePermissions(
                        ResourcePermissionsHelper.GetResourceOperations(typeof(PageTemplate)), typeof(PageTemplate), pageTemplate.Id);
                    Success(Translate("Messages.PageTemplateCreated"));

                    return RedirectToAction(MVC.Admin.PageTemplate.Index());
                }
            }

            Error(Translate("Messages.ValidationError"));
            return View("New", model);
        }

        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long id)
        {
            var pageTemplate = pageService.Find(id);
            if (pageTemplate == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            return View(new PageTemplateViewModel().MapFrom(pageTemplate));
        }

        [HttpPost]
        public virtual ActionResult Update(long id, PageTemplateViewModel model)
        {
            var pageTemplate = pageService.Find(id);
            if (pageTemplate == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (ModelState.IsValid)
            {
                //UserHelper.Update(user, model);
                Success(Translate("Messages.PageTemplateUpdated"));
                return RedirectToAction(MVC.Admin.PageTemplate.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("Edit", model);
        }

        [HttpGet]
        public virtual ActionResult Remove(long id)
        {
            var pageTemplate = pageService.Find(id);
            if (pageTemplate == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            return View(new PageTemplateViewModel().MapFrom(pageTemplate));
        }

        [HttpDelete]
        public virtual ActionResult ConfirmRemove(long id)
        {
            var pageTemplate = pageService.Find(id);
            if (pageTemplate != null)
            {
                PageHelper.UnlinkTemplatePages(pageTemplate);
                pageService.Delete(pageTemplate);
                Success(Translate("Messages.PageTemplateDeleted"));
            }

            return RedirectToAction(MVC.Admin.PageTemplate.Index());
        }
    }
}