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
    [Export(typeof(IController)), ExportMetadata("Name", "Section")]
    [Permissions((int)WebContentPluginOperations.ManageSections, typeof(WebContentPlugin))]
    public partial class SectionController : CorePluginController
    {
        #region Fields

        private readonly ISectionService sectionService;
        private readonly ISectionLocaleService sectionLocaleService;
        private readonly IPermissionCommonService permissionService;
        private readonly IPermissionsHelper permissionsHelper;

        #endregion

        #region Constructor

        public SectionController()
        {
            sectionService = ServiceLocator.Current.GetInstance<ISectionService>();
            sectionLocaleService = ServiceLocator.Current.GetInstance<ISectionLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();
        }

        #endregion

        #region Actions

        [MvcSiteMapNode(Title = "$t:Titles.Sections", AreaName = "WebContent", ParentKey = "Home", Key = "WebContent.Section.Show")]
        public virtual ActionResult Show()
        {
            return View(BuildSectionsGrid());
        }

        [HttpPost]
        public virtual JsonResult LoadData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = sectionLocaleService.GetSearchCriteria(search, this.CorePrincipal(), (Int32)SectionOperations.View);
            long totalRecords = sectionLocaleService.Count(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var sections = searchQuery.AddOrder(new Order(sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<SectionLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from section in sections
                    select new
                    {
                        id = section.Id,
                        cell = new[] {  
                                        section.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","Section",new { sectionId = section.Section.Id }),HttpContext.Translate("Details", ResourceHelper.GetControllerScope(this)))
                        }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "WebContent", ParentKey = "WebContent.Section.Show")]
        public virtual ActionResult New()
        {
            return View(new SectionViewModel { AllowManage = true, SectionSettings = new SectionSettings()});
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(SectionViewModel section)
        {
            if (ModelState.IsValid)
            {
                var newSection = section.MapTo(new Section { UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null });
                if (sectionService.Save(newSection))
                {
                    permissionService.SetupDefaultRolePermissions(OperationsHelper.GetOperations<SectionOperations>(), typeof(Section), newSection.Id);
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.Section.Show());
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            }

            section.AllowManage = true;
            return View("New", section);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="sectionId">The section id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "WebContent", ParentKey = "WebContent.Section.Show", Key = "WebContent.Section.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long sectionId)
        {
            var section = sectionService.Find(sectionId);

            if (section == null || !permissionService.IsAllowed((Int32)SectionOperations.View, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)SectionOperations.Manage, this.CorePrincipal(),
                                                            typeof(Section), section.Id, IsSectionOwner(section),
                                                            PermissionOperationLevel.Object);

            return View("Edit", new SectionViewModel { AllowManage = allowManage }.MapFrom(section));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long sectionId, String culture)
        {
            var section = sectionService.Find(sectionId);

            if (section == null || !permissionService.IsAllowed((Int32)SectionOperations.View, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)SectionOperations.Manage, this.CorePrincipal(),
                                                        typeof(Section), section.Id, IsSectionOwner(section),
                                                        PermissionOperationLevel.Object);

            SectionViewModel model = new SectionViewModel { AllowManage = allowManage }.MapFrom(section);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<ISectionLocaleService>();
            SectionLocale locale = localeService.GetLocale(sectionId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("SectionDetails", model);
        }

        [HttpPost]
        public virtual ActionResult Save(SectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var section = sectionService.Find(model.Id);

                if (section == null || !permissionService.IsAllowed((Int32)SectionOperations.Manage, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (sectionService.Save(model.MapTo(section)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<ISectionLocaleService>();
                    SectionLocale locale = localeService.GetLocale(section.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new SectionLocale { Section = section });

                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(WebContentMVC.Section.Edit(model.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            }

            model.AllowManage = true;

            return View("Edit", model);
        }

        [MvcSiteMapNode(Title = "$t:Titles.Permissions", AreaName = "WebContent", ParentKey = "WebContent.Section.Edit")]
        public virtual ActionResult ShowPermissions(long sectionId)
        {
            var section = sectionService.Find(sectionId);

            if (section == null || !permissionService.IsAllowed((Int32)SectionOperations.Permissions, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Notfound", ResourceHelper.GetControllerScope(this)));
            }

            return View("ShowPermissions", permissionsHelper.BindPermissionsModel(section.Id, typeof(Section), false));
        }

        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionsModel model)
        {
            var section = sectionService.Find(model.EntityId);

            if (section != null)
            {
                if (permissionService.IsAllowed((Int32)SectionOperations.Permissions, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
                {
                    permissionsHelper.ApplyPermissions(model, typeof(Section));
                }
                if (permissionService.IsAllowed((Int32)SectionOperations.Permissions, this.CorePrincipal(), typeof(Section), section.Id, IsSectionOwner(section), PermissionOperationLevel.Object))
                {
                    Success(HttpContext.Translate("Messages.PermitionsSuccess", ResourceHelper.GetControllerScope(this)));
                    return Content(Url.Action("ShowPermissions", "Section", new { sectionId = section.Id }));
                }
                Error(String.Format(HttpContext.Translate("Messages.PermitionsUnSuccess", ResourceHelper.GetControllerScope(this)), model.EntityId));
            }
            Error(String.Format(HttpContext.Translate("Messages.NotFoundEntity", ResourceHelper.GetControllerScope(this)), model.EntityId));

            return Content(Url.Action("Show"));
        }

        public virtual ActionResult Remove(long sectionId)
        {
            var section = sectionService.Find(sectionId);
            if (section != null && permissionService.IsAllowed((Int32)SectionOperations.Manage, this.CorePrincipal(), typeof(Section), sectionId, IsSectionOwner(section), PermissionOperationLevel.Object))
            {
                sectionService.Delete(section);
            }

            return RedirectToAction("Show");
        }

        #endregion

        #region Helper Methods

        private bool IsSectionOwner(Section section)
        {
            return section != null && this.CorePrincipal() != null && section.UserId != null &&
                             section.UserId == this.CorePrincipal().PrincipalId;
        }

        private GridViewModel BuildSectionsGrid()
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
                DataUrl = Url.Action("LoadData", "Section"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("Titles.Sections", String.Empty),
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
