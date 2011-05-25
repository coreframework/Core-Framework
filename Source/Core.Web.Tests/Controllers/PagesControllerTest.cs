using System.Web;
using System.Web.Mvc;
using Core.Web.Controllers;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.Tests.Services;
using NUnit.Framework;

namespace Core.Web.Tests.Controllers
{
    [TestFixture]
    public class PagesControllerTest : AbstractServiceTest<Page, IPageService>
    {
        /// <summary>
        /// Creates the page.
        /// </summary>
        /// <returns></returns>
        public Page CreatePage()
        {
            var page = new Page
            {
                Title = "Test page",
                Url = "test-page-url"
            };
            return page;
        }

        public void GetPagesList()
        {
            var pageService = Container.Resolve<IPageService>();

            var page = CreatePage();
            pageService.Save(page);
       
            PagesController controller = new PagesController();
            ActionResult result  = controller.Index(new PageViewModel().MapFrom(page));

            Assert.IsNotNull(result,"Didn't render view");
            pageService.Delete(page);
        }
    }
}
