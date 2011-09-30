using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.ContentPages.Permissions.Operations;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.ContentPages.Verbs
{
    /// <summary>
    /// Provides a navigational admin verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class AdminVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public String  Name
        {
            get { return HttpContext.GetGlobalResourceObject("ContentPage.Verbs.AdminVerb", "ContentPages") as String ?? "Content Pages"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public String  Action
        {
            get { return "ShowAll"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public String  Controller
        {
            get
            {
                return "ContentPage";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public String  ControllerPluginIdentifier
        {
            get { return ContentPagePlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get
            {
                return "Admin.ContentPages";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int) ContentPagePluginOperations.ManageContentPages, user,
                                               typeof (ContentPagePlugin), null);
        }

        #endregion
    }
}
