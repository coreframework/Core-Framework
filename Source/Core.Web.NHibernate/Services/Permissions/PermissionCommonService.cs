using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Framework.Core.Extensions;
using Framework.Facilities.NHibernate;
using LinqKit;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Type;
using EntityType = Core.Web.NHibernate.Models.Permissions.EntityType;
using Omu.ValueInjecter;

namespace Core.Web.NHibernate.Services.Permissions
{
    public class PermissionCommonService : NHibernateDataService<Permission>, IPermissionCommonService
    {
        public PermissionCommonService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public bool IsAllowed(int operation, ICorePrincipal user, Type entityType, long? entityId)
        {
            return IsAllowed(operation, user, entityType, entityId, false, entityId!=null? PermissionOperationLevel.Object: PermissionOperationLevel.Type);
        }

        public bool IsAllowed(int operation, ICorePrincipal user, Type entityType, long? entityId, PermissionOperationLevel level)
        {
            return IsAllowed(operation, user, entityType, entityId, false, level);
        }

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
        public bool IsAllowed(int operation, ICorePrincipal user, Type entityType, long? entityId, bool isOwner, PermissionOperationLevel level)
        {
            //check if user is administrator
            if (user != null && user.IsInRole(SystemRoles.Administrator.ToString()))
                return true;

            bool isAllowed = false;

            var criteria = Session.CreateCriteria<Permission>();

            if (user != null)
            {
                var rolesSubQuery = DetachedCriteria.For<Role>()
                               .CreateAlias("Users", "user")
                               .Add(Restrictions.Eq("user.id", user.PrincipalId))
                               .SetProjection(Projections.Id());

                var userUserGroupsSubQuery = DetachedCriteria.For<UserGroup>()
                           .CreateAlias("Users", "userGroupUser", JoinType.LeftOuterJoin)
                           .Add(Restrictions.Eq("userGroupUser.id", user.PrincipalId))
                           .SetProjection(Projections.Id());

                var userGroupsRolesSubQuery = DetachedCriteria.For<Role>()
                             .CreateAlias("UserGroups", "userGroup", JoinType.LeftOuterJoin)
                             .Add(Subqueries.PropertyIn("userGroup.id", userUserGroupsSubQuery))
                             .SetProjection(Projections.Id());

                criteria.Add(Restrictions.Or(
                    Restrictions.Or(Subqueries.PropertyIn("Role.Id", rolesSubQuery), Subqueries.PropertyIn("Role.Id", userGroupsRolesSubQuery)), !isOwner ?
                    Restrictions.Eq("Role.Id", (Int64)SystemRoles.User) :
                    Restrictions.In("Role.Id", new List<SystemRoles> { SystemRoles.User, SystemRoles.Owner })));
            }
            else
            {
                criteria.Add(Restrictions.Eq("Role.Id", (Int64)SystemRoles.Guest));
            }

            criteria.CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(entityType)));

            switch (level)
            {
                case PermissionOperationLevel.Type:
                    criteria.Add(Restrictions.IsNull("EntityId"));
                    break;
                case PermissionOperationLevel.Object:
                    criteria.Add(Restrictions.Eq("EntityId", entityId));
                    break;
                case PermissionOperationLevel.ObjectType:
                    criteria.Add(Restrictions.Or(Restrictions.IsNull("EntityId"), Restrictions.Eq("EntityId",entityId)));
                    break;
            }

            var rules = criteria.SetCacheable(true).List<Permission>();

            foreach (var rule in rules.Where(rule => !isAllowed))
            {
                isAllowed = (rule.Permissions & operation) == operation;
            }

