using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Principal;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class User : Entity, ICorePrincipal, IPermissible
    {
        #region Constructor

        public User()
        {
            Roles = new List<Role>();
            UserGroups = new List<UserGroup>();
            PermissionTitle = "Users";
            Operations = OperationsHelper.GetOperations<BaseEntityOperations>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the roles assigned to.
        /// </summary>
        /// <value>The roles assigned to.</value>
        public virtual IList<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public virtual String Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public virtual String Email { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>The password hash.</value>
        public virtual String Hash { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>The password salt.</value>
        public virtual String Salt { get; set; }

        /// <summary>
        /// Gets or sets the password encryption mode.
        /// </summary>
        /// <value>The encryption mode.</value>
        public virtual PasswordMode EncryptionMode { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public virtual UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        /// <value>The user groups.</value>
        public virtual IList<UserGroup> UserGroups { get; set; }

        #endregion

        #region ICorePrincipal members

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <value>Identity value.</value>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public virtual IIdentity Identity
        {
            get
            {
                return new GenericIdentity(Username);
            }
        }

        /// <summary>
        /// Gets the principal id.
        /// </summary>
        /// <value>The principal id.</value>
        public virtual long PrincipalId
        {
            get
            {
                return Id;
            }
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">The name of the role for which to check membership. </param>
        public virtual bool IsInRole(String role)
        {
            return !String.IsNullOrEmpty(role) && Roles.Where(r => r.Name == role).Any();
        }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual string PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the permission groups.
        /// </summary>
        /// <value>The permission groups.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
