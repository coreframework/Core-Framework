using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.MVC.Controllers;
using Framework.MVC.Grids;
using Framework.MVC.Grids.jqGrid;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using System.Linq.Dynamic;
using MvcSiteMapProvider.Filters;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(Plugin))]
    public partial class WidgetController : FrameworkController
    {
        #region Fields

        private readonly IWidgetService _widgetService;

        private readonly IWidgetLocaleService _widgetLocaleService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetController"/> class.
        /// </summary>
        public WidgetController()
        {
            _widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            _widgetLocaleService = ServiceLocator.Current.GetInstance<IWidgetLocaleService>();
        }

        #endregion

        /// <summary>
        /// Renders widgets listing.
        /// </summary>
        /// <returns>List of registered widgets</returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Widget.Title"), Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Widget.Module"), Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Widget.Status"), Index = "widget.Status"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Actions.Actions"), Sortable = false
                                                             }
                                                             ,
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Actions.Edit"), Sortable = false
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.Widget.DynamicGridData()),
                DefaultOrderColumn = "Id",
                GridTitle = Translate(".Widgets"),
                Columns = columns
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            ICriteria searchCriteria = _widgetLocaleService.GetSearchCriteria(search);

            long totalRecords = _widgetLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var widgets = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<WidgetLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                           widgets.Select(widget => new
                           {
                               id = JqGridConstants.NotClickableId,
                               cell = new[]
                                            {
                                                widget.Title, 
                                                widget.Widget.Plugin.Title,
                                                widget.Widget.Status.ToString(),
                                                widget.Widget.Status.Equals(WidgetStatus.Disabled) ? String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.Admin.Widget.Enable(widget.Widget.Id)), Translate("Actions.Install")) :
                                                widget.Widget.Status.Equals(WidgetStatus.Enabled) ? String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.Admin.Widget.Disable(widget.Widget.Id)), Translate("Actions.Uninstall")) : 
                                                String.Empty,
                                                String.Format(JqGridConstants.UrlTemplate, Url.Action(MVC.Admin.Widget.Edit(widget.Widget.Id)),Translate("Actions.Edit"))
                                            }
                           }).ToArray())
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Render edit form for widget specified.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>Widget edit view.</returns>
        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long id)
        {
            var widget = _widgetService.Find(id);
            if (widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            return View(new WidgetViewModel().MapFrom(widget));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long widgetId, String culture)
        {
            var widget = _widgetService.Find(widgetId);
            if (widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.WidgetNotFound"));
            }
            WidgetViewModel model = new WidgetViewModel().MapFrom(widget);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<IWidgetLocaleService>();
            WidgetLocale locale = localeService.GetLocale(widgetId, culture);
            if (locale != null)
            {
                model.Title = locale.Title;
            }

            return PartialView("EditForm", model);
        }

        /// <summary>
        /// Updates widget details.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <param name="model">The widget view model.</param>
        /// <returns>Redirect back to widgets list.</returns>
        [HttpPost]
        public virtual ActionResult Update(long id, WidgetViewModel model)
        {
            var widget = _widgetService.Find(id);
            if (widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (ModelState.IsValid)
            {
                var localeService = ServiceLocator.Current.GetInstance<IWidgetLocaleService>();
                WidgetLocale widgetLocale = localeService.GetLocale(id, model.SelectedCulture) ??
                                            new WidgetLocale { Widget = widget, Culture = model.SelectedCulture };
                widgetLocale.Title = model.Title;
                localeService.Save(widgetLocale);
                Success(Translate("Messages.WidgetUpdated"));
                return RedirectToAction(MVC.Admin.Widget.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("Edit", model);
        }

        /// <summary>
        /// Enables widget.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>List of registered widgets.</returns>
        [HttpGet]
        public virtual ActionResult Enable(long id)
        {
            Widget widgetEntity = _widgetService.Find(id);
            if (widgetEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            if (widgetEntity.Status.Equals(WidgetStatus.Disabled))
            {
                widgetEntity.Status = WidgetStatus.Enabled;
                _widgetService.Save(widgetEntity);
                Success(Translate("Messages.InstallWidget"));
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            Error(Translate("Messages.UnknownError"));
            return View("Index", _widgetService.GetInstalledWidgets());
        }

        /// <summary>
        /// Disables widget.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>List of registered widgets.</returns>
        [HttpGet]
        public virtual ActionResult Disable(long id)
        {
            Widget widgetEntity = _widgetService.Find(id);
            if (widgetEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            if (widgetEntity.Status.Equals(WidgetStatus.Enabled))
            {
                widgetEntity.Status = WidgetStatus.Disabled;
                _widgetService.Save(widgetEntity);
                Success(Translate("Messages.UninstallWidget"));
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            Error(Translate("Messages.UnknownError"));
            return View("Index", _widgetService.GetInstalledWidgets());
        }

    }
}
