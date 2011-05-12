using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Migrator;
using Core.Web.NHibernate.Models;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles module administration operation requests.
    /// </summary>
    [Permissions((int)BaseEntityOperations.Manage, typeof(Plugin))]
    public partial class ModuleController : Controller
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
            return View(PluginHelper.GetAvailablePlugins());
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
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
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
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            if (pluginEntity.Status.Equals(PluginStatus.NotInstalled))
            {
                ICorePlugin corePlugin =
                    MvcApplication.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
                if (corePlugin == null)
                {
                    throw new HttpException((int) HttpStatusCode.NotFound,
                                            HttpContext.Translate("Messages.CouldNotFoundPlugin",
                                                                  ResourceHelper.GetControllerScope(this)));
                }
                corePlugin.Install();
                CoreMigrator.Current.MigrateUp(corePlugin);
                pluginEntity.Status = PluginStatus.Installed;
                pluginService.Save(pluginEntity);
            }
            return View("Index", PluginHelper.GetAvailablePlugins());
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
                    MvcApplication.Plugins.FirstOrDefault(pl => pl.Identifier == pluginEntity.Identifier);
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

            return View("Index", PluginHelper.GetAvailablePlugins());
        }
    }
}
