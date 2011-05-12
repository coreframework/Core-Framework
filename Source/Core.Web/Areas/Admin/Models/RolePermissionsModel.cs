using System.Collections.Generic;
using Core.Framework.Permissions.Models;

namespace Core.Web.Areas.Admin.Models
{
    public class RolePermissionsModel
    {
        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        public long RoleId { get; set; }

        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>The resource id.</value>
        public long? ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        public PermissionArea Area { get; set; }

        /// <summary>
        /// Gets or sets the permissible objects.
        /// </summary>
        /// <value>The permissible objects.</value>
        public List<RolePermissionsItem> PermissibleObjects { get; set; }

        /// <summary>
        /// Gets or sets the operations model.
        /// </summary>
        /// <value>The operations model.</value>
        public PermissionOperationsModel OperationsModel { get; set; }

    }
}