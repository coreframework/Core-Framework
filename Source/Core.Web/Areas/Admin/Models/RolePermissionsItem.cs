using System;
using Core.Framework.Permissions.Models;

namespace Core.Web.Areas.Admin.Models
{
    public class RolePermissionsItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        /// <value>The object.</value>
        public PermissionArea  Area { get; set; }
    }
}