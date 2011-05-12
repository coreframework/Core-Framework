using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(Plugin))]
    public partial class WidgetController : Controller
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
            return View(widgetService.GetInstalledWidgets());
        }

        /// <summary>
        /// Enables widget.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>List of registered widgets.</returns>
        [HttpPost]
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
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            return View("Index",widgetService.GetInstalledWidgets());
        }

        /// <summary>
        /// Disables widget.
        /// </summary>
        /// <param name="id">The widget id.</param>
        /// <returns>List of registered widgets.</returns>
        [HttpPost]
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
                return RedirectToAction(MVC.Admin.Widget.Index());
            }
            return View("Index", widgetService.GetInstalledWidgets());
        }

    }
}
