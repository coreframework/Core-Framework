using System;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Contracts
{
    public interface IBaseUserService
    {
        /// <summary>
        /// Gets the user by email or username.
        /// </summary>
        /// <param name="emailOrUsername">The email or username.</param>
        /// <returns>
        /// User with specified email or username or <c>null</c>.
        /// </returns>
        BaseUser FindByEmailOrUsername(String emailOrUsername);

        /// <summary>
        /// Determines whether <paramref name="user"/> password is valid for <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="user"/> password is valid; otherwise <c>false</c>.
        /// </returns>
        bool VerifyPassword(BaseUser user, String password);
    }
}
