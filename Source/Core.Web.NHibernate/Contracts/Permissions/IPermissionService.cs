using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models.Permissions;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts.Permissions
{
    /// <summary>
    /// Specifies interface for role data service.
    /// </summary>
    public interface IPermissionService : IDataService<Permission>
    {
        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        Permission GetPermission(long roleId, long resourceId, long? entityId);

        /// <summary>
        /// Gets the resource permissions.
        /// </summary>
        /// <param name="entityType">Type of the resource.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="includeEntityNull">if set to <c>true</c> [include entity null].</param>
        /// <returns></returns>
        IEnumerable<Permission> GetResourcePermissions(Type entityType, long entityId, bool includeEntityNull);
    }
}