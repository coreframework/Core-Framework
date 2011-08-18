using System.Web.Mvc;
using Framework.MVC.Breadcrumbs;
using Framework.MVC.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles admin home page requests.
    /// </summary>
    public partial class AdminHomeController : FrameworkController
    {
        #region Fields

        private readonly IBreadcrumbsBuilder _breadcrumbsBuilder;

        #endregion

        #region Constructor

        public AdminHomeController()
        {
            _breadcrumbsBuilder = ServiceLocator.Current.GetInstance<IBreadcrumbsBuilder>();
        }

        #endregion

        /// <summary>
        /// Renders admin dashboard.
        /// </summary>
        /// <returns>Dashboard view.</returns>
        public virtual ActionResult Index()
        {
            _breadcrumbsBuilder.BuildBreadcrumbs(this, new[] {
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
