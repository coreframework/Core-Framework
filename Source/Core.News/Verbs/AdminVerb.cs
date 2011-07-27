using System;
using System.ComponentModel.Composition;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.News.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("News", "AdminModules")]
    public class AdminVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "News"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action
        {
            get { return "ShowAll"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public string Controller
        {
            get
            {
                return "News";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public string ControllerPluginIdentifier
        {
            get { return NewsPlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get
            {
                return "Admin.News";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int)NewsPluginOperations.ManageNews, user,
                                               typeof(NewsPlugin), null);
        }

        #endregion
    }
}