using System;

namespace Core.Web.Areas.Admin.Models
{
    /// <summary>
    /// View model for users to role assignment.
    /// </summary>
    public class UserToRoleAssignmentModel
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role for assignment.</value>
        public RoleViewModel Role { get; set; }

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
            if (Role != null)
            {
                return Role.ToString();
            }

            return base.ToString();
        }

        #endregion
    }
}