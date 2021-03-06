﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Web.Areas.Admin.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    /// <summary>
    /// Provides helper methods for <see cref="User"/> managing.
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Created user.</returns>
        public static User Save(UserViewModel model)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var user = new User();
            model.MapTo(user);
            userService.SetPassword(user, model.Password);
            userService.Save(user);
            return user;
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="model">The model.</param>
        /// <returns>Updated user.</returns>
        public static User Update(User user, UserViewModel model)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            model.MapTo(user);
            if (!String.IsNullOrEmpty(model.Password))
            {
                userService.SetPassword(user, model.Password);
            }
            userService.Save(user);
            return user;
        }

        /// <summary>
        /// Builds the assignment model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static UserGroupToUserAssignmentModel BuildAssignmentModel(User user)
        {
            IUserGroupService userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var allUserGroups = userGroupService.GetAll();

            return new UserGroupToUserAssignmentModel
            {
                User = new UserViewModel().MapFrom(user),
                UserGroups = allUserGroups.Select(userGroup => BuildRoleAssignmentModel(user, userGroup)).ToArray()
            };
        }

        /// <summary>
        /// Builds the role assignment model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userGroup">The user group.</param>
        /// <returns></returns>
        public static AssignedUserGroupModel BuildRoleAssignmentModel(User user, UserGroup userGroup)
        {
            return new AssignedUserGroupModel
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Assigned = user.UserGroups.Contains(userGroup)
            };
        }

        /// <summary>
        /// Updates the role to users assignment.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static bool UpdateUserGroupToUsersAssignment(User user, UserGroupToUserAssignmentModel model)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            user.UserGroups.Clear();
            foreach (var userGroup in model.UserGroups)
            {
                if (userGroup.Assigned)
                {
                    user.UserGroups.Add(userGroupService.Find(userGroup.Id));
                }
            }

            return userService.Save(user);
        }
        /// <summary>
        /// Updates the role to users assignment.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="selids">The selected ids.</param>
        /// <returns></returns>
        public static bool UpdateUserGroupToUsersAssignment(User user, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = user.UserGroups.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var userGroup in noselected)
            {
                user.UserGroups.Remove(userGroup);
            }

            foreach (var selid in selids)
            {
                String selid1 = selid;
                if (!user.UserGroups.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        user.UserGroups.Add(userGroupService.Find(selectedID));
                    }
                }
            }

            return userService.Save(user);
        }

        public static bool UpdatRoleToUsersAssignment(User user, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var roleService = ServiceLocator.Current.GetInstance<IRoleService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = user.Roles.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var role in noselected)
            {
                user.Roles.Remove(role);
            }

            foreach (var selid in selids)
            {
                String selid1 = selid;
                if (!user.Roles.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        user.Roles.Add(roleService.Find(selectedID));
                    }
                }
            }

            return userService.Save(user);
        }
    }
}