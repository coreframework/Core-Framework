using System;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IUserService : IDataService<User>
    {
        /// <summary>
        /// Gets the account by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Account with username specified or null.</returns>
        User FindByUsername(String username);

        /// <summary>
        /// Gets the account by email or username.
        /// </summary>
        /// <param name="emailOrUsername">The email or username.</param>
        /// <returns>
        /// Account with specified email or username or <c>null</c>.
        /// </returns>
        User FindByEmailOrUsername(String emailOrUsername);

        /// <summary>
        /// Encrypts <paramref name="user"/> password using default mode.
        /// </summary>
        /// <param name="user">The user to process.</param>
        /// <param name="password">The password.</param>
        void SetPassword(User user, String password);

        /// <summary>
        /// Determines whether <paramref name="account"/> password is valid for <paramref name="account"/>.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="account"/> password is valid; otherwise <c>false</c>.
        /// </returns>
        bool VerifyPassword(User user, String password);

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
        IQueryable<User> GetSearchQuery(string searchString);
    }
}