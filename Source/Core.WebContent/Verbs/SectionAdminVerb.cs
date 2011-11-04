using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Verbs
{
    /// <summary>
    /// Provides a navigational admin verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class SectionAdminVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public String  Name
        {
            get { return HttpContext.GetGlobalResourceObject("WebContent.Verbs.SectionAdminVerb", "Sections") as String ?? "Web Content: Sections"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public String  Action
        {
            get { return "Show"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public String  Controller
        {
            get
            {
                return "Section";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public String  ControllerPluginIdentifier
        {
            get { return WebContentPlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get
            {
                return "Admin.WebContentSections";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int)WebContentPluginOperations.ManageSections, user,
                                               typeof (WebContentPlugin), null);
        }

        #endregion
    }
}
