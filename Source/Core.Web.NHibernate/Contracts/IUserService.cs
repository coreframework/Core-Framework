using System;
using System.Linq;
using Core.Framework.Permissions.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IUserService : IDataService<User>, IBaseUserService
    {
        /// <summary>
        /// Determines whether email used by specified user is unique.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="email">The user email.</param>
        /// <returns>
        ///     <c>true</c> if email used by specified user is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsEmailUnique(long id, String email);

        /// <summary>
        /// Determines whether user with specified email already exists.
        /// </summary>
        /// <param name="email">The email for validation.</param>
        /// <returns>
        ///     <c>true</c> if email is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsEmailUnique(String email);

        /// <summary>
        /// Determines whether username used by specified user is unique.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="username">The user username.</param>
        /// <returns>
        ///     <c>true</c> if username used by specified user is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsUsernameUnique(long id, String username);

        /// <summary>
        /// Determines whether user with specified username already exists.
        /// </summary>
        /// <param name="username">The username for validation.</param>
        /// <returns>
        ///     <c>true</c> if username is unique; otherwise, <c>false</c>.
        /// </returns>
        bool IsUsernameUnique(String username);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>User with email specified or <c>null</c>.</returns>
        User FindByEmail(String email);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User with username specified or null.</returns>
        User FindByUsername(String username);

        /// <summary>
        /// Encrypts <paramref name="user"/> password using default mode.
        /// </summary>
        /// <param name="user">The user to process.</param>
        /// <param name="password">The password.</param>
        void SetPassword(User user, String password);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<User> baseQuery);
        
        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<User> GetSearchQuery(String searchString);
    }
}