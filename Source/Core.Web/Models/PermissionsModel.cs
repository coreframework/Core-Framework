using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.Models
{
    public class PermissionsModel
    {
        /// <summary>
        /// Gets or sets the page id.
        /// </summary>
        /// <value>The page id.</value>
        public long EntityId { get; set; }

        /// <summary>
        /// Gets or sets the operation ids.
        /// </summary>
        /// <value>The operation ids.</value>
        public IEnumerable<String> Actions { get; set; }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public IEnumerable<IPermissionOperation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public IEnumerable<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IEnumerable<Role> Roles { get; set; }

    }
}
