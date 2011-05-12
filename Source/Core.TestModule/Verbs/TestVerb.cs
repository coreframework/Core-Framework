using System;
using System.ComponentModel.Composition;
using Core.Framework.MEF.Contracts.Web;

namespace Core.TestModule.Verbs
{
    /// <summary>
    /// Provides a navigational test verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Modules")]
    public class TestVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Test"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action
        {
            get { return "Index"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public string Controller
        {
            get { return "Test"; }
        }

        public string ControllerPluginIdentifier
        {
            get { return TestPlugin.GetPluginIdentifier(); }
        }

        public string RouteName
        {
            get { return null; }
        }

        #endregion
    }
}
