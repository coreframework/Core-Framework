using System;

namespace Core.Web.Areas.Admin.Models
{
    /// <summary>
    /// View model for users assignment.
    /// </summary>
    public class AssignedUserModel
    {
        /// <summary>
        /// Gets or sets the id of user.
        /// </summary>
        /// <value>The user id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is assigned.
        /// </summary>
        /// <value><c>true</c> if assigned; otherwise, <c>false</c>.</value>
        public bool Assigned { get; set; }

        /// <summary>
        /// Gets or sets the name of user.
        /// </summary>
        /// <value>The user name.</value>
        public String Name { get; set; }
    }
}