using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.Areas.Admin.Models
{
    public class PermissionOperationsModel
    {
        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>The resource id.</value>
        public long ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        public long RoleId { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        public PermissionArea Area { get; set; }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public IEnumerable<IPermissionOperation> Operations { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public Permission Permissions { get; set; }

        /// <summary>
        /// Gets or sets the operation ids.
        /// </summary>
        /// <value>The operation ids.</value>
        public IEnumerable<Int32> OperationIds { get; set; }

        public String Title { get; set; }
    }
}