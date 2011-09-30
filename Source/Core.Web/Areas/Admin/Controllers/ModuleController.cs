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
using Framework.Mvc.Breadcrumbs;
using Framework.Mvc.Controllers;
using Framework.Mvc.Grids;
using Framework.Mvc.Grids.JqGrid;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using MvcSiteMapProvider.Filters;

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
        private readonly IPluginLocaleService pluginLocaleService;
        private readonly IBreadcrumbsBuilder breadcrumbsBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleController"/> class.
        /// </summary>
        public ModuleController()
        {
            pluginService = ServiceLocator.Current.GetInstance<IPluginService>();
            pluginLocaleService = ServiceLocator.Current.GetInstance<IPluginLocaleService>();
            breadcrumbsBuilder = ServiceLocator.Current.GetInstance<IBreadcrumbsBuilder>();
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
                                                                 Name = Translate("Models.PluginLocale.Title"),
                                                                 Index = "Title"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.PluginLocale.Description"),
                                                                 Index = "Description"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.Plugin.CreateDate"),
                                                                 Index = "plugin.CreateDate"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate("Models.Plugin.Status"),
                                                                 Index = "plugin.Status"
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
                DefaultOrderColumn = "Title",
                GridTitle =Translate(".Modules"),
                Columns = columns
            };

            //build breadcrumbs
            breadcrumbsBuilder.BuildBreadcrumbs(this, new[]
                                                           {
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Titles.Home"),
                                                                       Url = Url.Action(MVC.Admin.AdminHome.Index())
                                                                   },
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Titles.Modules"),
                                                                   }
                                                           });


            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            ICriteria searchCriteria = pluginLocaleService.GetSearchCriteria(search);
            int totalRecords = PluginHelper.CountAvailablePlugins(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);

            var plugins = PluginHelper.GetAvailablePlugins(searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)));
            
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = plugins.Select(pluginLocale => new
                                                        {
                                                            id = JqGridConstants.NotClickableId,
                                                            cell = new[]
                                                                       {
                                                                            pluginLocale.Title, 
                                                                            pluginLocale.Description,
                                                                            pluginLocale.Plugin.CreateDate.ToLongDateString(),
                                                                            pluginLocale.Plugin.Status.ToString(),
                                                                            pluginLocale.Plugin.Status.Equals(PluginStatus.NotInstalled) ? String.Format(JqGridConstants.UrlTemplate, Url.Action(MVC.Admin.Module.Install(pluginLocale.Plugin.Id)), Translate("Actions.Install")) :
                                                                            pluginLocale.Plugin.Status.Equals(PluginStatus.Installed) ? String.Format(JqGridConstants.UrlTemplate, Url.Action(MVC.Admin.Module.Uninstall(pluginLocale.Plugin.Id)), Translate("Actions.Uninstall")) : 
                                                                            String.Empty,
                                                                            String.Format(JqGridConstants.UrlTemplate,Url.Action(MVC.Admin.Module.Edit(pluginLocale.Plugin.Id)), Translate("Actions.Edit"))
                                                                       }
                                                        }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Render edit form for plugin specified.
        /// </summary>
        /// <param name="id">The plugin id.</param>
        /// <returns>Plugin edit view.</returns>
        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Edit(long id)
        {
            var plugin = pluginService.Find(id);
            if (plugin == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            //build breadcrumbs
            breadcrumbsBuilder.BuildBreadcrumbs(this, new[]
                                                           {
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Titles.Home"),
                                                                       Url = Url.Action(MVC.Admin.AdminHome.Index())
                                                                   },
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Titles.Modules"),
                                                                        Url = Url.Action(MVC.Admin.Module.Index())
                                                                   },
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Actions.Edit")
                                                                   }
                                                           });

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
