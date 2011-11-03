using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Core.WebContents.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "WebContent_Section")]
    public partial class SectionController : Controller
    {
        #region Constructor

        #endregion

        #region Actions

        public virtual ActionResult Show()
        {
            return View();
        }

        #endregion
    }
}
