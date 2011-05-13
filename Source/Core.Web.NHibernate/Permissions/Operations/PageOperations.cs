using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Web.NHibernate.Permissions.Operations
{
    /// <summary>
    /// Defines page permission operations
    /// </summary>
    public enum PageOperations
    {
        /// <summary>
        /// 
        /// </summary>
        [OperationDescription(PermissionArea.Applications,PermissionOperationLevel.Object, GuestDefaultAcess = true, OwnerDefaultAcess = true, UserDefaultAccess = true)]
        View = 1,

        /// <summary>
        /// 
        /// </summary>
        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Delete = 2,

        /// <summary>
        /// 
        /// </summary>
        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Update = 4,

        /// <summary>
        /// 
        /// </summary>
        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Permissions = 8,

        /// <summary>
        /// 
        /// </summary>
        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Type)]
        [Description("Add new pages")]
        AddNewPages = 16
    }
}
