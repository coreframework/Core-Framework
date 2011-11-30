using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Framework.Facilities.NHibernate;

namespace Core.Framework.Permissions.Services
{
    public class NHibernateBaseUserService<T> : NHibernateDataService<T>, IBaseUserService where T : BaseUser
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateBaseUserService"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public NHibernateBaseUserService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #endregion

        #region IBaseUserService members

        /// <summary>
        /// Gets the user by email or username.
        /// </summary>
        /// <param name="emailOrUsername">The email or username.</param>
        /// <returns>
        /// User with specified email or username or <c>null</c>.
        /// </returns>
        public BaseUser FindByEmailOrUsername(String emailOrUsername)
        {
            var query = from user in CreateQuery()
                        where user.Email == emailOrUsername || user.Username == emailOrUsername
                        select user;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Determines whether <paramref name="user"/> password is valid for <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="user"/> password is valid; otherwise <c>false</c>.
        /// </returns>
        public bool VerifyPassword(BaseUser user, String password)
        {
            return PasswordHelper.Verify(password, new PasswordHash { Hash = user.Hash, Salt = user.Salt }, user.EncryptionMode);
        }

        #endregion
    }
}
