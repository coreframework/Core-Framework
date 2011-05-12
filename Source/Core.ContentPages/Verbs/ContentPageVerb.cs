using System.ComponentModel.Composition;
using Core.Framework.MEF.Contracts.Web;

namespace Core.ContentPages.Verbs
{
    /// <summary>
    /// Provides a navigational test verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Modules")]
    public class ContentPageVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "ContentPage"; }
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
            get { return "ContentPage"; }
        }

        public string ControllerPluginIdentifier
        {
            get { return ContentPagePlugin.GetPluginIdentifier(); }
        }

        public string RouteName
        {
            get { return null; }
        }

        #endregion
    }
}
