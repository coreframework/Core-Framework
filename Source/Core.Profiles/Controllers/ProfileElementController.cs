using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Profiles.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Core.Profiles.Permissions.Operations;
using Framework.Core.Extensions;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;
using Core.Framework.Permissions.Helpers;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ProfileElement")]
    [Permissions((int)ProfilesPluginOperations.ManageProfileTypes, typeof(ProfilesPlugin))]
    public partial class ProfileElementController : CorePluginController
    {
        #region Fields

        private readonly IProfileTypeService profileTypesService;
        private readonly IProfileElementService profileElementService;
        private readonly IProfileHeaderService profileHeaderService;

        #endregion

        #region Constructor

        public ProfileElementController()
        {
            profileElementService = ServiceLocator.Current.GetInstance<IProfileElementService>();
            profileTypesService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
            profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
        }

        #endregion

        #region ProfileType Elements

        [MvcSiteMapNode(Title = "$t:Titles.ProfileElements", AreaName = "Profiles", ParentKey = "Profiles.ProfileType.Edit", Key = "ProfileTypes.ProfileElements")]
        public virtual ActionResult Show(long profileTypeId)
        {
            var profileType = profileTypesService.Find(profileTypeId);

            if (profileType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            var profileMembers = ProfileHelper.BindProfileElement(profileTypeId);
         
            ViewData["ProfileType"] = new ProfileTypeViewModel { Id = profileType.Id };

            return View(profileMembers);
        }

        [HttpPost]
        public virtual ActionResult UpdateProfileElementPosition(long? profileElementId, long? profileHeaderId, int orderNumber)
        {
            if (profileHeaderId != null || profileElementId != null)
            {
                ProfileHelper.UpdateProfileElementsPositions(profileElementId, profileHeaderId, orderNumber);
            }
            return null;
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "Profiles", ParentKey = "ProfileTypes.ProfileElements")]
        public virtual ActionResult New(long profileTypeId)
        {
            var profileType = profileTypesService.Find(profileTypeId);
            if (profileType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            return View(new ProfileElementViewModel { ProfileTypeId = profileTypeId });
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(long profileTypeId, ProfileElementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profileType = profileTypesService.Find(profileTypeId);
                if (profileType == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
                }

                var profileElement = model.MapTo(new ProfileElement());
                profileElement.OrderNumber = profileElementService.GetLastOrderNumber(profileElement.ProfileHeader.Id);

                if (profileElementService.Save(profileElement))
                {
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileElement.Show(profileTypeId));
                }
            }

            Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            return View("New", model);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "Profiles", ParentKey = "ProfileTypes.ProfileElements")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long profileTypeId, long profileElementId)
        {
            var profileElement = profileElementService.Find(profileElementId);

            if (profileElement == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            return View(new ProfileElementViewModel { ProfileTypeId = profileTypeId }.MapFrom(profileElement));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long profileElementId, String culture)
        {
            var profileElement = profileElementService.Find(profileElementId);

            if (profileElement == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            ProfileElementViewModel model = new ProfileElementViewModel { ProfileTypeId = profileElement.ProfileHeader.Id }.MapFrom(profileElement);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IProfileElementLocaleService>();
            ProfileElementLocale locale = localeService.GetLocale(profileElementId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("ProfileElementDetails", model);
        }

        [HttpPost]
        public virtual ActionResult Save(long profileTypeId, ProfileElementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profileType = profileTypesService.Find(profileTypeId);
                if (profileType == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
                }

                var profileElement = profileElementService.Find(model.Id);

                if (profileElementService.Save(model.MapTo(profileElement)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<IProfileElementLocaleService>();
                    ProfileElementLocale locale = localeService.GetLocale(profileElement.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new ProfileElementLocale { ProfileElement = profileElement });
                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileElement.Show(profileTypeId));
                }
            }

            Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            return View("Edit", model);
        }

        public virtual ActionResult Remove(long profileElementId, long profileTypeId)
        {
            var profileElement = profileElementService.Find(profileElementId);
            if (profileElement != null)
            {
                profileElement.ProfileHeader.ProfileElements.Update(el =>
                {
                    if (el.Id != profileElement.Id)
                    {
                         el.OrderNumber =
                       el.OrderNumber > profileElement.OrderNumber
                           ? el.OrderNumber - 1
                           : el.OrderNumber;
                    }
                });

                profileElement.ProfileHeader.ProfileElements.Remove(profileElement);
                profileHeaderService.Save(profileElement.ProfileHeader);

                Success(HttpContext.Translate("Messages.RemoveSuccess", String.Empty));
                return RedirectToAction("Show", new { profileTypeId = profileElement.ProfileHeader.ProfileType.Id });
            }

            Error(HttpContext.Translate("Messages.UnknownError", String.Empty));
            return Content(String.Empty);
        }

        #endregion

        public override string ControllerPluginIdentifier
        {
            get { return ProfilesPlugin.Instance.Identifier; }
        }
    }
}
