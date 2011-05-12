using System.Web.Mvc;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles admin home page requests.
    /// </summary>
    public partial class AdminHomeController : Controller
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
