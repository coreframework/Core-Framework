using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.MVC.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Controllers
{
   [Permissions((int)BaseEntityOperations.Manage, typeof(SiteSettings))]
    public partial class SiteSettingsController : FrameworkController
    {
        #region Fields

        private readonly ISiteSettingsService _siteSettingsService;

        #endregion

        #region Constructor

       public SiteSettingsController()
       {
           _siteSettingsService = ServiceLocator.Current.GetInstance<ISiteSettingsService>();
       }

        #endregion

        #region Actions

       public virtual ActionResult Show()
        {
            return View("ViewSettings", _siteSettingsService.GetSettings());
        }

       [HttpPost]
       public virtual ActionResult Edit(SiteSettings model)
       {
           if (ModelState.IsValid)
           {
               if (_siteSettingsService.Save(model))
                   Success(Translate("Messages.SiteSettingsApplied"));
               else Error(Translate("Messages.Error"));
           }
           else
           {
               Error(Translate("Messages.ValidationError"));
           }
           return View("ViewSettings");
       }

        #endregion
    }
}
