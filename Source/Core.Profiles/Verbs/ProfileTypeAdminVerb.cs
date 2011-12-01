using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Profiles.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Verbs
{
    /// <summary>
    /// Provides a navigational admin verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class ProfileTypeAdminVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public String  Name
        {
            get { return HttpContext.GetGlobalResourceObject("Profiles.Verbs.ProfileTypeAdminVerb", "ProfileTypes") as String ?? "Profiles: ProfileTypes"; }
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
                return "ProfileType";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public String  ControllerPluginIdentifier
        {
            get { return ProfilesPlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get
            {
                return "Admin.ProfileTypes";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int)ProfilesPluginOperations.ManageProfileTypes, user,
                                               typeof (ProfilesPlugin), null);
        }

        #endregion
    }
}
