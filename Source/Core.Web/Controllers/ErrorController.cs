using System.Web.Mvc;

namespace Core.Web.Controllers
{
    public partial class ErrorController : Controller
    {
        public virtual ActionResult Index()
        {
            return View("Error");
        }
    }
}
