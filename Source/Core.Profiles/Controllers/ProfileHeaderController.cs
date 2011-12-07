using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Core.Profiles.Permissions.Operations;
using Framework.Core.Extensions;
using Framework.Mvc.Extensions;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Filters;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ProfileHeader")]
    [Permissions((int)ProfilesPluginOperations.ManageProfileTypes, typeof(ProfilesPlugin))]
    public partial class ProfileHeaderController : CorePluginController
    {
        #region Fields

        private readonly IProfileHeaderService profileHeaderService;
        private readonly IProfileHeaderLocaleService profileHeaderLocaleService;
        private readonly IProfileTypeService profileTypeService;

        #endregion

        #region Constructor

        public ProfileHeaderController()
        {
            profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
            profileHeaderLocaleService = ServiceLocator.Current.GetInstance<IProfileHeaderLocaleService>();
            profileTypeService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
        }

        #endregion

        #region Actions

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "Profiles", ParentKey = "ProfileTypes.ProfileElements")]
        public virtual ActionResult New(long profileTypeId)
        {
            return View(new ProfileHeaderViewModel ());
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(long profileTypeId, ProfileHeaderViewModel profileHeader)
        {
            if (ModelState.IsValid)
            {
                var savedItem = profileHeader.MapTo(new ProfileHeader
                                                        {
                                                            ProfileType = new ProfileType { Id = profileTypeId }
                                                        });
                savedItem.OrderNumber = profileHeaderService.GetLastOrderNumber(profileTypeId);
                if (profileHeaderService.Save(savedItem))
                {
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileElement.Show(profileTypeId));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            }

            return View("New", profileHeader);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="profileTypeId">The profile type id.</param>
        /// <param name="profileHeaderId">The profileHeader id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "Profiles", ParentKey = "ProfileTypes.ProfileElements", Key = "Profiles.ProfileHeader.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long profileTypeId, long profileHeaderId)
        {
            var profileHeader = profileHeaderService.Find(profileHeaderId);

            if (profileHeader == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

            return View("Edit", new ProfileHeaderViewModel().MapFrom(profileHeader));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long profileHeaderId, String culture)
        {
            var profileHeader = profileHeaderService.Find(profileHeaderId);

            if (profileHeader == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
            }

        
            ProfileHeaderViewModel model = new ProfileHeaderViewModel ().MapFrom(profileHeader);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IProfileHeaderLocaleService>();
            ProfileHeaderLocale locale = localeService.GetLocale(profileHeaderId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("HeaderDetails", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Save(long profileTypeId, ProfileHeaderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profileHeader = profileHeaderService.Find(model.Id);

                if (profileHeader == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", String.Empty));
                }

                if (profileHeaderService.Save(model.MapTo(profileHeader)))
                {
                    //save locale
                    ProfileHeaderLocale locale = profileHeaderLocaleService.GetLocale(profileHeader.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new ProfileHeaderLocale { ProfileHeader = profileHeader });

                    profileHeaderLocaleService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileElement.Show(profileTypeId));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            }

            return View("Edit", model);
        }

        public virtual ActionResult Remove(long profileTypeId, long profileHeaderId)
        {
            var profileHeader = profileHeaderService.Find(profileHeaderId);
            if (profileHeader != null)
            {
                profileHeader.ProfileType.ProfileHeaders.Update(el =>
                {
                    if (el.Id != profileHeader.Id)
                    {
                        el.OrderNumber =
                      el.OrderNumber > profileHeader.OrderNumber
                          ? el.OrderNumber - 1
                          : el.OrderNumber;
                    }
                });

                profileHeader.ProfileType.ProfileHeaders.Remove(profileHeader);
                profileTypeService.Save(profileHeader.ProfileType);

                Success(HttpContext.Translate("Messages.RemoveSuccess", String.Empty));
                return RedirectToAction("Show", "ProfileElement", new { profileTypeId = profileHeader.ProfileType.Id });
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
