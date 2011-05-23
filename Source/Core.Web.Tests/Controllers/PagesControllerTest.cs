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
        [Test]
        public void GetPagesList()
        {
       
            PagesController controller = new PagesController();
            ActionResult result  = controller.Index(new PageViewModel());

            

            Assert.IsNotNull(result,"Didn't render view");
        }
    }
}
