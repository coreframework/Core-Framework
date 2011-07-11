using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Languages.Controllers
{
    public partial class LanguagesController : Controller
    {
        //
        // GET: /Languages/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
