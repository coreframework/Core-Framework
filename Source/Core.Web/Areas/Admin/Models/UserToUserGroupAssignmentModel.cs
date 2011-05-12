using System;

namespace Core.Web.Areas.Admin.Models
{
    /// <summary>
    /// View model for users to user group assignment.
    /// </summary>
    public class UserToUserGroupAssignmentModel
    {
        /// <summary>
        /// Gets or sets the user group.
        /// </summary>
        /// <value>The user group for assignment.</value>
        public UserGroupViewModel UserGroup { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public AssignedUserModel[] Users { get; set; }

        #region Object members

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            if (UserGroup != null)
            {
                return UserGroup.ToString();
            }

            return base.ToString();
        }

        #endregion
    }
}