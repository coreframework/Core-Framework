﻿using System;
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
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "Forms")]
    [Permissions((int)FormsPluginOperations.ManageForms, typeof(FormsPlugin))]
    public partial class FormsController : CorePluginController
    {
        #region Fields

        private readonly IFormService formsService;

        private readonly IFormLocaleService formsLocaleService;

        private readonly IFormElementService formsElementService;

        private readonly IFormElementLocaleService formsElementLocaleService;

        private readonly IPermissionCommonService permissionService;

        private readonly IPermissionsHelper permissionsHelper;

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

        public FormsController()
        {
            formsService = ServiceLocator.Current.GetInstance<IFormService>();
            formsLocaleService = ServiceLocator.Current.GetInstance<IFormLocaleService>();
            formsElementService = ServiceLocator.Current.GetInstance<IFormElementService>();
            formsElementLocaleService = ServiceLocator.Current.GetInstance<IFormElementLocaleService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();

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
        [MvcSiteMapNode(Title = "$t:Titles.Forms", AreaName = "Forms", ParentKey = "Home", Key = "Forms.ShowAll")]
        public virtual ActionResult ShowAll()
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
                DataUrl = Url.Action("DynamicGridData", "Forms"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("Forms", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                IsRowNotClickable = true
            };

            return View("FormsList", model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = formsLocaleService.GetSearchCriteria(search, this.CorePrincipal(), (Int32)FormOperations.View);
            long totalRecords = formsLocaleService.Count(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var forms = searchQuery.AddOrder(new Order(sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<FormLocale>();
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
                                            Url.Action("Edit","Forms",new { formId = form.Form.Id }),HttpContext.Translate("Details", ResourceHelper.GetControllerScope(this)))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Shows the permissions view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        [MvcSiteMapNode(Title = "$t:Titles.Permissions", AreaName = "Forms", ParentKey = "Forms.Edit")]
        public virtual ActionResult ShowPermissions(long formId)
        {
            var form = formsService.Find(formId);

            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Notfound", ResourceHelper.GetControllerScope(this))/*"Not Found"*/);
            }

            return View("FormPermissions", permissionsHelper.BindPermissionsModel(form.Id, typeof(Form), false));
        }

        /// <summary>
        /// Forms the tabs.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="activeDetails">if set to <c>true</c> [active details].</param>
        /// <param name="activeElements">if set to <c>true</c> [active elements].</param>
        /// <param name="activePermissions">if set to <c>true</c> [active permissions].</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult FormTabs(long formId, bool activeDetails, bool activeElements, bool activePermissions)
        {
            var form = formsService.Find(formId);
            if (form != null)
            {
                var result = new List<MenuItemModel>();
                var formPermissions = permissionService.GetAccess(OperationsHelper.GetOperations<FormOperations>(), this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form));
                if (formPermissions.ContainsKey((int)FormOperations.View) && formPermissions[(int)FormOperations.View])
                {
                    result.Add(new MenuItemModel
                                   {
                                       Title = HttpContext.Translate("Tabs.Details", "Forms.Controllers"),
                                       IsActive = activeDetails,
                                       Url = Url.Action("Edit", "Forms", new { formId = form.Id, Area = "Forms" })
                                   });
                   
                    result.Add(new MenuItemModel
                    {
                        Title = HttpContext.Translate("Tabs.FormElements", "Forms.Controllers"),
                        IsActive = activeElements,
                        Url = Url.Action("ShowFormElements", "Forms", new { formId = form.Id, Area = "Forms" })
                    }); 
                }
                if (formPermissions.ContainsKey((int)FormOperations.Permissions) && formPermissions[(int)FormOperations.Permissions])
                {
                    result.Add(new MenuItemModel
                    {
                        Title = HttpContext.Translate("Tabs.Permissions", "Forms.Controllers"),
                        IsActive = activePermissions,
                        Url = Url.Action("ShowPermissions", "Forms", new { formId = form.Id, Area = "Forms" })
                    }); 
                }

                return PartialView("FormTabs", result);
            }

            return Content(String.Empty);
        }

        /// <summary>
        /// Removes the form by Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual ActionResult Remove(long id)
        {
            var form = formsService.Find(id);
            if (form != null && permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                formsService.Delete(form);
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
            var form = formsService.Find(model.EntityId);

            if (form != null)
            {
                if (permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    permissionsHelper.ApplyPermissions(model, typeof(Form));
                }
                if (permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    Success(HttpContext.Translate("Messages.PermitionsSuccess", ResourceHelper.GetControllerScope(this))/*"Successfully apply permissions."*/);
                    return Content(Url.Action("ShowPermissions", "Forms", new { formId = form.Id }));
                }
                Error(String.Format(HttpContext.Translate("Messages.PermitionsUnSuccess", ResourceHelper.GetControllerScope(this))/*"Could not apply permissions to Form Entity: {0}"*/, model.EntityId));
            }
            Error(String.Format(HttpContext.Translate("Messages.NotFoundEntity", ResourceHelper.GetControllerScope(this))/*"Could not found Form Entity: {0}"*/, model.EntityId));
            return Content(Url.Action("ShowAll"));
        }

        /// <summary>
        /// New form action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "Forms", ParentKey = "Forms.ShowAll")]
        public virtual ActionResult New()
        {
            return View("New", new FormViewModel {AllowManage = true});
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(FormViewModel form)
        {
            FormsHelper.ValidateForm(form, ModelState);
            if (ModelState.IsValid)
            {
                var newForm = form.MapTo(new Form{UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null});
                if (formsService.Save(newForm))
                {
                    permissionService.SetupDefaultRolePermissions(OperationsHelper.GetOperations<FormOperations>(), typeof(Form), newForm.Id);
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(FormsMVC.Forms.Edit(newForm.Id));
                }
            }

            Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            form.AllowManage = true;
            return View("New", form);
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long formId, String culture)
        {
            var form = formsService.Find(formId);

            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(),
                                                        typeof(Form), form.Id, IsFormOwner(form),
                                                        PermissionOperationLevel.Object);

            FormViewModel model = new FormViewModel {AllowManage = allowManage}.MapFrom(form);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IFormLocaleService>();
            FormLocale locale = localeService.GetLocale(formId, culture);

            if (locale!=null)
                model.MapLocaleFrom(locale);

            return PartialView("EditForm", model);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "Forms", ParentKey = "Forms.ShowAll", Key = "Forms.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long formId)
        {
            var form = formsService.Find(formId);

            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            return View("Edit", new FormViewModel { AllowManage = allowManage}.MapFrom(form));
        }

        [HttpPost]
        public virtual ActionResult Save(FormViewModel model)
        {
            FormsHelper.ValidateForm(model, ModelState);
            if (ModelState.IsValid)
            {
                var form = formsService.Find(model.Id);

                if (form == null || !permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (formsService.Save(model.MapTo(form)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<IFormLocaleService>();
                    FormLocale locale = localeService.GetLocale(form.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new FormLocale{Form = form});

                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.SuccessFormSubmit",
                                                                ResourceHelper.GetControllerScope(this)));
                    return RedirectToAction(FormsMVC.Forms.Edit(model.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError",
                                                                ResourceHelper.GetControllerScope(this)));
            }

            model.AllowManage = true;

            return View("Edit", model);
        }

        #region Form Elements

        [MvcSiteMapNode(Title = "$t:Titles.FormElements", AreaName = "Forms", ParentKey = "Forms.Edit", Key = "Forms.FormElements")]
        public virtual ActionResult ShowFormElements(long formId)
        {
            var form = formsService.Find(formId);

            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
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
                                                                 Name = HttpContext.Translate("Type", ResourceHelper.GetControllerScope(this)), 
                                                                 Sortable = false,
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = HttpContext.Translate("Required", ResourceHelper.GetControllerScope(this)), 
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
                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),//"Form Elements",
                Columns = columns,
                IsRowNotClickable = true
            };
            
            bool allowManage = permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            ViewData["Form"] = new FormViewModel {Id = form.Id, AllowManage = allowManage};

            return View("FormElements", model);
        }

        [HttpPost]
        public virtual JsonResult FormElementsDynamicGridData(int formId, int page, int rows, String search, String sidx, String sord)
        {
            var form = formsService.Find(formId);

            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.View, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            bool allowManage = permissionService.IsAllowed((Int32) FormOperations.Manage, this.CorePrincipal(),
                                                            typeof (Form), form.Id, IsFormOwner(form),
                                                            PermissionOperationLevel.Object);

            ICriteria searchQuery = formsElementLocaleService.GetSearchCriteria(form.Id, search);
            long totalRecords = formsLocaleService.Count(searchQuery);
            int totalPages = 1;
            var forms = searchQuery.AddOrder(new Order("formElement.OrderNumber", sord == "asc")).List<FormElementLocale>();

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from formElementLocale in forms
                    select new
                    {
                        id = form.Id,
                        cell = new[] {  
                                        String.Format("{0}<input id=\"formElementId\" type=\"hidden\" value={1} />",formElementLocale.Title,formElementLocale.FormElement.Id), 
                                        formElementLocale.FormElement.Type.ToString(), 
                                        formElementLocale.FormElement.IsRequired.ToString(), 
                                        allowManage?String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("EditElement","Forms",new { formElementId = formElementLocale.FormElement.Id, formId = formElementLocale.FormElement.Form.Id }), HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))):String.Empty,
                                        allowManage?String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("RemoveElement","Forms",new { id = formElementLocale.FormElement.Id})):String.Empty}

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
        [MvcSiteMapNode(Title = "New", AreaName = "Forms", ParentKey = "Forms.FormElements")]
        public virtual ActionResult NewElement(long formId)
        {
            var form = formsService.Find(formId);
            if (form == null || !permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("EditFormElement", new FormElementViewModel {FormId = formId});
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "Forms", ParentKey = "Forms.FormElements")]
        [SiteMapTitle("Title")]
        public virtual ActionResult EditElement(long formId, long formElementId)
        {
            var formElement = formsElementService.Find(formElementId);

            if (formElement == null || formElement.Form == null || !permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), formElement.Form.Id, IsFormOwner(formElement.Form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("EditFormElement", new FormElementViewModel { FormId = formId }.MapFrom(formElement));
        }

        [HttpPost]
        public virtual ActionResult ChangeFormElementLanguage(long formElementId, String culture)
        {
            var formElement = formsElementService.Find(formElementId);

            if (formElement == null || formElement.Form == null || !permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), formElement.Form.Id, IsFormOwner(formElement.Form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            FormElementViewModel model = new FormElementViewModel{FormId = formElement.Form.Id}.MapFrom(formElement);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IFormElementLocaleService>();
            FormElementLocale locale = localeService.GetLocale(formElementId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("FormElementEditor", model);
        }

        [HttpPost]
        public virtual ActionResult SaveElement(long formId, FormElementViewModel model)
        {
            FormsHelper.ValidateFormElement(model, ModelState);

            if (ModelState.IsValid)
            {
                FormsHelper.UpdateFormElement(model);

                var form = formsService.Find(formId);
                if (form == null || !permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                var formElement = new FormElement();
                bool isEdited = model.Id > 0;
                if (isEdited)
                {
                    formElement = formsElementService.Find(model.Id);
                }
                else
                {
                    formElement.Form = form;
                    formElement.OrderNumber = formsElementService.GetLastOrderNumber(formElement.Form.Id);
                }

                if (formsElementService.Save(model.MapTo(formElement)))
                {
                    if (isEdited)
                    {
                        //save locale
                        var localeService = ServiceLocator.Current.GetInstance<IFormElementLocaleService>();
                        FormElementLocale locale = localeService.GetLocale(formElement.Id, model.SelectedCulture);
                        locale = model.MapLocaleTo(locale ?? new FormElementLocale { FormElement = formElement });
                        localeService.Save(locale);
                    }
                    Success(HttpContext.Translate("Messages.FormElementSaveSuccess", ResourceHelper.GetControllerScope(this))/*"Sucessfully save form element."*/);
                    return RedirectToAction(FormsMVC.Forms.ShowFormElements(formId));
                }
            }

            Error(HttpContext.Translate("Messages.ElementValidationError", ResourceHelper.GetControllerScope(this))/*"Validation errors occurred while processing this form. Please take a moment to review the form and correct any input errors before continuing."*/);
            return View("EditFormElement", model);
        }

        public virtual ActionResult RemoveElement(long id)
        {
            var formElement = formsElementService.Find(id);
            if (formElement != null && permissionService.IsAllowed((Int32)FormOperations.Manage, this.CorePrincipal(), typeof(Form), formElement.Form.Id, IsFormOwner(formElement.Form), PermissionOperationLevel.Object))
            {
                var relatedElements = formsElementService.GetSearchQuery(formElement.Form.Id, String.Empty).ToList();
                relatedElements.Update(el =>
                {
                    el.OrderNumber =
                       el.OrderNumber > formElement.OrderNumber
                           ? el.OrderNumber - 1
                           : el.OrderNumber;
                });

                formsElementService.Delete(formElement);

                foreach (var element in relatedElements)
                {
                    if (element.Id != formElement.Id)
                        formsElementService.Save(element);
                }
                Success(HttpContext.Translate("Messages.RemoveSuccess", ResourceHelper.GetControllerScope(this))/*"Sucessfully remove form element."*/);
                return RedirectToAction("ShowFormElements", new { formId = formElement.Form.Id });
            }

            Error(HttpContext.Translate("Messages.UnknownError", ResourceHelper.GetControllerScope(this))/*"Some error has been occured. Please try again."*/);
            return Content(String.Empty);
        }

        #endregion

        #endregion
    }
}
