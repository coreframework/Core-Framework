using System;
using System.Collections.Generic;

namespace Core.Framework.Permissions.Models
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
        public IEnumerable<IPermissionModel> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IEnumerable<IRole> Roles { get; set; }
    }
}
