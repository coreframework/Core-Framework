using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.Permissions.Operations;
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "Section")]
    public partial class SectionController : CorePluginController
    {
        #region Fields

        private readonly ISectionService sectionService;
        private readonly ISectionLocaleService sectionLocaleService;
        private readonly IPermissionCommonService permissionService;

        #endregion

        #region Constructor

        public SectionController()
        {
            sectionService = ServiceLocator.Current.GetInstance<ISectionService>();
            sectionLocaleService = ServiceLocator.Current.GetInstance<ISectionLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
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
                                        section.Description}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "WebContent", ParentKey = "WebContent.Section.Show")]
        public virtual ActionResult New()
        {
            return View(new SectionViewModel { AllowManage = true });
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
                }
            }

            Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            section.AllowManage = true;
            return View("New", section);
        }

        #endregion

        #region Helper Methods

        public GridViewModel BuildSectionsGrid()
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
