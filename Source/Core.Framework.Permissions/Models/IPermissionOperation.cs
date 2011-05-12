using System;

namespace Core.Framework.Permissions.Models
{
    public interface IPermissionOperation
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        Int32 Key { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        PermissionArea Area { get; set; }

        /// <summary>
        /// Gets or sets the operation level.
        /// </summary>
        /// <value>The operation level.</value>
        PermissionOperationLevel OperationLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user default access].
        /// </summary>
        /// <value><c>true</c> if [user default access]; otherwise, <c>false</c>.</value>
        bool UserDefaultAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [owner default acess].
        /// </summary>
        /// <value><c>true</c> if [owner default acess]; otherwise, <c>false</c>.</value>
        bool OwnerDefaultAcess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [guest default acess].
        /// </summary>
        /// <value><c>true</c> if [guest default acess]; otherwise, <c>false</c>.</value>
        bool GuestDefaultAcess { get; set; }
    }
}
