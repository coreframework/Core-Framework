using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Languages.Models;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Core.Languages.Permissions.Operations;
using Framework.MVC.Grids;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;

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
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Title", 
                                                                 Index = "Title",
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
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
                DataUrl = Url.Action("DynamicGridData", "Languages"),
                DefaultOrderColumn = "Id",
                GridTitle = "Users",
                Columns = columns,
                IsRowNotClickable = true
            };
            return View("Admin/Index", model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<Language> searchQuery = languageService.GetSearchQuery(search);
            int totalRecords = languageService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var languages = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from language in languages
                    select new
                    {
                        id = language.Id,
                        cell = new[] {  language.Title, 
                                        String.Format("<a href=\"{0}\">{1}</a>",
                                            Url.Action("Edit","Languages",new { id = language.Id }),"Edit"),
                                        String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                            Url.Action("Remove","Languages",new { id = language.Id }))}
                    }).ToArray()
            };
            return Json(jsonData);
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
        /// <param name="language">The language page.</param>
        /// <returns></returns>
        [HttpPut]
        public virtual ActionResult Create(LanguageViewModel language)
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
        [HttpGet]
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

