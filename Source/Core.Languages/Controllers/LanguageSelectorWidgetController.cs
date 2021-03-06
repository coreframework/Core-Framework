﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Widgets;
using Framework.Core;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LanguageSelectorWidget")]
    public partial class LanguageSelectorWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return LanguageSelectorWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            ILanguageService languageService = ServiceLocator.Current.GetInstance<ILanguageService>();
            IEnumerable<Language> languages = languageService.GetAll();
            return PartialView(languages);
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(String cultureCode)
        {
            HttpContext.Response.Cookies.Add(new HttpCookie(Constants.CultureCookieName, cultureCode));

            return null; 
        }

        #endregion
    }
}
