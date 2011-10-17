using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.Permissions.Operations;
using Core.Forms.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Framework.Core;
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using Core.Framework.Permissions.Helpers;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "FormAnswers")]
    [Permissions((int)FormsPluginOperations.ManageFormsAnswers, typeof(FormsPlugin))]
    public partial class FormAnswersController : CorePluginController
    {
        #region Fields

        private readonly IPermissionCommonService permissionService;

        private readonly IFormBuilderWidgetService formBuilderWidgetService;

        private readonly IFormWidgetAnswerService formWidgetAnswersService;

        #endregion

        #region CoreController Members

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public override String ControllerPluginIdentifier
        {
            get { return FormsPlugin.Instance.Identifier; }
        }

        #endregion

        #region Constructor

        public FormAnswersController()
        {
            formBuilderWidgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
            formWidgetAnswersService = ServiceLocator.Current.GetInstance<IFormWidgetAnswerService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
        }

        #endregion

        #region Helper Methods

        private bool IsFormOwner(Form form)
        {
            return form != null && this.CorePrincipal() != null && form.UserId != null &&
                             form.UserId == this.CorePrincipal().PrincipalId;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders roles listing.
        /// </summary>
        /// <returns>Accounts roles view.</returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "$t:Titles.FormAnswersForms", AreaName = "Forms", ParentKey = "Home", Key = "FormAnswers.ShowAll")]
        public virtual ActionResult ShowAll()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>();
            columns.Add(new GridColumnViewModel { Name = HttpContext.Translate("Form", ResourceHelper.GetControllerScope(this)), Index = "Title" });
            columns.Add(new GridColumnViewModel { Name = HttpContext.Translate("Answers", ResourceHelper.GetControllerScope(this)), Width = 50 });
            columns.Add(new GridColumnViewModel { Name = "Id", Sortable = false, Hidden = true });
            var model = new GridViewModel
                            {
                                DataUrl = Url.Action("FormAnswersDynamicGridData"),
                                DetailsUrl ="form-answers/",
                                DefaultOrderColumn = "Title",
                                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),
                                Columns = columns
                            };

            return View("FormsAnswersList", model);
        }

        [HttpPost]
        public virtual JsonResult FormAnswersDynamicGridData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var criteria = formBuilderWidgetService.GetSearchCriteria(this.CorePrincipal(), (int)FormsBuilderWidgetOperations.ViewAnswers, search, FormsBuilderWidget.Instance.Identifier);
            int totalRecords = formBuilderWidgetService.GetCount(criteria);
            int totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var widgets = formBuilderWidgetService.GetPagedCriteria(criteria, pageIndex, pageSize, sidx, sord.Equals("asc")).List<FormBuilderWidget>();

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from widget in widgets
                    select new
                    {
                        id = widget.Id.ToString(),
                        cell = new[] { 
                            widget.Title,
                            widget.Answers.Count().ToString()
                            }
                    }).ToList()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "$t:Titles.FormAnswers", AreaName = "Forms", ParentKey = "FormAnswers.ShowAll")]
        public virtual ActionResult ShowAnswers(long formWidgetId)
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>();
            columns.Add(new GridColumnViewModel { Name = HttpContext.Translate("User", ResourceHelper.GetControllerScope(this)), Index = "User" });
            columns.Add(new GridColumnViewModel { Name = HttpContext.Translate("Date", ResourceHelper.GetControllerScope(this)), Width = 150, Index = "CreateDate"});
            columns.Add(new GridColumnViewModel { Name = HttpContext.Translate("Title", ResourceHelper.GetControllerScope(this))});
            columns.Add(new GridColumnViewModel { Name = "Id", Sortable = false, Hidden = true });
            GridViewModel model = new GridViewModel
            {
                DataUrl = Url.Action("ShowAnswers"),
                DetailsUrl = ApplicationUtility.Path + "admin/forms-answer-details/",
                DefaultOrderColumn = "Title",
                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                SearchEnable = false
            };

            return View("FormAnswers", model);
        }

        [HttpPost]
        public virtual JsonResult ShowAnswers(long formWidgetId, int page, int rows, String sidx, String sord)
        {
          int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<FormWidgetAnswer> searchQuery = formWidgetAnswersService.GetAnswersQuery(formWidgetId, String.Empty);
            int totalRecords = searchQuery.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var widgetAnswers = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from answer in widgetAnswers
                    select new
                    {
                        id = answer.Id.ToString(),
                        cell = new IConvertible[] { 
                            answer.User!=null?answer.User.Username:null,
                            answer.CreateDate.ToString(),
                            answer.Title
                            }
                    }).ToList()
            };
            return Json(jsonData);
        }

        [MvcSiteMapNode(Title = "Details", AreaName = "Forms", ParentKey = "FormAnswers.ShowAll")]
        [SiteMapTitle("Title")]
        public virtual ActionResult ShowAnswerDetails(long answerId)
        {
            var model = formWidgetAnswersService.Find(answerId);

            if (model == null || !permissionService.IsAllowed((int)FormsBuilderWidgetOperations.ViewAnswers, this.CorePrincipal(), typeof(FormsBuilderWidget), model.FormBuilderWidget.Id, IsFormOwner(model.FormBuilderWidget.Form), PermissionOperationLevel.ObjectType))
            {
                throw new HttpException(HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("FormAnswerDetails", model);
        }

        #endregion
    }
}
