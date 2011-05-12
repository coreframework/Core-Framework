using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Models;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Extension methods for authentication control.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Currents the user.
        /// </summary>
        /// <param name="controller">The controller instance that this method extends.</param>
        /// <returns>Core principal user associated with current controller context.</returns>
        public static ICorePrincipal CorePrincipal(this Controller controller)
        {
            return controller.User as ICorePrincipal;
        }

        /// <summary>
        /// Currents the user.
        /// </summary>
        /// <param name="context">The http context instance that this method extends.</param>
        /// <returns>Core principal user associated with current http context.</returns>
        public static ICorePrincipal CorePrincipal(this HttpContext context)
        {
            return context.User as ICorePrincipal;
        }
    }
}