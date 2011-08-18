using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Products.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Products.Verbs
{
    /// <summary>
    /// Provides a navigational admin verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class AdminCategoryVerb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return HttpContext.GetGlobalResourceObject(String.Format("Product.Verbs.AdminCategoryVerb"), String.Format("Categories")) as String ?? "Categories"; }
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
                return "Category";
            }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public string ControllerPluginIdentifier
        {
            get { return ProductPlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get
            {
                return "Admin.Category";
            }
        }

        public bool IsAllowed(ICorePrincipal user)
        {
            return true;
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int) ProductsPluginOperations.ManageCategory, user,
                                               typeof (ProductPlugin), null);
        }

        #endregion
    }
}
