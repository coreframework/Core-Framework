using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Widgets;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LanguageSelectorWidget")]
    public partial class LanguageSelectorWidgetController : CoreWidgetController
    {
        #region Properties

        public override string ControllerWidgetIdentifier
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

        #endregion
    }
}
