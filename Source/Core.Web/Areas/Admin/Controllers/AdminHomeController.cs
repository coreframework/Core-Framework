using System.Web.Mvc;
using Framework.Mvc.Breadcrumbs;
using Framework.Mvc.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles admin home page requests.
    /// </summary>
    public partial class AdminHomeController : FrameworkController
    {
        #region Fields

        private readonly IBreadcrumbsBuilder breadcrumbsBuilder;

        #endregion

        #region Constructor

        public AdminHomeController()
        {
            breadcrumbsBuilder = ServiceLocator.Current.GetInstance<IBreadcrumbsBuilder>();
        }

        #endregion

        /// <summary>
        /// Renders admin dashboard.
        /// </summary>
        /// <returns>Dashboard view.</returns>
        public virtual ActionResult Index()
        {
            breadcrumbsBuilder.BuildBreadcrumbs(this, new[] {
                                                               new Breadcrumb
                                                                   {
                                                                       Text = Translate("Titles.Home"),
                                                                       Url = Url.Action(MVC.Admin.AdminHome.Index())
                                                                   }
                                                           });
            return  View();
        }
    }
}
