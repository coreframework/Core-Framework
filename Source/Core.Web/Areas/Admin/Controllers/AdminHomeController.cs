using System.Web.Mvc;
using Framework.Mvc.Controllers;

namespace Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Handles admin home page requests.
    /// </summary>
    public partial class AdminHomeController : FrameworkController
    {
        #region Fields

        #endregion

        #region Constructor

        public AdminHomeController()
        {
        }

        #endregion

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
