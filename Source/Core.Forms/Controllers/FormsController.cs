using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Forms.Helpers;
using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Forms.Permissions.Operations;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Framework.Core.Extensions;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "Forms")]
    [Permissions((int)FormsPluginOperations.ManageForms, typeof(FormsPlugin))]
    public partial class FormsController : CorePluginController
    {
        #region Fields

        private readonly IFormService _formsService;

        private readonly IFormElementService _formsElementService;

        private readonly IPermissionCommonService _permissionService;

        private readonly IPermissionsHelper _permissionsHelper;

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

        public FormsController()
        {
            _formsService = ServiceLocator.Current.GetInstance<IFormService>();
            _formsElementService = ServiceLocator.Current.GetInstance<IFormElementService>();
            _permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            _permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();

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
        /// Shows the list of all available forms.
        /// </summary>
        /// <returns></returns>
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
                                                                 Name = "Actions",
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
                DataUrl = Url.Action("DynamicGridData", "Forms"),
                DefaultOrderColumn = "Title",
                GridTitle = "Forms",
                Columns = columns,
                IsRowNotClickable = true
            };

            return View("FormsList", model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = _formsService.GetSearchQuery(search, this.CorePrincipal(), (Int32)FormOperations.View);
            int totalRecords = _formsService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var forms = searchQuery.AddOrder(new Order("forms." + sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<Form>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from form in forms
                    select new
                    {
                        id = form.Id,
                        cell = new[] {  
                                        form.Title, 
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("Edit","Forms",new { formId = form.Id }),"Details")}
                    }).ToArray()
            };
            return Json(jsonData);
        }


        public virtual ActionResult ShowPermissions(long formId)
        {
            var form = _formsService.Find(formId);

            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            return View("FormPermissions", _permissionsHelper.BindPermissionsModel(form.Id, typeof(Form), false));
        }

        [ChildActionOnly]
        public virtual ActionResult FormTabs(long formId, bool activeDetails, bool activeElements, bool activePermissions)
        {
            var form = _formsService.Find(formId);
            if (form != null)
            {
                var result = new List<MenuItemModel>();
                var formPermissions = _permissionService.GetAccess(OperationsHelper.GetOperations<FormOperations>(), this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form));
                if (formPermissions.ContainsKey((int)FormOperations.View) && formPermissions[(int)FormOperations.View])
                {
                    result.Add(new MenuItemModel
                                   {
                                       Title = "Details",
                                       IsActive = activeDetails,
                                       Url = Url.Action("Edit", "Forms", new { formId = form.Id, Area = "Forms" })
                                   });
                   
                    result.Add(new MenuItemModel
                    {
                        Title = "Form Elements",
                        IsActive = activeElements,
                        Url = Url.Action("ShowFormElements", "Forms", new { formId = form.Id, Area = "Forms" })
                    }); 
                }
                if (formPermissions.ContainsKey((int)FormOperations.Permissions) && formPermissions[(int)FormOperations.Permissions])
                {
                    result.Add(new MenuItemModel
                    {
                        Title = "Permissions",
                        IsActive = activePermissions,
                        Url = Url.Action("ShowPermissions", "Forms", new { formId = form.Id, Area = "Forms" })
                    }); 
                }

                return PartialView("FormTabs", result);
            }

            return Content(String.Empty);
        }

        public virtual ActionResult Remove(long id)
        {
            var form = _formsService.Find(id);
            if (form != null && _permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                _formsService.Delete(form);
            }

            return RedirectToAction("ShowAll");
        }

        /// <summary>
        /// Applies the permissions action.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionsModel model)
        {
            var form = _formsService.Find(model.EntityId);

            if (form != null)
            {
                if (_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    _permissionsHelper.ApplyPermissions(model, typeof(Form));
                }
                if (_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    Success("Successfully apply permissions.");
                    return Content(Url.Action("ShowPermissions", "Forms", new { formId = form.Id }));
                }
                Error(String.Format("Could not apply permissions to Form Entity: {0}", model.EntityId));
            }
            Error(String.Format("Could not found Form Entity: {0}",model.EntityId));
            return Content(Url.Action("ShowAll"));
        }

        /// <summary>
        /// New form action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult New()
        {
            return View("EditForm", new FormViewModel {AllowManage = true});
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Edit(long formId)
        {
            var form = _formsService.Find(formId);

            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            bool allowManage = _permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            return View("EditForm", new FormViewModel { AllowManage = allowManage}.MapFrom(form));
        }

        [HttpPost]
        public virtual ActionResult Save(FormViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isNew = false;
                var form = new Form();
                if (model.Id > 0)
                {
                     form = _formsService.Find(model.Id);

                     if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                     {
                         throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
                     }
                }
                else
                {
                    isNew = true;
                    form.UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?) null;
                }

                if (_formsService.Save(model.MapTo(form)))
                {
                    Success(HttpContext.Translate("Messages.SuccessFormSubmit",
                                                                ResourceHelper.GetControllerScope(this)));
                    if (isNew)
                    {
                        _permissionService.SetupDefaultRolePermissions(OperationsHelper.GetOperations<FormOperations>(), typeof(Form), form.Id);
                        return RedirectToAction(FormsMVC.Forms.Edit(form.Id));
                    }
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError",
                                                                ResourceHelper.GetControllerScope(this)));
            }

            model.AllowManage = true;
            return View("EditForm", model);
        }

        public virtual ActionResult ShowFormElements(long formId)
        {
            var form = _formsService.Find(formId);

            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Title", 
                                                                 Sortable = false,
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Type", 
                                                                 Sortable = false,
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Required", 
                                                                 Sortable = false,
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 50,
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
                DataUrl = Url.Action("FormElementsDynamicGridData", "Forms", new { formId = form.Id}),
                DefaultOrderColumn = "Title",
                GridTitle = "Form Elements",
                Columns = columns,
                IsRowNotClickable = true
            };
            
            bool allowManage = _permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            ViewData["Form"] = new FormViewModel {Id = form.Id, AllowManage = allowManage};

            return View("FormElements", model);
        }

        [HttpPost]
        public virtual JsonResult FormElementsDynamicGridData(int formId, int page, int rows, string search, string sidx, string sord)
        {
            var form = _formsService.Find(formId);

            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            bool allowManage = _permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<FormElement> searchQuery = _formsElementService.GetSearchQuery(form.Id,search);
            int totalRecords = _formsElementService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var forms = searchQuery.OrderBy("OrderNumber asc").Skip(pageIndex * pageSize).Take(pageSize).ToList(); //todo order only on OrderNumber field
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from formElement in forms
                    select new
                    {
                        id = form.Id,
                        cell = new[] {  
                                        String.Format("{0}<input id=\"formElementId\" type=\"hidden\" value={1} />",formElement.Title,formElement.Id), 
                                        formElement.Type.ToString(), 
                                        formElement.IsRequired.ToString(), 
                                        allowManage?String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("EditElement","Forms",new { formElementId = formElement.Id, formId = formElement.Form.Id }),"Edit"):String.Empty,
                                        allowManage?String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("RemoveElement","Forms",new { id = formElement.Id})):String.Empty}

                    }).ToArray()
            };
            return Json(jsonData);
        }


        [HttpPost]
        public virtual ActionResult UpdateFormElementPosition(long formElementId, int orderNumber)
        {
            //check permissions inside UpdateFormElementsPositions widget
            FormsHelper.UpdateFormElementsPositions(formElementId, this.CorePrincipal(), orderNumber);
            return null;
        }

        [HttpGet]
        public virtual ActionResult NewElement(long formId)
        {
            var form = _formsService.Find(formId);
            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            return View("EditFormElement", new FormElementViewModel {FormId = formId}.MapFrom(new FormElement()));
        }

        [HttpGet]
        public virtual ActionResult EditElement(long formId, long formElementId)
        {
            var formElement = _formsElementService.Find(formElementId);

            if (formElement == null || formElement.Form == null || !_permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), formElement.Form.Id, IsFormOwner(formElement.Form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            return View("EditFormElement", new FormElementViewModel { FormId = formId }.MapFrom(formElement));
        }

        [HttpPost]
        public virtual ActionResult SaveElement(long formId, FormElementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var form = _formsService.Find(formId);
                if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
                }

                var formElement = new FormElement();
                if (model.Id > 0)
                {
                    formElement = _formsElementService.Find(model.Id);
                }
                else
                {
                    formElement.Form = form;
                    formElement.OrderNumber = _formsElementService.GetLastOrderNumber(formElement.Form.Id);
                }

                if (_formsElementService.Save(model.MapTo(formElement)))
                {
                    Success("Sucessfully save form element.");
                    return RedirectToAction(FormsMVC.Forms.ShowFormElements());
                }
            }

            Error("Validation errors occurred while processing this form. Please take a moment to review the form and correct any input errors before continuing.");
            return View("EditFormElement", model);
        }

        public virtual ActionResult RemoveElement(long id)
        {
            var formElement = _formsElementService.Find(id);
            if (formElement != null && _permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), formElement.Form.Id, IsFormOwner(formElement.Form), PermissionOperationLevel.Object))
            {
                var relatedElements = _formsElementService.GetSearchQuery(formElement.Form.Id, String.Empty).ToList();
                relatedElements.Update(el =>
                {
                    el.OrderNumber =
                       el.OrderNumber > formElement.OrderNumber
                           ? el.OrderNumber - 1
                           : el.OrderNumber;
                });

                _formsElementService.Delete(formElement);

                foreach (var element in relatedElements)
                {
                    if (element.Id != formElement.Id)
                        _formsElementService.Save(element);
                }
                Success("Sucessfully remove form element.");
                return RedirectToAction("ShowFormElements", new { formId = formElement.Form.Id });
            }

            Error("Some error has been occured. Please try again.");
            return Content(String.Empty);
        }

        #endregion
    }
}
