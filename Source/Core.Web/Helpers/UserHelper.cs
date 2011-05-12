using System.Linq;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
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
            if (!string.IsNullOrEmpty(model.Password))
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
    }
}