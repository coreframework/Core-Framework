using System;
using System.ComponentModel.Composition;
using System.Web;
using Core.Forms.Permissions.Operations;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Verbs
{
    /// <summary>
    /// Provides a navigational test verb.
    /// </summary>
    [Export(typeof (IActionVerb)), ExportMetadata("Category", "AdminModules")]
    public class FormsAnswersAdminVerb : IActionVerb
    {
        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return HttpContext.GetGlobalResourceObject(String.Format("Forms.Verbs.FormsAnswersAdminVerb"), String.Format("FormsAnswers")) as String ?? "FormsAnswers"; }
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
            get { return "FormAnswers"; }
        }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public string ControllerPluginIdentifier
        {
            get { return FormsPlugin.Instance.Identifier; }
        }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public String RouteName
        {
            get { return "Admin.FormsAnswers"; }
        }

        /// <summary>
        /// Determines whether the specified user is allowed to manage forms.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// 	<c>true</c> if the specified user is allowed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllowed(ICorePrincipal user)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionService.IsAllowed((int) FormsPluginOperations.ManageFormsAnswers, user,
                                               typeof(FormsPlugin), null);
        }

        #endregion
    }
}