            return isAllowed;
        }

        public Dictionary<int, bool> GetAccess(IEnumerable<IPermissionOperation> operations, ICorePrincipal user, Type entityType, long? entityId)
        {
            return GetAccess(operations, user, entityType, entityId, false);
        }

        /// <summary>
        /// Gets the access.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <param name="isOwner">if set to <c>true</c> [is owner].</param>
        /// <returns></returns>
        public Dictionary<int, bool> GetAccess(IEnumerable<IPermissionOperation> operations, ICorePrincipal user, Type entityType, long? entityId, bool isOwner)
        {
            //check if user is administrator
            if (user != null && user.IsInRole(SystemRoles.Administrator.ToString()))
                return operations.ToDictionary(value => value.Key, value => true);

            var result = operations.ToDictionary(value => value.Key, value => false);

            var criteria = Session.CreateCriteria<Permission>();

            if (user != null)
            {
                var rolesSubQuery = DetachedCriteria.For<Role>()
                               .CreateAlias("Users", "user", JoinType.LeftOuterJoin)
                               .Add(Restrictions.Eq("user.id", user.PrincipalId))
                               .SetProjection(Projections.Id());

                var userUserGroupsSubQuery = DetachedCriteria.For<UserGroup>()
                               .CreateAlias("Users", "userGroupUser", JoinType.LeftOuterJoin)
                               .Add(Restrictions.Eq("userGroupUser.id", user.PrincipalId))
                               .SetProjection(Projections.Id());

                var userGroupsRolesSubQuery = DetachedCriteria.For<Role>()
                             .CreateAlias("UserGroups", "userGroup", JoinType.LeftOuterJoin)
                             .Add(Subqueries.PropertyIn("userGroup.id", userUserGroupsSubQuery))
                             .SetProjection(Projections.Id());

                criteria.Add(Restrictions.Or(
                  Restrictions.Or(Subqueries.PropertyIn("Role.Id", rolesSubQuery), Subqueries.PropertyIn("Role.Id", userGroupsRolesSubQuery)), !isOwner ?
                  Restrictions.Eq("Role.Id", (Int64)SystemRoles.User) :
                  Restrictions.In("Role.Id", new List<SystemRoles> { SystemRoles.User, SystemRoles.Owner })));
            }
            else
            {
                criteria.Add(Restrictions.Eq("Role.Id", (Int64)SystemRoles.Guest));
            }

            criteria.Add(Restrictions.Eq("EntityId", entityId)).CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(entityType)));

            var permissions = criteria.SetCacheable(true).List<Permission>();

            permissions.ForEach(permission =>
                operations.Where(operation => !result[operation.Key]).ForEach(
                operation => result[operation.Key] = (permission.Permissions & operation.Key) == operation.Key));

            return result;
        }

        /// <summary>
        /// Setups the default role permissions.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <param name="type">The type.</param>
        /// <param name="entityId">The entity id.</param>
        public void SetupDefaultRolePermissions(IEnumerable<IPermissionOperation> operations, Type type, long entityId)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();
            var entityTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();

            EntityType entityType = entityTypeService.GetByType(type);

            if (operations!=null && entityType != null)
            {
                //setup permissions for Owner
                var ownerPermissions = new Permission
                                           {
                                               EntityId = entityId,
                                               EntityType = entityType,
                                               Role = new Role {Id = (long) SystemRoles.Owner},
                                               Permissions =
                                                   operations.Where(
                                                       permissionOperation => permissionOperation.OwnerDefaultAcess).
                                                   Aggregate(0,
                                                             (current, permissionOperation) =>
                                                             current | permissionOperation.Key)
                                           };
                permissionService.Save(ownerPermissions);

                //setup permissions for User
                var userPermissions = new Permission
                                          {
                                              EntityId = entityId,
                                              EntityType = entityType,
                                              Role = new Role {Id = (long) SystemRoles.User},
                                              Permissions =
                                                  operations.Where(
                                                      permissionOperation => permissionOperation.UserDefaultAccess).
                                                  Aggregate(0,
                                                            (current, permissionOperation) =>
                                                            current | permissionOperation.Key)
                                          };
                permissionService.Save(userPermissions);

                //setup permissions for Guest
                var guestPermissions = new Permission
                                           {
                                               EntityId = entityId,
                                               EntityType = entityType,
                                               Role = new Role {Id = (long) SystemRoles.Guest},
                                               Permissions =
                                                   operations.Where(
                                                       permissionOperation => permissionOperation.GuestDefaultAcess).
                                                   Aggregate(0,
                                                             (current, permissionOperation) =>
                                                             current | permissionOperation.Key)
                                           };

                permissionService.Save(guestPermissions);
            }
        }

        public void CloneObjectPermisions(Type type, long sourceEntityId, long targetEntityId)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();

            var query = from permission in permissionService.CreateQuery()
                        where permission.EntityId == sourceEntityId && permission.EntityType.Name == PermissionsHelper.GetEntityType(type)
                        select permission;

            foreach (var targetPermission in
                query.ToList().Select(permission => new Permission().InjectFrom<CloneEntityInjection>(permission) as Permission))
            {
                targetPermission.EntityId = targetEntityId;
                permissionService.Save(targetPermission);
            }
        }

        public AbstractCriterion GetPermissionsCriteria(ICorePrincipal user, int operationCode, Type permissibleObjectType, String permissibleIdPropertyName, String permissibleOwnerPropertyName)
        {
            if (user != null)
            {
                if (user.IsInRole(SystemRoles.Administrator.ToString()))
                    return null;

                var rolesSubQuery = DetachedCriteria.For<Role>()
                               .CreateAlias("Users", "user")
                               .Add(Restrictions.Eq("user.id", user.PrincipalId))
                               .SetProjection(Projections.Id());

                var userUserGroupsSubQuery = DetachedCriteria.For<UserGroup>()
                          .CreateAlias("Users", "userGroupUser", JoinType.LeftOuterJoin)
                          .Add(Restrictions.Eq("userGroupUser.id", user.PrincipalId))
                          .SetProjection(Projections.Id());

                var userGroupsRolesSubQuery = DetachedCriteria.For<Role>()
                             .CreateAlias("UserGroups", "userGroup", JoinType.LeftOuterJoin)
                             .Add(Subqueries.PropertyIn("userGroup.id", userUserGroupsSubQuery))
                             .SetProjection(Projections.Id());

                var permissionsSubQuery = DetachedCriteria.For<Permission>()
                                .Add(Restrictions.EqProperty("EntityId", permissibleIdPropertyName)).CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(permissibleObjectType))).
                                 Add(Restrictions.Or(Restrictions.Or(
                                          Restrictions.Or(Subqueries.PropertyIn("Role.Id", rolesSubQuery), Subqueries.PropertyIn("Role.Id", userGroupsRolesSubQuery)),
                                          Restrictions.Eq("Role.Id", (Int64)SystemRoles.User)),

                                          !String.IsNullOrEmpty(permissibleOwnerPropertyName) ? Restrictions.And(Restrictions.IsNotNull(permissibleOwnerPropertyName), Restrictions.And(Restrictions.Eq(permissibleOwnerPropertyName, user.PrincipalId), Restrictions.Eq("Role.Id", (Int64)SystemRoles.Owner))) : null

                                          )).Add(

                                          Restrictions.Eq(Projections.SqlProjection(String.Format("Permissions & {0} as result", operationCode), new[] { "result" }, new IType[] { NHibernateUtil.Int32 }), operationCode))
                                .SetProjection(Projections.Id());

               return Subqueries.Exists(permissionsSubQuery);
            }
            else
            {
                var permissionsSubQuery = DetachedCriteria.For<Permission>()
                               .Add(Restrictions.EqProperty("EntityId", permissibleIdPropertyName)).CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(permissibleObjectType))).
                                Add(Restrictions.Eq("Role.Id", (Int64)SystemRoles.Guest)).Add(
                                Restrictions.Eq(Projections.SqlProjection(String.Format("Permissions & {0} as result", operationCode), new[] { "result" }, new IType[] { NHibernateUtil.Int32 }), operationCode))
                               .SetProjection(Projections.Id());

                return Subqueries.Exists(permissionsSubQuery);
            }
        }
    }
}
