using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Extensions;
using Core.Profiles.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Mvc.Extensions;
using Framework.Mvc.Grids;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ProfileElement")]
    public partial class ProfileElementController : Controller
    {
        #region Fields

        private readonly IProfileTypeService profileTypesService;
        private readonly IProfileTypeLocaleService profileTypeLocaleService;

        #endregion

        #region Constructor

        public ProfileElementController()
        {
            profileTypesService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
            profileTypeLocaleService = ServiceLocator.Current.GetInstance<IProfileTypeLocaleService>();
        }

        #endregion

        #region ProfileType Elements

        [MvcSiteMapNode(Title = "$t:Titles.ProfileElements", AreaName = "Profiles", ParentKey = "Profiles.ProfileType.Edit", Key = "ProfileTypes.ProfileElements")]
        public virtual ActionResult Show(long profileTypeId)
        {
            var profileType = profileTypesService.Find(profileTypeId);

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
                DataUrl = Url.Action("LoadData", "ProfileElement", new { profileTypeId = profileType.Id }),
                DefaultOrderColumn = "Title",
                GridTitle = HttpContext.Translate("GridTitle", ResourceHelper.GetControllerScope(this)),
                Columns = columns,
                IsRowNotClickable = true
            };

         
            ViewData["ProfileType"] = new ProfileTypeViewModel { Id = profileType.Id };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult LoadData(int profileTypeId, int page, int rows, String search, String sidx, String sord)
        {
            var profileType = profileTypesService.Find(profileTypeId);

            if (profileType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            var profileMembers = ProfileHelper.BindProfileElement(profileTypeId);

            var jsonData = new
            {
                total = profileMembers.Count(),
                page,
                records = profileMembers.Count(),
                rows = (
                    from element in profileMembers
                    select new
                    {
                        id = profileType.Id,
                        cell = new[] {  
                                        String.Format("{0}<input id=\"profileElementId\" type=\"hidden\" value={1} />",
                                        element.Title,
                                        element.Id), 
                                        element.Type.ToString(), 
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 10px;\">{1}</a>",
                                            Url.Action("Edit","ProfileElement", new { }), HttpContext.Translate("Edit", ResourceHelper.GetControllerScope(this))),
                                        String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action("Remove","ProfileElement", new { }))}

                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpPost]
        public virtual ActionResult UpdateProfileElementPosition(long profileElementId, int orderNumber)
        {
            //check permissions inside UpdateProfileElementsPositions widget
          //  ProfileTypesHelper.UpdateProfileElementsPositions(profileElementId, this.CorePrincipal(), orderNumber);
            return null;
        }

      /*  [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "ProfileTypes", ParentKey = "ProfileTypes.ProfileElements")]
        public virtual ActionResult New(long profileTypeId)
        {
            var profileType = profileTypesService.Find(profileTypeId);
            if (profileType == null || !permissionService.IsAllowed((Int32)ProfileTypeOperations.Manage, this.CorePrincipal(), typeof(ProfileType), profileType.Id, IsProfileTypeOwner(profileType), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View(new ProfileElementViewModel { ProfileTypeId = profileTypeId });
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "ProfileTypes", ParentKey = "ProfileTypes.ProfileElements")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long profileTypeId, long profileElementId)
        {
            var profileElement = profileTypesElementService.Find(profileElementId);

            if (profileElement == null || profileElement.ProfileType == null || !permissionService.IsAllowed((Int32)ProfileTypeOperations.Manage, this.CorePrincipal(), typeof(ProfileType), profileElement.ProfileType.Id, IsProfileTypeOwner(profileElement.ProfileType), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View(new ProfileElementViewModel { ProfileTypeId = profileTypeId }.MapFrom(profileElement));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long profileElementId, String culture)
        {
            var profileElement = profileTypesElementService.Find(profileElementId);

            if (profileElement == null || profileElement.ProfileType == null || !permissionService.IsAllowed((Int32)ProfileTypeOperations.Manage, this.CorePrincipal(), typeof(ProfileType), profileElement.ProfileType.Id, IsProfileTypeOwner(profileElement.ProfileType), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            ProfileElementViewModel model = new ProfileElementViewModel { ProfileTypeId = profileElement.ProfileType.Id }.MapFrom(profileElement);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IProfileElementLocaleService>();
            ProfileElementLocale locale = localeService.GetLocale(profileElementId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("ProfileElementEditor", model);
        }

        [HttpPost]
        public virtual ActionResult Save(long profileTypeId, ProfileElementViewModel model)
        {
            ProfileTypesHelper.ValidateProfileElement(model, ModelState);

            if (ModelState.IsValid)
            {
                ProfileTypesHelper.UpdateProfileElement(model);

                var profileType = profileTypesService.Find(profileTypeId);
                if (profileType == null || !permissionService.IsAllowed((Int32)ProfileTypeOperations.Manage, this.CorePrincipal(), typeof(ProfileType), profileType.Id, IsProfileTypeOwner(profileType), PermissionOperationLevel.Object))
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                var profileElement = new ProfileElement();
                bool isEdited = model.Id > 0;
                if (isEdited)
                {
                    profileElement = profileTypesElementService.Find(model.Id);
                }
                else
                {
                    profileElement.ProfileType = profileType;
                    profileElement.OrderNumber = profileTypesElementService.GetLastOrderNumber(profileElement.ProfileType.Id);
                }

                if (profileTypesElementService.Save(model.MapTo(profileElement)))
                {
                    if (isEdited)
                    {
                        //save locale
                        var localeService = ServiceLocator.Current.GetInstance<IProfileElementLocaleService>();
                        ProfileElementLocale locale = localeService.GetLocale(profileElement.Id, model.SelectedCulture);
                        locale = model.MapLocaleTo(locale ?? new ProfileElementLocale { ProfileElement = profileElement });
                        localeService.Save(locale);
                    }
                    Success(HttpContext.Translate("Messages.ProfileElementSaveSuccess", ResourceHelper.GetControllerScope(this))/*"Sucessfully save profileType element."♥1♥);
                    return RedirectToAction(ProfileTypesMVC.ProfileTypes.ShowProfileElements(profileTypeId));
                }
            }

            Error(HttpContext.Translate("Messages.ElementValidationError", ResourceHelper.GetControllerScope(this))/*"Validation errors occurred while processing this profileType. Please take a moment to review the profileType and correct any input errors before continuing."♥1♥);
            return View("EditProfileElement", model);
        }

        public virtual ActionResult Remove(long id)
        {
            var profileElement = profileTypesElementService.Find(id);
            if (profileElement != null && permissionService.IsAllowed((Int32)ProfileTypeOperations.Manage, this.CorePrincipal(), typeof(ProfileType), profileElement.ProfileType.Id, IsProfileTypeOwner(profileElement.ProfileType), PermissionOperationLevel.Object))
            {
                var relatedElements = profileTypesElementService.GetSearchQuery(profileElement.ProfileType.Id, String.Empty).ToList();
                relatedElements.Update(el =>
                {
                    el.OrderNumber =
                       el.OrderNumber > profileElement.OrderNumber
                           ? el.OrderNumber - 1
                           : el.OrderNumber;
                });

                profileTypesElementService.Delete(profileElement);

                foreach (var element in relatedElements)
                {
                    if (element.Id != profileElement.Id)
                        profileTypesElementService.Save(element);
                }
                Success(HttpContext.Translate("Messages.RemoveSuccess", ResourceHelper.GetControllerScope(this))/*"Sucessfully remove profileType element."♥1♥);
                return RedirectToAction("ShowProfileElements", new { profileTypeId = profileElement.ProfileType.Id });
            }

            Error(HttpContext.Translate("Messages.UnknownError", ResourceHelper.GetControllerScope(this))/*"Some error has been occured. Please try again."♥1♥);
            return Content(String.Empty);
        }*/

        #endregion
    }
}
