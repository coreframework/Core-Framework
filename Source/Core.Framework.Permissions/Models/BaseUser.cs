﻿using System;
using System.Security.Principal;
using FluentNHibernate.Data;

namespace Core.Framework.Permissions.Models
{
    public class BaseUser : Entity, ICorePrincipal
    {
        #region Properties

        public virtual String Username { get; set; }

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
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public virtual String Email { get; set; }

        #endregion

        #region ICorePrincipal

        public virtual bool IsInRole(String role)
        {
            return true;
        }

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

        #endregion
    }
}
