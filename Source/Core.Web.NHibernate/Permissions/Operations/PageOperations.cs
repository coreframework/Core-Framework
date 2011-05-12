using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Web.NHibernate.Permissions.Operations
{
    public enum PageOperations
    {
        [OperationDescription(PermissionArea.Applications,PermissionOperationLevel.Object, GuestDefaultAcess = true, OwnerDefaultAcess = true, UserDefaultAccess = true)]
        View = 1,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Delete = 2,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Update = 4,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Permissions = 8,

        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Type)]
        AddNewPages = 16
    }
}
