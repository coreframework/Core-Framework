using System;
using Core.Framework.Permissions.Models;

namespace Core.Framework.MEF.Contracts.Web
{
    /// <summary>
    /// Defines an action verb.
    /// </summary>
    public interface IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name of the verb.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        String Action { get; }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        String Controller { get; }

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        String ControllerPluginIdentifier { get; }

        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        String RouteName { get; }

        /// <summary>
        /// Determines whether the specified user is allowed.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// 	<c>true</c> if the specified user is allowed; otherwise, <c>false</c>.
        /// </returns>
        bool IsAllowed(ICorePrincipal user);

        #endregion
    }
}
