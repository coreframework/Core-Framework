using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using NHibernate.Criterion;

namespace Core.Framework.Permissions.Contracts
{
    public interface IPermissionCommonService
    {
        /// <summary>
        /// Determines whether the specified operation is allowed.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified operation is allowed; otherwise, <c>false</c>.
        /// </returns>
        bool IsAllowed(int operation, ICorePrincipal user, Type entityType, long? entityId);

        /// <summary>
        /// Determines whether the specified operation is allowed.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="level">The level.</param>
        /// <returns>
        /// 	<c>true</c> if the specified operation is allowed; otherwise, <c>false</c>.
        /// </returns>
        bool IsAllowed(Int32 operation, ICorePrincipal user, Type entityType, long? entityId, PermissionOperationLevel level);

        /// <summary>
        /// Determines whether the specified operation is allowed.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="isOwner">if set to <c>true</c> [is owner].</param>
        /// <param name="level">The level.</param>
        /// <returns>
        /// 	<c>true</c> if the specified operation is allowed; otherwise, <c>false</c>.
        /// </returns>
        bool IsAllowed(Int32 operation, ICorePrincipal user, Type entityType, long? entityId, bool isOwner, PermissionOperationLevel level);

        /// <summary>
        /// Gets the permission access.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        Dictionary<Int32, bool> GetAccess(IEnumerable<IPermissionOperation> operations, ICorePrincipal user, Type entityType, long? entityId);

        /// <summary>
        /// Gets the access.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="isOwner">if set to <c>true</c> [is owner].</param>
        /// <returns></returns>
        Dictionary<Int32, bool> GetAccess(IEnumerable<IPermissionOperation> operations, ICorePrincipal user, Type entityType, long? entityId, bool isOwner);

        /// <summary>
        /// Setups the default role permissions.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="type">The type.</param>
        /// <param name="entityId">The entity id.</param>
        void SetupDefaultRolePermissions(IEnumerable<IPermissionOperation> operations, Type type, long entityId);

        /// <summary>
        /// Clones the object permisions.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="sourceEntityId">The source entity id.</param>
        /// <param name="targetEntityId">The target entity id.</param>
        void CloneObjectPermisions(Type type, long sourceEntityId, long targetEntityId);

        /// <summary>
        /// Gets the permissions criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <param name="permissibleObjectType">Type of the permissible object.</param>
        /// <param name="permissibleIdPropertyName">Name of the permissible id property.</param>
        /// <param name="permissibleOwnerPropertyName">Name of the permissible owner property.</param>
        /// <returns></returns>
        AbstractCriterion GetPermissionsCriteria(ICorePrincipal user, int operationCode,
                                         Type permissibleObjectType, String permissibleIdPropertyName,
                                         String permissibleOwnerPropertyName);
    }
}
