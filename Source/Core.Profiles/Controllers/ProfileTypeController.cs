using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Core.Profiles.Permissions.Operations;
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
    [Export(typeof(IController)), ExportMetadata("Name", "ProfileType")]
    [Permissions((int)ProfilesPluginOperations.ManageProfileTypes, typeof(ProfilesPlugin))]
    public partial class ProfileTypeController : CorePluginController
    {
        #region Fields

        private readonly IProfileTypeService profileTypeService;
        private readonly IProfileTypeLocaleService profileTypeLocaleService;

        #endregion

        #region Constructor

        public ProfileTypeController()
        {
            profileTypeService = ServiceLocator.Current.GetInstance<IProfileTypeService>();
            profileTypeLocaleService = ServiceLocator.Current.GetInstance<IProfileTypeLocaleService>();
        }

        #endregion

        #region Actions

        [MvcSiteMapNode(Title = "$t:Titles.ProfileTypes", AreaName = "Profiles", ParentKey = "Home", Key = "Profiles.ProfileType.Show")]
        public virtual ActionResult Show()
        {
            return View(BuildProfileTypesGrid());
        }

        [HttpPost]
        public virtual JsonResult LoadData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchQuery = profileTypeLocaleService.GetSearchCriteria(search);
            long totalRecords = profileTypeLocaleService.Count(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var profileTypes = searchQuery.AddOrder(new Order(sidx, sord == "asc")).SetFirstResult(pageIndex * rows).SetMaxResults(rows).List<ProfileTypeLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from profileType in profileTypes
                    select new
                    {
                        id = profileType.Id,
                        cell = new[] {  
                                        profileType.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit", "ProfileType", new { profileTypeId = profileType.ProfileType.Id }),HttpContext.Translate("Details", ResourceHelper.GetControllerScope(this)))
                        }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        [MvcSiteMapNode(Title = "New", AreaName = "Profiles", ParentKey = "Profiles.ProfileType.Show")]
        public virtual ActionResult New()
        {
            return View(new ProfileTypeViewModel ());
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(ProfileTypeViewModel profileType)
        {
            if (ModelState.IsValid)
            {
                var newProfileType = profileType.MapTo(new ProfileType { UserId = this.CorePrincipal() != null ? this.CorePrincipal().PrincipalId : (long?)null });
                if (profileTypeService.Save(newProfileType))
                {
                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileType.Edit(newProfileType.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));

            }

            return View("New", profileType);
        }

        /// <summary>
        /// Edit form action.
        /// </summary>
        /// <param name="profileTypeId">The profileType id.</param>
        /// <returns></returns>
        [HttpGet]
        [MvcSiteMapNode(Title = "Edit", AreaName = "Profiles", ParentKey = "Profiles.ProfileType.Show", Key = "Profiles.ProfileType.Edit")]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long profileTypeId)
        {
            var profileType = profileTypeService.Find(profileTypeId);

            if (profileType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }

            return View("Edit", new ProfileTypeViewModel().MapFrom(profileType));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long profileTypeId, String culture)
        {
            var profileType = profileTypeService.Find(profileTypeId);

            if (profileType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
            }


            ProfileTypeViewModel model = new ProfileTypeViewModel().MapFrom(profileType);
            model.SelectedCulture = culture;

            //get locale
            var localeService = ServiceLocator.Current.GetInstance<IProfileTypeLocaleService>();
            ProfileTypeLocale locale = localeService.GetLocale(profileTypeId, culture);

            if (locale != null)
                model.MapLocaleFrom(locale);

            return PartialView("ProfileTypeDetails", model);
        }

        [HttpPost]
        public virtual ActionResult Save(ProfileTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profileType = profileTypeService.Find(model.Id);

                if (profileType == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.NotFound", ResourceHelper.GetControllerScope(this)));
                }

                if (profileTypeService.Save(model.MapTo(profileType)))
                {
                    //save locale
                    var localeService = ServiceLocator.Current.GetInstance<IProfileTypeLocaleService>();
                    ProfileTypeLocale locale = localeService.GetLocale(profileType.Id, model.SelectedCulture);
                    locale = model.MapLocaleTo(locale ?? new ProfileTypeLocale { ProfileType = profileType });

                    localeService.Save(locale);

                    Success(HttpContext.Translate("Messages.Success", String.Empty));
                    return RedirectToAction(ProfilesMVC.ProfileType.Edit(model.Id));
                }
            }
            else
            {
                Error(HttpContext.Translate("Messages.ValidationError", String.Empty));
            }

            return View("Edit", model);
        }

        public virtual ActionResult Remove(long profileTypeId)
        {
            var profileType = profileTypeService.Find(profileTypeId);
            if (profileType != null)
            {
                profileTypeService.Delete(profileType);
            }

            return RedirectToAction("Show");
        }

        #endregion

        #region Helper Methods

        private GridViewModel BuildProfileTypesGrid()
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
                DataUrl = Url.Action("LoadData", "ProfileType"),
                DefaultOrderColumn = "Id",
                GridTitle = HttpContext.Translate("Titles.ProfileTypes", String.Empty),
                Columns = columns,
                IsRowNotClickable = true
            };

            return model;
        }

        #endregion

        public override string ControllerPluginIdentifier
        {
            get { return ProfilesPlugin.Instance.Identifier; }
        }
    }
}
