using System;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.NHibernate.Helpers.Permissions
{
    /// <summary>
    /// Provides fluent API from <see cref="Permission"/> querying.
    /// </summary>
    public class PermissionsQuery : IPermissionsQuery
    {
        #region Fields

        private readonly IRoleService roleService;

        private readonly IPermissionService permissionService;

        private Type entityType;

        private long entityId;

        private Role role;

        private ICorePrincipal user;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsQuery"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        /// <param name="permissionService">The permission service.</param>
        public PermissionsQuery(IRoleService roleService, IPermissionService permissionService)
        {
            this.roleService = roleService;
            this.permissionService = permissionService;
        }

        #endregion

        #region IPermissionsQuery members

        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="resource">The resource to assign permission on.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsQuery On<T>(T resource) where T : IPermissable
        {
            return On(typeof(T), resource.EntityId);
        }

        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsQuery On<T>(long resourceId) where T : IPermissable
        {
            return On(typeof(T), resourceId);
        }

        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsQuery On(Type resourceType, long resourceId)
        {
            entityType = resourceType;
            entityId = resourceId;
            return this;
        }

        /// <summary>
        /// Specifies user for permissions query.
        /// </summary>
        /// <param name="forUser">The user for permissions query.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsQuery For(ICorePrincipal forUser)
        {
            user = forUser;
            return this;
        }

        /// <summary>
        /// Specifies role for permissions query.
        /// </summary>
        /// <param name="forRole">The role for permissions query.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsQuery For(Role forRole)
        {
            role = forRole;
            return this;
        }

        /// <summary>
        /// Executes permissions query to determine wheter operation is allowed for specified query.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the specified operation is allowed; otherwise, <c>false</c>.
        /// </returns>
        public bool Check()
        {
            ValidateRule();
            return permissionService.IsAllowed(role, user, entityType, entityId);
        }

        #endregion

        #region Helper members

        private void ValidateRule()
        {
            if (role == null)
            {
                throw new InvalidOperationException("User or role should be specified for permission assignment.");
            }
        }

        #endregion
    }
}