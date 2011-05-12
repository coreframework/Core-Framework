using System;
using System.Collections.Generic;

namespace Core.Framework.Permissions.Models
{
    /// <summary>
    /// Specifies entities applicable for permissions.
    /// </summary>
    public interface IPermissible
    {

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        String PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the permission groups.
        /// </summary>
        /// <value>The permission groups.</value>
        IEnumerable<IPermissionOperation> Operations { get; set; }
    }
}