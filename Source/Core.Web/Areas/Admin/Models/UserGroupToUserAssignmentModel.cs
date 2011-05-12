using System;

namespace Core.Web.Areas.Admin.Models
{
    public class UserGroupToUserAssignmentModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user for assignment.</value>
        public UserViewModel User { get; set; }

        /// <summary>
        /// Gets or sets the user groups.
        /// </summary>
        /// <value>The user groups.</value>
        public AssignedUserGroupModel[] UserGroups { get; set; }

        #region Object members

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            if (User != null)
            {
                return User.ToString();
            }

            return base.ToString();
        }

        #endregion
    }
}