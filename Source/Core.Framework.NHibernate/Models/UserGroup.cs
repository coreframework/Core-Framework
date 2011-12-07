using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Framework.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class UserGroup : Entity, IPermissible
    {
        #region Fields

        private IList<User> users = new List<User>();

        private String permissionTitle = "User Groups";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<BaseEntityOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public virtual String Description { get; set; }

        public virtual IList<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual String PermissionTitle
        {
            get { return permissionTitle; }
            set { permissionTitle = value; }
        }

        /// <summary>
        /// Gets or sets the permission operations.
        /// </summary>
        /// <value>The permission operations.</value>
        public virtual IEnumerable<IPermissionOperation> Operations
        {
            get { return operations; }
            set { operations = value; }
        }

        #endregion
    }
}