using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof (IPermissible))]
    public class Role : LocalizableEntity<RoleLocale>, IPermissible, IRole
    {
        #region Fields

        private IList<User> users = new List<User>();
        private IList<UserGroup> userGroups = new List<UserGroup>();

        private String permissionTitle = "Roles";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<BaseEntityOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual String Name
        {
            get { return ((RoleLocale) CurrentLocale).Name; }
            set { ((RoleLocale) CurrentLocale).Name = value; }
        }

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

        public override ILocale InitializeLocaleEntity()
        {
            return new RoleLocale
            {
                Role = this,
                Culture = null
            };
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