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
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Migrator;
using Core.Web.NHibernate.Models;
using Framework.MVC.Controllers;
using Framework.MVC.Grids;
using Framework.MVC.Grids.jqGrid;
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
                                                         new GridColumnViewModel {
                                                                 Name = Translate(".Model.Module.Title"),
                                                                 Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Module.Description"),
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Module.Version"),
                                                                 Index = "Version"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Module.CreateDate"),
                                                                 Index = "CreateDate"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.Module.Status"),
                                                                 Index = "Status"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Actions.Actions"),
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Sortable = false
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.Module.DynamicGridData()),
                DefaultOrderColumn = "Id",
                GridTitle =Translate(".Modules"),
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
            int totalRecords = PluginHelper.CountAvailablePlugins(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var plugins = PluginHelper.GetAvailablePlugins(searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize));
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (plugins.Select(plugin => new
                                                        {
                                                            id = JqGridConstants.NotClickableId,
                                                            cell = new[]
                                                                       {
                                                                            plugin.Title, 
                                                                            plugin.Description,
                                                                            plugin.Version,
                                                                            plugin.CreateDate.ToLongDateString(),
                                                                            plugin.Status.ToString(),
                                                                            plugin.Status.Equals(PluginStatus.NotInstalled) ? String.Format(JqGridConstants.UrlTemplate, Url.Action(MVC.Admin.Module.Install(plugin.Id)), Translate("Actions.Install")) :
                                                                            plugin.Status.Equals(PluginStatus.Installed) ? String.Format(JqGridConstants.UrlTemplate, Url.Action(MVC.Admin.Module.Uninstall(plugin.Id)), Translate("Actions.Uninstall")) : 
                                                                            String.Empty,
                                                                            String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.Admin.Module.Edit(plugin.Id)), Translate("Actions.Edit"))
                                                                       }
                                                        }).ToArray())
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Render edit form for plugin specified.
        /// </summary>
        /// <param name="id">The plugin id.</param>
        /// <returns>Plugin edit view.</returns>
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            var plugin = pluginService.Find(id);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            return View(new PluginViewModel().MapFrom(plugin));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long pluginId, String culture)
        {
            var plugin = pluginService.Find(pluginId);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.PluginNotFound"));
            }
            PluginViewModel model = new PluginViewModel().MapFrom(plugin);
            model.SelectedCulture = culture;
            var localeService = ServiceLocator.Current.GetInstance<IPluginLocaleService>();
            PluginLocale locale = localeService.GetLocale(pluginId, culture);
            if (locale != null)
            {
                model.Title = locale.Title;
                model.Description = locale.Description;
            }

            return PartialView("EditForm", model);
        }

        /// <summary>
        /// Updates plugin details.
        /// </summary>
        /// <param name="id">The plugin id.</param>
        /// <param name="pluginView">The plugin view model.</param>
        /// <returns>Redirect back to plugins list.</returns>
        [HttpPost]
        public virtual ActionResult Update(long id, PluginViewModel pluginView)
        {
            var plugin = pluginService.Find(id);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (ModelState.IsValid)
            {
                var localeService = ServiceLocator.Current.GetInstance<IPluginLocaleService>();
                PluginLocale pluginLocale = localeService.GetLocale(id, pluginView.SelectedCulture) ??
                                            new PluginLocale { Plugin = plugin, Culture = pluginView.SelectedCulture };
                pluginLocale.Title = pluginView.Title;
                pluginLocale.Description = pluginView.Description;
                localeService.Save(pluginLocale);
                Success(Translate("Messages.PluginUpdated"));
                return RedirectToAction(MVC.Admin.Module.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("Edit", pluginView);
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            return View(new PluginViewModel().MapFrom(plugin));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            return View(new PluginViewModel().MapFrom(plugin));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            if (pluginEntity.Status.Equals(PluginStatus.NotInstalled))
            {
                ICorePlugin corePlugin =
                    Application.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
                if (corePlugin == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundPlugin"));
                }
                CoreMigrator.Current.MigrateUp(corePlugin);
                corePlugin.Install();
                pluginEntity.Status = PluginStatus.Installed;
                pluginService.Save(pluginEntity);
                corePlugin.Start();
                Success(Translate("Messages.InstallPlugin"));
                return RedirectToAction(MVC.Admin.Module.Index());
            }
            Error(Translate("Messages.UnknownError"));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            if (!pluginEntity.Status.Equals(PluginStatus.NotInstalled))
            {
                ICorePlugin corePlugin =
                    Application.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
                if (corePlugin == null)
                {
                    throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundPlugin"));
                }

                corePlugin.Uninstall();

                CoreMigrator.Current.MigrateDown(corePlugin);

                pluginEntity.Status = PluginStatus.NotInstalled;
                pluginService.Save(pluginEntity);
                Success(Translate("Messages.UninstallPlugin"));
                return RedirectToAction(MVC.Admin.Module.Index());
            }
            Error(Translate("Messages.UnknownError"));
            return RedirectToAction(MVC.Admin.Module.Index());
        }
    }
}
