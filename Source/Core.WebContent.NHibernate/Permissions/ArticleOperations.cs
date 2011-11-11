using System;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.WebContent.NHibernate.Permissions
{
    [Flags]
    public enum ArticleOperations
    {
        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        View = 1,

        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Manage = 2,

        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Object, UserDefaultAccess = true, GuestDefaultAcess = true, OwnerDefaultAcess = true)]
        AddToWidget = 4,

        [OperationDescription(PermissionArea.Portal, PermissionOperationLevel.Object, OwnerDefaultAcess = true)]
        Permissions = 8
    }
}