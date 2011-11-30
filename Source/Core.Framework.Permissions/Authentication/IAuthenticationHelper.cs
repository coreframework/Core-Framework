using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Authentication
{
    /// <summary>
    /// Specifies interface for controller that controls user authentication process.
    /// </summary>
    public interface IAuthenticationHelper
    {
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="remember"><c>true</c> to create a persistent cookie (one that is saved across browser sessions); otherwise, <c>false</c>.</param>
        void LoginUser(ICorePrincipal account, bool remember);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        void LogoutUser();

    }
}
