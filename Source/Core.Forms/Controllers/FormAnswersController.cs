using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.Permissions.Operations;
using Core.Forms.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Framework.MVC.Grids;
using Microsoft.Practices.ServiceLocation;
using Core.Framework.Permissions.Helpers;
using System.Linq.Dynamic;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "FormAnswers")]
    [Permissions((int)FormsPluginOperations.ManageFormsAnswers, typeof(FormsPlugin))]
    public partial class FormAnswersController : CorePluginController
    {
        #region Fields

        private readonly IPermissionCommonService _permissionService;

        private readonly IFormBuilderWidgetService _formBuilderWidgetService;

        private readonly IFormWidgetAnswerService _formWidgetAnswersService;

        #endregion

        #region CoreController Members

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public override string ControllerPluginIdentifier
        {
            get { return FormsPlugin.Instance.Identifier; }
        }

        #endregion

        #region Constructor

        public FormAnswersController()
        {
            _formBuilderWidgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
            _formWidgetAnswersService = ServiceLocator.Current.GetInstance<IFormWidgetAnswerService>();
            _permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
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
        public virtual ActionResult ShowAll()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>();
            columns.Add(new GridColumnViewModel { Name = "Form", Index = "Title" });
            columns.Add(new GridColumnViewModel { Name = "Answers", Width = 50});
            columns.Add(new GridColumnViewModel { Name = "Id", Sortable = false, Hidden = true });
            var model = new GridViewModel
                            {
                                DataUrl = Url.Action("FormAnswersDynamicGridData"),
                                DetailsUrl ="form-answers/",
                                DefaultOrderColumn = "Title",
                                GridTitle = "Forms Answers",
                                Columns = columns
                            };

            return View("FormsAnswersList", model);
        }

        [HttpPost]
        public virtual JsonResult FormAnswersDynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var criteria = _formBuilderWidgetService.GetSearchCriteria(this.CorePrincipal(), (int)FormsBuilderWidgetOperations.ViewAnswers, search, FormsBuilderWidget.Instance.Identifier);
            int totalRecords = _formBuilderWidgetService.GetCount(criteria);
            int totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var widgets = _formBuilderWidgetService.GetPagedCriteria(criteria, pageIndex, pageSize, sidx, sord.Equals("asc")).List<FormBuilderWidget>();

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
        public virtual ActionResult ShowAnswers(long formWidgetId)
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>();
            columns.Add(new GridColumnViewModel { Name = "User", Index = "User" });
            columns.Add(new GridColumnViewModel { Name = "Date", Width = 150, Index = "CreateDate"});
            columns.Add(new GridColumnViewModel { Name = "Title"});
            columns.Add(new GridColumnViewModel { Name = "Id", Sortable = false, Hidden = true });
            GridViewModel model = new GridViewModel
            {
                DataUrl = Url.Action("ShowAnswers"),
                DetailsUrl = System.Web.HttpContext.Current.Request.ApplicationPath+ "/admin/forms-answer-details/",
                DefaultOrderColumn = "Title",
                GridTitle = "Form Answers",
                Columns = columns,
                SearchEnable = false
            };

            return View("FormAnswers", model);
        }

        [HttpPost]
        public virtual JsonResult ShowAnswers(long formWidgetId, int page, int rows, string sidx, string sord)
        {
          int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<FormWidgetAnswer> searchQuery = _formWidgetAnswersService.GetAnswersQuery(formWidgetId, String.Empty);
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

        public virtual ActionResult ShowAnswerDetails(long answerId)
        {
            var model = _formWidgetAnswersService.Find(answerId);

            if (model == null || !_permissionService.IsAllowed((int)FormsBuilderWidgetOperations.ViewAnswers, this.CorePrincipal(), typeof(FormsBuilderWidget), model.FormBuilderWidget.Id, IsFormOwner(model.FormBuilderWidget.Form), PermissionOperationLevel.ObjectType))
            {
                throw new Exception("Answer not found.");
            }

            return View("FormAnswerDetails", model);
        }

        #endregion
    }
}
