using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;

namespace Core.TestModule.Controllers
{
    /// <summary>
    /// Manages actions of the test.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "Test")]
    public class TestController : CoreController
    {
        #region Properties

        public override string ControllerPluginIdentifier
        {
            get { return TestPlugin.GetPluginIdentifier(); }
        }

        #endregion
        
        [ImportingConstructor]
        public TestController()
        {
           
        }


        #region Actions
        /// <summary>
        /// Displays the test index.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
