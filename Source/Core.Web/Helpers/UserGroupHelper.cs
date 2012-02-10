using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Web.Areas.Admin.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Provides helper methods for <see cref="UserGroup"/> managing.
    /// </summary>
    public static class UserGroupHelper
    {
        /// <summary>
        /// Builds the assignment model.
        /// </summary>
        /// <param name="userGroup">The user group for assignment.</param>
        /// <returns>User to user group assignment model.</returns>
        public static UserToUserGroupAssignmentModel BuildAssignmentModel(UserGroup userGroup)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var allUsers = userService.GetAll();

            return new UserToUserGroupAssignmentModel
            {
                UserGroup = new UserGroupViewModel().MapFrom(userGroup),
                Users = allUsers.Select(user => BuildUserAssignmentModel(userGroup, user)).ToArray()
            };
        }

        /// <summary>
        /// Builds the user assignment model.
        /// </summary>
        /// <param name="userGroup">The user group for assignment.</param>
        /// <param name="user">The user for binding.</param>
        /// <returns>User assignment model.</returns>
        public static AssignedUserModel BuildUserAssignmentModel(UserGroup userGroup, User user)
        {
            return new AssignedUserModel
            {
                Id = user.Id,
                Name = user.Username,
                Assigned = userGroup.Users.Contains(user)
            };
        }

        /// <summary>
        /// Updates the user group to users assignment.
        /// </summary>
        /// <param name="userGroup">The user group to update.</param>
        /// <param name="model">The assignment model.</param>
        /// <returns>
        ///     <c>true</c> if reassignment is OK; <c>false</c> otherwise.
        /// </returns>
        public static bool UpdateUserGroupToUsersAssignment(UserGroup userGroup, UserToUserGroupAssignmentModel model)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            userGroup.Users.Clear();
            foreach (var user in model.Users)
            {
                if (user.Assigned)
                {
                    userGroup.Users.Add(userService.Find(user.Id));
                }
            }

            return userGroupService.Save(userGroup);
        }

        /// <summary>
        /// Updates the role to users assignment.
        /// </summary>
        /// <param name="userGroup">The user.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="selids">The selected ids.</param>
        /// <returns></returns>
        public static bool UpdateUserGroupToUsersAssignment(UserGroup userGroup, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = userGroup.Users.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var user in noselected)
            {
                userGroup.Users.Remove(user);
            }

            foreach (var selid in selids)
            {
                String selid1 = selid;
                if (!userGroup.Users.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        userGroup.Users.Add(userService.Find(selectedID));
                    }
                }
            }

            return userGroupService.Save(userGroup);
        }

        public static bool UpdateUserGroupToRolesAssignment(UserGroup userGroup, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = userGroup.Roles.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var role in noselected)
            {
                userGroup.Roles.Remove(role);
            }

            foreach (var selid in selids)
            {
                String selid1 = selid;
                if (!userGroup.Roles.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        userGroup.Roles.Add(roleService.Find(selectedID));
                    }
                }
            }

            return userGroupService.Save(userGroup);
        }
    }
}