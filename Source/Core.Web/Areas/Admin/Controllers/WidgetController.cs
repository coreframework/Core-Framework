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
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(Plugin))]
    public partial class WidgetController : FrameworkController
    {
        #region Fields

        private readonly IWidgetService widgetService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetController"/> class.
        /// </summary>
        public WidgetController()
        {
            widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
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
                                                                 Name = "Widget", Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Plugin", Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Status", Index = "Status"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Actions", Sortable = false
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.Widget.DynamicGridData()),
                DefaultOrderColumn = "Title",
                GridTitle = "Modules",
                Columns = columns
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<Widget> searchQuery = widgetService.GetSearchQuery(search);
            int totalRecords = widgetService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var plugins = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                           plugins.Select(widget => new
                           {
                               id = "not_clickabe",
                               cell = new[]
                                                                       {
                                                                            widget.Title, 
                                                                            widget.Plugin.Title,
                                                                            widget.Status.ToString(),
                                                                            widget.Status.Equals(WidgetStatus.Disabled) ? String.Format("<a href=\"{0}\">{1}</a>",Url.Action(MVC.Admin.Widget.Enable(widget.Id)),HttpContext.Translate("Install", ResourceHelper.GetControllerScope(this))) :
                                                                            widget.Status.Equals(WidgetStatus.Enabled) ? String.Format("<a href=\"{0}\">{1}</a>",Url.Action(MVC.Admin.Widget.Disable(widget.Id)),HttpContext.Translate("Uninstall", ResourceHelper.GetControllerScope(this))) : 
                                                                            String.Empty,
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
        public virtual ActionResult Edit(long id)
        {
            var widget = widgetService.Find(id);
            if (widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(new WidgetViewModel().MapFrom(widget));
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
            var widget = widgetService.Find(id);
            if (widget == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (ModelState.IsValid)
            {
                model.MapTo(widget);
                widgetService.Save(widget);
                Success(Translate("Messages.PluginUpdated"));
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
        //[HttpPost]
        public virtual ActionResult Enable(long id)
        {
            Widget widgetEntity = widgetService.Find(id);
            if (widgetEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            if (widgetEntity.Status.Equals(WidgetStatus.Disabled))
            {
                widgetEntity.Status = WidgetStatus.Enabled;
                widgetService.Save(widgetEntity);
                Success(Translate(".InstallWidget"));
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            Error(Translate(".UnknownError"));
            return View("Index", widgetService.GetInstalledWidgets());
        }

        /// <summary>
        /// Disables widget.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>List of registered widgets.</returns>
        //[HttpPost]
        public virtual ActionResult Disable(long id)
        {
            Widget widgetEntity = widgetService.Find(id);
            if (widgetEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            if (widgetEntity.Status.Equals(WidgetStatus.Enabled))
            {
                widgetEntity.Status = WidgetStatus.Disabled;
                widgetService.Save(widgetEntity);
                Success(Translate(".UninstallWidget"));
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            Error(Translate(".UnknownError"));
            return View("Index", widgetService.GetInstalledWidgets());
        }

    }
}
