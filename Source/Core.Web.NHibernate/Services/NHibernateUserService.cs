using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Permissions.Services;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateUserService : NHibernateBaseUserService<User>, IUserService
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
        /// Determines whether email used by specified user is unique.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="email">The user email.</param>
        /// <returns>
        ///     <c>true</c> if email used by specified user is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmailUnique(long id, String email)
        {
            var query = from a in CreateQuery()
                        where a.Email == email && a.Id != id
                        select a;

            return query.Count() == 0;
        }

        /// <summary>
        /// Determines whether user with specified email already exists.
        /// </summary>
        /// <param name="email">The email for validation.</param>
        /// <returns>
        ///     <c>true</c> if email is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmailUnique(String email)
        {
            var query = from a in CreateQuery()
                        where a.Email == email
                        select a;

            return query.Count() == 0;
        }

        /// <summary>
        /// Determines whether username used by specified user is unique.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="username">The user username.</param>
        /// <returns>
        ///     <c>true</c> if username used by specified user is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUsernameUnique(long id, String username)
        {
            var query = from a in CreateQuery()
                        where a.Username == username && a.Id != id
                        select a;

            return query.Count() == 0;
        }

        /// <summary>
        /// Determines whether user with specified username already exists.
        /// </summary>
        /// <param name="username">The username for validation.</param>
        /// <returns>
        ///     <c>true</c> if username is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUsernameUnique(String username)
        {
            var query = from a in CreateQuery()
                        where a.Username == username
                        select a;

            return query.Count() == 0;
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>User with email specified or null.</returns>
        public User FindByEmail(String email)
        {
            var query = from a in CreateQuery()
                        where a.Email == email
                        select a;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User with username specified or null.</returns>
        public User FindByUsername(String username)
        {
            var query = from user in CreateQuery()
                        where user.Username == username
                        select user;

            return query.FirstOrDefault();
        }

        

        /// <summary>
        /// Encrypts <paramref name="user"/> password using <see cref="EncryptionMode"/>.
        /// </summary>
        /// <param name="user">The user to process.</param>
        /// <param name="password">The password.</param>
        public void SetPassword(User user, String password)
        {
            var encrypted = PasswordHelper.Encrypt(password, EncryptionMode);
            user.Hash = encrypted.Hash;
            user.Salt = encrypted.Salt;
            user.EncryptionMode = EncryptionMode;
        }

        

        public int GetCount(IQueryable<User> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<User> GetSearchQuery(String searchString)
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