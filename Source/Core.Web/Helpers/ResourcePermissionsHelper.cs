using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Models;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    public class ResourcePermissionsHelper
    {
        /// <summary>
        /// Binds the page permissions model.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="includeEntityNull"></param>
        /// <returns></returns>
        public static PermissionsModel BindPermissionsModel(long entityId, Type entityType, bool includeEntityNull)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var permissionsService = ServiceLocator.Current.GetInstance<IPermissionService>();

            var permissibleItem = MvcApplication.PermissibleObjects.FirstOrDefault(
                  perm =>
                  perm.GetType() ==
                  entityType);

            if (permissibleItem != null)
            {
                return new PermissionsModel
                    {
                        Roles = roleService.GetAll().Where(role=>role.Id!=(int)SystemRoles.Administrator),
                        EntityId = entityId,
                        Permissions = permissionsService.GetResourcePermissions(entityType, entityId, includeEntityNull),
                        Operations = permissibleItem.Operations.Where(operation => operation.OperationLevel != PermissionOperationLevel.Type).ToList()
                    };
            }

            return null;
        }

        /// <summary>
        /// Saves the resource permissions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="type">The type.</param>
        public static void SaveResourcePermissions(PermissionsModel model, Type type)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var permissionsService = ServiceLocator.Current.GetInstance<IPermissionService>();
            var entityTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();

            var permissibleItem = MvcApplication.PermissibleObjects.FirstOrDefault(
               perm =>
               perm.GetType() ==
               type);
            
            if (permissibleItem != null)
            {
                var operations =
                    permissibleItem.Operations.Where(
                        operation =>
                        operation.OperationLevel != PermissionOperationLevel.Type &&
                        operation.Area == PermissionArea.Applications).ToList();

                var roles = roleService.GetAll();
                var permissions = permissionsService.GetResourcePermissions(type, model.EntityId, false);
                var entityType = entityTypeService.GetByType(type);

                foreach (var role in roles)
                {
                    var rolePermission = permissions.Where(permission => permission.Role == role).FirstOrDefault();
                    if (rolePermission == null)
                    {
                        rolePermission = new Permission
                                             {
                                                 Role = role,
                                                 EntityId = model.EntityId,
                                                 Scope = PermissionScope.Role,
                                                 EntityType = entityType,
                                             };
                    }

                    foreach (var operation in operations)
                    {
                        if (model.Actions == null || !model.Actions.Contains(String.Format("{0}_{1}", role.Id, operation.Key)))
                        {
                            rolePermission.Permissions = (rolePermission.Permissions & (~operation.Key));
                        }
                        else
                        {
                            rolePermission.Permissions = (rolePermission.Permissions | operation.Key);
                        }
                    }
                    permissionsService.Save(rolePermission);
                }
            }
        }

        /// <summary>
        /// Gets the resource operations.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public static IEnumerable<IPermissionOperation> GetResourceOperations(Type resourceType)
        {
            var permissibleType = MvcApplication.PermissibleObjects.Find(item => item.GetType() == resourceType);

            if (permissibleType != null)
                return permissibleType.Operations;

            return null;
        }
    }
}