using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Migrator;
using Core.Web.NHibernate.Models;
using Framework.MVC.Controllers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles module administration operation requests.
    /// </summary>
    [Permissions((int)BaseEntityOperations.Manage, typeof(Plugin))]
    public partial class ModuleController : FrameworkController
    {
        #region Fields

        private readonly IPluginService pluginService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleController"/> class.
        /// </summary>
        public ModuleController()
        {
            pluginService = ServiceLocator.Current.GetInstance<IPluginService>();
        }

        #endregion

        /// <summary>
        /// Renders modules listing.
        /// </summary>
        /// <returns>List of registered modules</returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Title", Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Description", Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Version", Index = "Version"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Added", Index = "CreateDate"
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
                DataUrl = Url.Action(MVC.Admin.Module.DynamicGridData()),
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
            IQueryable<Plugin> searchQuery = pluginService.GetSearchQuery(search);
            int totalRecords = PluginHelper.CountAvailablePlugins(searchQuery);//pluginService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var plugins = PluginHelper.GetAvailablePlugins(searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize));
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                           plugins.Select(plugin => new
                                                        {
                                                            id = "not_clickabe",
                                                            cell = new[]
                                                                       {
                                                                            plugin.Title, 
                                                                            plugin.Description,
                                                                            plugin.Version,
                                                                            plugin.CreateDate.ToLongDateString(),
                                                                            plugin.Status.ToString(),
                                                                            plugin.Status.Equals(PluginStatus.NotInstalled) ? String.Format("<a href=\"{0}\">{1}</a>",Url.Action(MVC.Admin.Module.Install(plugin.Id)),HttpContext.Translate("Install", ResourceHelper.GetControllerScope(this))) :
                                                                            plugin.Status.Equals(PluginStatus.Installed) ? String.Format("<a href=\"{0}\">{1}</a>",Url.Action(MVC.Admin.Module.Uninstall(plugin.Id)),HttpContext.Translate("Uninstall", ResourceHelper.GetControllerScope(this))) : 
                                                                            String.Empty,
                                                                       }
                                                        }).ToArray())
            };
            return Json(jsonData);
        }


        /// <summary>
        /// Renders module install confirmation view.
        /// </summary>
        /// <param name="id">The module Id</param>
        /// <returns>Module install confirmation view.</returns>
        [HttpGet]
        public virtual ActionResult Install(long id)
        {
            Plugin plugin = pluginService.Find(id);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            return RedirectToAction(MVC.Admin.Module.Index());
        }

        /// <summary>
        /// Uninstalls the specified id.
        /// </summary>
        /// <param name="id">The module Id.</param>
        /// <returns>Module uninstall confirmation view.</returns>
        [HttpGet]
        public virtual ActionResult Uninstall(long id)
        {
            Plugin plugin = pluginService.Find(id);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            return RedirectToAction(MVC.Admin.Module.Index());
        }

        /// <summary>
        /// Installs module.
        /// </summary>
        /// <param name="id">The module id.</param>
        /// <returns>List of registered modules.</returns>
        [HttpPost]
        public virtual ActionResult ConfirmInstall(long id)
        {
            Plugin pluginEntity = pluginService.Find(id);
            if (pluginEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            if (pluginEntity.Status.Equals(PluginStatus.NotInstalled))
            {
                ICorePlugin corePlugin =
                    Application.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
                if (corePlugin == null)
                {
                    throw new HttpException((int) HttpStatusCode.NotFound,
                                            HttpContext.Translate("Messages.CouldNotFoundPlugin",
                                                                  ResourceHelper.GetControllerScope(this)));
                }
                CoreMigrator.Current.MigrateUp(corePlugin);
                corePlugin.Install();
                pluginEntity.Status = PluginStatus.Installed;
                pluginService.Save(pluginEntity);
            }
            return RedirectToAction(MVC.Admin.Module.Index());
        }

        /// <summary>
        /// Uninstall module.
        /// </summary>
        /// <param name="id">The module id.</param>
        /// <returns>List of registered modules.</returns>
        [HttpPost]
        public virtual ActionResult ConfirmUninstall(long id)
        {
            Plugin pluginEntity = pluginService.Find(id);
            if (pluginEntity == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            if (!pluginEntity.Status.Equals(PluginStatus.NotInstalled))
            {
                ICorePlugin corePlugin =
                    Application.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
                if (corePlugin == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound,
                                            HttpContext.Translate("Messages.CouldNotFoundPlugin",
                                                                  ResourceHelper.GetControllerScope(this)));
                }

                corePlugin.Uninstall();

                CoreMigrator.Current.MigrateDown(corePlugin);

                pluginEntity.Status = PluginStatus.NotInstalled;
                pluginService.Save(pluginEntity);
            }

            return RedirectToAction(MVC.Admin.Module.Index());
        }
    }
}
