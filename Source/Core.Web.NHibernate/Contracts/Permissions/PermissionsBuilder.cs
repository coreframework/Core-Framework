using System;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;

namespace Core.Web.NHibernate.Contracts.Permissions
{
    /// <summary>
    /// Provides fluent API from <see cref="Permission"/> definition.
    /// </summary>
    public class PermissionsBuilder : IPermissionsBuilder
    {
        #region Fields

        private readonly Permission rule;

        private readonly IRoleService roleService;

        private readonly IPermissionService permissionService;

        private readonly bool revoke;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsBuilder"/> class.
        /// </summary>
        /// <param name="rule">The permission rule.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="permissionService">The permissions service.</param>
        public PermissionsBuilder(Permission rule, IRoleService roleService, IPermissionService permissionService)
        {
            this.rule = rule;
            this.roleService = roleService;
            this.permissionService = permissionService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsBuilder"/> class.
        /// </summary>
        /// <param name="rule">The permission rule.</param>
        /// <param name="roleService">The role service.</param>
        /// <param name="permissionService">The permissions service.</param>
        /// <param name="revoke">if set to <c>true</c> [revoke].</param>
        public PermissionsBuilder(Permission rule, IRoleService roleService, IPermissionService permissionService, bool revoke)
        {
            this.rule = rule;
            this.roleService = roleService;
            this.permissionService = permissionService;
            this.revoke = revoke;
        }

        #endregion

        #region IPermissionsBuilder members

        /// <summary>
        /// Specifies operation to grant opertaion on.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="entityType">The entity to assign permission on.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsBuilder On<T>(Type entityType)
            where T : IPermissible
        {
            rule.EntityType.Name = PermissionsHelper.GetEntityType(entityType);
            return this;
        }

        /// <summary>
        /// Specifies operation to grant opertaion on.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsBuilder On<T>(long entityId)
            where T : IPermissible
        {
            rule.EntityId = entityId;
            return this;
        }

        /// <summary>
        /// Specifies operation to grant opertaion on.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsBuilder On(Type entityType, long? entityId)
        {
            rule.EntityType.Name = PermissionsHelper.GetEntityType(entityType);
            rule.EntityId = entityId;
            return this;
        }

        /// <summary>
        /// Specifies role to grant permission.
        /// </summary>
        /// <param name="role">The role to grant permission.</param>
        /// <returns>Permission builder instance.</returns>
        public IPermissionsBuilder For(Role role)
        {
            rule.Role = role;
            return this;
        }

        /// <summary>
        /// Creates permissions rule without saving.
        /// </summary>
        /// <returns>Created permission rule instance.</returns>
        public Permission Build()
        {
            ValidateRule();

            return rule;
        }

        /// <summary>
        /// Saves permission rule.
        /// </summary>
        /// <returns>Created permission rule instance.</returns>
        public Permission Save()
        {
            ValidateRule();

            if (!revoke)
            {
                permissionService.Save(rule);
            }
            else
            {
              //  permissionService.Revoke(rule);
            }
            return rule;
        }

        #endregion

        #region Helper members

        private void ValidateRule()
        {
            if (rule.Role == null)
            {
                throw new InvalidOperationException("User or role should be specified for permission assignment.");
            }
        }

        #endregion
    }
}