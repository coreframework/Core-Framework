using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Helpers;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateUserService : NHibernateDataService<User>, IUserService
    {

        #region Fields

        private PasswordMode encryptionMode = PasswordMode.SHA256;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the encryption mode.
        /// </summary>
        /// <value>The encryption mode.</value>
        public virtual PasswordMode EncryptionMode
        {
            get
            {
                return encryptionMode;
            }
            set
            {
                encryptionMode = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateUserService"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        public NHibernateUserService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        #endregion

        #region IUserService members


        /// <summary>
        /// Gets the account by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Account with username specified or null.</returns>
        public User FindByUsername(String username)
        {
            var query = from user in CreateQuery()
                        where user.Username == username
                        select user;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets the account by email or username.
        /// </summary>
        /// <param name="emailOrUsername">The email or username.</param>
        /// <returns>
        /// Account with specified email or username or <c>null</c>.
        /// </returns>
        public User FindByEmailOrUsername(String emailOrUsername)
        {
            var query = from user in CreateQuery()
                        where user.Email == emailOrUsername || user.Username == emailOrUsername
                        select user;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Encrypts <paramref name="user"/> password using <see cref="EncryptionMode"/>.
        /// </summary>
        /// <param name="user">The account to process.</param>
        /// <param name="password">The password.</param>
        public void SetPassword(User user, String password)
        {
            var encrypted = PasswordHelper.Encrypt(password, EncryptionMode);
            user.Hash = encrypted.Hash;
            user.Salt = encrypted.Salt;
            user.EncryptionMode = EncryptionMode;
        }

        /// <summary>
        /// Determines whether <paramref name="account"/> password is valid for <paramref name="account"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="account"/> password is valid; otherwise <c>false</c>.
        /// </returns>
        public bool VerifyPassword(User user, String password)
        {
            return PasswordHelper.Verify(password, new PasswordHash { Hash = user.Hash, Salt = user.Salt }, user.EncryptionMode);
        }

        public int GetCount(IQueryable<User> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<User> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(user => user.Username.Contains(searchString));
        }

        #endregion
    }
}