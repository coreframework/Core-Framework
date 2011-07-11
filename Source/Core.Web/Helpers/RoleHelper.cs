using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Framework.Core.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Provides helper methods for <see cref="Role"/> managing.
    /// </summary>
    public static class RoleHelper
    {
        /// <summary>
        /// Builds the assignment model.
        /// </summary>
        /// <param name="role">The role for assignment.</param>
        /// <returns>User to role assignment model.</returns>
        public static UserToRoleAssignmentModel BuildRoleToUsersAssignmentModel(Role role)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var allUsers = userService.GetAll();

            return new UserToRoleAssignmentModel
            {
                Role = new RoleViewModel().MapFrom(role),
                Users = allUsers.Select(user => BuildUserAssignmentModel(role, user)).ToArray()
            };
        }

        /// <summary>
        /// Builds the user assignment model.
        /// </summary>
        /// <param name="role">The role for assignment.</param>
        /// <param name="user">The user for binding.</param>
        /// <returns>User assignment model.</returns>
        public static AssignedUserModel BuildUserAssignmentModel(Role role, User user)
        {
            return new AssignedUserModel
            {
                Id = user.Id,
                Name = user.Username,
                Assigned = role.Users.Contains(user)
            };
        }

        /// <summary>
        /// Updates the role to users assignment.
        /// </summary>
        /// <param name="role">The role to update.</param>
        /// <param name="model">The assignment model.</param>
        /// <returns>
        ///     <c>true</c> if reassignment is OK; <c>false</c> otherwise.
        /// </returns>
        public static bool UpdateRoleToUsersAssignment(Role role, UserToRoleAssignmentModel model)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            role.Users.Clear();
            foreach (var user in model.Users)
            {
                if (user.Assigned)
                {
                    role.Users.Add(userService.Find(user.Id));
                }
            }

            return roleService.Save(role);
        }

        public static bool UpdateRoleToUsersAssignment(Role role, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = role.Users.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var user in noselected)
            {
                role.Users.Remove(user);
            }

            foreach (var selid in selids)
            {
                string selid1 = selid;
                if (!role.Users.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        role.Users.Add(userService.Find(selectedID));
                    }
                }
            }

            return roleService.Save(role);
        }

        /// <summary>
        /// Builds the assignment model.
        /// </summary>
        /// <param name="role">The role for assignment.</param>
        /// <returns>User to role assignment model.</returns>
        public static UserGroupToRoleAssignmentModel BuildRoleToUserGroupsAssignmentModel(Role role)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var allUserGroups = userGroupService.GetAll();

            return new UserGroupToRoleAssignmentModel
            {
                Role = new RoleViewModel().MapFrom(role),
                UserGroups = allUserGroups.Select(userGroup => BuildUserGroupAssignmentModel(role, userGroup)).ToArray()
            };
        }

        /// <summary>
        /// Builds the user group assignment model.
        /// </summary>
        /// <param name="role">The role for assignment.</param>
        /// <param name="userGroup">The user group.</param>
        /// <returns>User group assignment model.</returns>
        public static AssignedUserGroupModel BuildUserGroupAssignmentModel(Role role, UserGroup userGroup)
        {
            return new AssignedUserGroupModel
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Assigned = role.UserGroups.Contains(userGroup)
            };
        }

        /// <summary>
        /// Updates the role to user groups assignment.
        /// </summary>
        /// <param name="role">The role to update.</param>
        /// <param name="model">The assignment model.</param>
        /// <returns>
        ///     <c>true</c> if reassignment is OK; <c>false</c> otherwise.
        /// </returns>
        public static bool UpdateRoleToUserGroupsAssignment(Role role, UserGroupToRoleAssignmentModel model)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();

            role.Users.Clear();
            foreach (var userGroup in model.UserGroups)
            {
                if (userGroup.Assigned)
                {
                    role.UserGroups.Add(userGroupService.Find(userGroup.Id));
                }
            }

            return roleService.Save(role);
        }

        public static bool UpdateRoleToUserGroupsAssignment(Role role, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = role.UserGroups.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var userGroup in noselected)
            {
                role.UserGroups.Remove(userGroup);
            }

            foreach (var selid in selids)
            {
                string selid1 = selid;
                if (!role.UserGroups.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        role.UserGroups.Add(userGroupService.Find(selectedID));
                    }
                }
            }

            return roleService.Save(role);
        }

        /// <summary>
        /// Binds the role permission model.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public static RolePermissionsModel BindRolePermissionModel(long roleId, String resource)
        {
            var objectTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();
            IEnumerable<EntityType> items = objectTypeService.GetAll();

            PermissionOperationsModel operationsModel = null;

            long resourceId = 0;
            int areaId = 0;
            PermissionArea area = PermissionArea.Portal;

            if (!String.IsNullOrEmpty(resource))
            {
                //try parse resource
                var parts = resource.Trim().Split('_');
                if (parts.Length==2)
                {
                    Int64.TryParse(parts[0], out resourceId);
                    Int32.TryParse(parts[1], out areaId);
                }

                if (resourceId>0 && areaId>0)
                {
                    if (Enum.TryParse(areaId.ToString(),out area))
                    {
                        var permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();
                        var permissions  = permissionService.GetPermission(roleId, (long) resourceId, null);

                        var currentResource = items.FirstOrDefault(item => item.Id == resourceId);

                        if (currentResource != null)
                        {
                            operationsModel = new PermissionOperationsModel
                                                  {
                                                      Permissions = permissions,
                                                      ResourceId = resourceId,
                                                      RoleId = roleId,
                                                      Area = area,
                                                      Operations = GetResourceOperations(currentResource,area)
                                                  };
                        }
                    }
                }
            }

            return new RolePermissionsModel
                       {
                           RoleId = roleId,
                           ResourceId = resourceId,
                           Area = area,
                           PermissibleObjects = BindRolePermissionItems(items),
                           OperationsModel = operationsModel
                       };
        }

        /// <summary>
        /// Binds the role permission items.
        /// </summary>
        /// <param name="permissionResources">The permission resources.</param>
        /// <returns></returns>
        private static List<RolePermissionsItem> BindRolePermissionItems(IEnumerable<EntityType> permissionResources)
        {
            var resultList = new List<RolePermissionsItem>();
            foreach (var resource in permissionResources)
            {
                EntityType res = resource;
                var item = MvcApplication.PermissibleObjects.FirstOrDefault(
                    perm =>
                    PermissionsHelper.GetEntityType(perm.GetType()) ==
                    res.Name);

                if (item != null)
                {
                    resultList.AddRange(item.Operations.Where(operation => operation.OperationLevel != PermissionOperationLevel.Object).GroupBy(operation => operation.Area).Select(
                        area =>
                        new RolePermissionsItem { Id = res.Id, Title = item.PermissionTitle, Area = area.Key }));
                }
            }

            return resultList;
        }

        /// <summary>
        /// Gets the resource operations.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="area">The area.</param>
        /// <returns></returns>
        public static IEnumerable<IPermissionOperation> GetResourceOperations(EntityType resource, PermissionArea area)
        {
            var permissibleObject = MvcApplication.PermissibleObjects.FirstOrDefault(
                perm =>
                PermissionsHelper.GetEntityType(perm.GetType()) ==
                resource.Name);
            if (permissibleObject!=null)
            {
                return permissibleObject.Operations.Where(operation=>operation.Area==area && operation.OperationLevel!=PermissionOperationLevel.Object);
            }
            return null;
        }

        /// <summary>
        /// Applies the role permissions.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void ApplyRolePermissions(PermissionOperationsModel model)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();
            var permissions = permissionService.GetPermission(model.RoleId, model.ResourceId, null);

            if (permissions==null)
            {
                permissions = new Permission
                                  {
                                      Role =
                                          {
                                              Id = model.RoleId
                                          },
                                      EntityType =
                                          {
                                              Id = model.ResourceId
                                          },
                                  };
                if (model.OperationIds!=null)
                {
                    foreach (var operation in model.OperationIds)
                    {
                        permissions.Permissions = (permissions.Permissions | operation);
                    }
                }
            }
            else
            {
                var objectTypeService = ServiceLocator.Current.GetInstance<IEntityTypeService>();
                var resourceType = objectTypeService.Find(model.ResourceId);
                if (resourceType!=null)
                {
                    var operations = GetResourceOperations(resourceType, model.Area);

                    foreach (var operation in operations)
                    {
                         if (model.OperationIds==null || !model.OperationIds.Contains(operation.Key))
                         {
                             permissions.Permissions = (permissions.Permissions & (~ operation.Key));
                         }
                         else
                         {
                             permissions.Permissions = (permissions.Permissions | operation.Key);
                         }
                    }
                }
            }

            permissionService.Save(permissions);
        }
    }
}