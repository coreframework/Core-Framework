using System;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OperationDescriptionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <param name="level">The level.</param>
        public OperationDescriptionAttribute(PermissionArea area, PermissionOperationLevel level)
        {
            Area = area;
            OperationLevel = level;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [user default access].
        /// </summary>
        /// <value><c>true</c> if [user default access]; otherwise, <c>false</c>.</value>
        public bool UserDefaultAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [owner default acess].
        /// </summary>
        /// <value><c>true</c> if [owner default acess]; otherwise, <c>false</c>.</value>
        public bool OwnerDefaultAcess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [guest default acess].
        /// </summary>
        /// <value><c>true</c> if [guest default acess]; otherwise, <c>false</c>.</value>
        public bool GuestDefaultAcess { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        public PermissionArea Area { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>The type of the operation.</value>
        public PermissionOperationLevel OperationLevel { get; set; }

        #endregion
    }
}
