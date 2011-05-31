using System.Web.Mvc;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var page = pageService.GetFirstAllowedPage(this.CorePrincipal(), (int) PageOperations.View);

            if (page !=null)
            {
                return View(MVC.Pages.Views.Show, PageHelper.BindPageViewModel(page, this.CorePrincipal()));
            }

            return View();
        }
    }
}
