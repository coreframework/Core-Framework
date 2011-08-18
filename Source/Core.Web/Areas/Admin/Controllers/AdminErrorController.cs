using System.Web.Mvc;

namespace Core.Web.Areas.Admin.Controllers
{
    public partial class AdminErrorController : Controller
    {
        //
        // GET: /Error/

        public virtual ActionResult Index()
        {
            return View(MVC.Admin.AdminError.Views.AdminError);
        }

    }
}
