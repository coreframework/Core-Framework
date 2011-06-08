using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class Role : Entity, IRole, IPermissible
    {
        #region Fields

        private IList<User> users = new List<User>();
        private IList<UserGroup> userGroups = new List<UserGroup>();

        #endregion

        public Role()
        {
            PermissionTitle = "Roles";
            Operations = OperationsHelper.GetOperations<BaseEntityOperations>();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is system role.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is system role; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsSystemRole { get; set; }

        public virtual bool NotAssignableRole { get; set; }

        public virtual bool NotPermissible { get; set; }

        public virtual IList<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public virtual IList<UserGroup> UserGroups
        {
            get { return userGroups; }
            set { userGroups = value; }
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
