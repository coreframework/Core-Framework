using System.Linq;
using Core.Web.Areas.Admin.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
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
    }
}