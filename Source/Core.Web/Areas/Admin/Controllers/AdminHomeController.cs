using System.Web.Mvc;
using Framework.MVC.Controllers;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles admin home page requests.
    /// </summary>
    public partial class AdminHomeController : FrameworkController
    {
        /// <summary>
        /// Renders admin dashboard.
        /// </summary>
        /// <returns>Dashboard view.</returns>
        public virtual ActionResult Index()
        {
            return  View();
        }
    }
}
