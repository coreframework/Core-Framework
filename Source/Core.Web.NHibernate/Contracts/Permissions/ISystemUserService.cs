using System;
using Core.Framework.Permissions.Models;

namespace Core.Web.NHibernate.Contracts.Permissions
{
    /// <summary>
    /// Specifies interface for user data service.
    /// </summary>
    public interface ISystemUserService
    {
        /// <summary>
        /// Finds user by name.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <returns>User with name spaecified or <c>null</c>.</returns>
        ICorePrincipal GetUser(String name);

    }
}
