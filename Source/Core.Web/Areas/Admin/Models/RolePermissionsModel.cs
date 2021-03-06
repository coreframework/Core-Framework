﻿using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;

namespace Core.Web.Areas.Admin.Models
{
    public class RolePermissionsModel
    {
        #region Properties

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
        public IEnumerable<PermissionOperationsModel> OperationsModels { get; set; }

        public String Title { get; set; }

        #endregion

        #region Constructors

        public RolePermissionsModel()
        {
            OperationsModels = new List<PermissionOperationsModel>();
        }

        #endregion

    }
}