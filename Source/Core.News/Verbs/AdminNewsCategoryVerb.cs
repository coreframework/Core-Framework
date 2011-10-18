using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.News.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Verbs
{
    /// <summary>
    /// Provides a navigational admin verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class AdminNewsCategoryVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public String Name
        {
            get { return HttpContext.GetGlobalResourceObject(String.Format("News.Verbs.AdminNewsCategoryVerb"), String.Format("NewsCategories")) as String ?? "NewsCategoriesDefault"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public String Action
        {
            get { return "ShowAll"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public String Controller
        {
            get
            {
                return "NewsCategory";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public String ControllerPluginIdentifier
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
                return "Admin.NewsCategory";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int) NewsPluginOperations.ManageCategories, user,
                                               typeof (NewsPlugin), null);
        }

        #endregion
    }
}
