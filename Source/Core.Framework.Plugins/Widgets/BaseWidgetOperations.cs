﻿using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Plugins.Widgets
{
    public enum BaseWidgetOperations
    {
        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, GuestDefaultAcess = true, OwnerDefaultAcess = true, UserDefaultAccess = true)]
        View = 1,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        Manage = 2,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        AddToPage = 4,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        Permissions = 8,

//        [OperationDescription(PermissionArea.Content, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
//        ShowSomeButton = 16
    }
}
