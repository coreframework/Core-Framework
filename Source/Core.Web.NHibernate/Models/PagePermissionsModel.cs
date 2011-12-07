using System;
using System.Collections.Generic;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.NHibernate.Models
{
    public class PagePermissionsModel
    {
        public long PageId { get; set; }

        public IEnumerable<Int32> OperationIds { get; set; }

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
