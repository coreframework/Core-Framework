using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Languages.Models;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages.Controllers
{
    /// <summary>
    /// Handles module requests.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "Languages")]
    [Permissions((int)LanguagesPluginOperations.ManageLanguages, typeof(LanguagesPlugin))]
    public partial class LanguagesController : CorePluginController
    {
        #region Fields
        
        private readonly ILanguageService languageService;

        #endregion

        #region Properties

        public override string ControllerPluginIdentifier
        {
            get { return LanguagesPlugin.Instance.Identifier; }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesController"/> class.
        /// </summary>
        public LanguagesController()
        {
            this.languageService = ServiceLocator.Current.GetInstance<ILanguageService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders languages listing.
        /// </summary>
        /// <returns>List of languages.</returns>
        
        public virtual ActionResult ShowAll()
        {
            return View("Admin/Index", languageService.GetAll());
        }

        /// <summary>
        /// Shows language details by id.
        /// </summary>
        /// <param name="id">The language id.</param>
        /// <returns>Language details</returns>
        public virtual ActionResult ShowById(long? id)
        {
            var language = languageService.Find(id ?? 0);
            if (language == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Show", language);
        }

        /// <summary>
        /// Shows language edit form.
        /// </summary>
        /// <param name="id">The language id.</param>
        /// <returns>Language edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var language = languageService.Find(id ?? 0);
            if (language == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Edit", new LanguageViewModel().MapFrom(language));
        }

        /// <summary>
        /// Updates language.
        /// </summary>
        /// <param name="id">The language id.</param>
        /// <param name="contentPage">The language model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, LanguageViewModel languageModel)
        {
            if (ModelState.IsValid && id!=null)
            {
                Language language = languageService.Find((long)id);
                languageService.Save(languageModel.MapTo(language));
                return RedirectToAction("ShowAll");
            }

            return View("Admin/Edit", languageModel);
        }

        /// <summary>
        /// Shows language create form.
        /// </summary>
        /// <returns>Language create form.</returns>
        public virtual ActionResult New()
        {
            return View("Admin/New", new LanguageViewModel());
        }

        /// <summary>
        /// Saves new language.
        /// </summary>
        /// <param name="contentPage">The language page.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(LanguageViewModel language)
        {
            if (ModelState.IsValid)
            {
                languageService.Save(language.MapTo(new Language()));
                return RedirectToAction("ShowAll");
            }

            return View("Admin/New", language);
        }


        /// <summary>
        /// Removes the specified language.
        /// </summary>
        /// <param name="id">The language id.</param>
        /// <returns>List of language pages</returns>
        [HttpPost]
        public virtual ActionResult Remove(long id)
        {
            var language = languageService.Find(id);
            if (language != null)
            {
                languageService.Delete(language);
            }

            return RedirectToAction("ShowAll");
        }

        #endregion
    }
}

