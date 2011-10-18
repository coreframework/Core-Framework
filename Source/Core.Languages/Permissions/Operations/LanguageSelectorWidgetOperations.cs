using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Languages.Permissions.Operations
{
    [Flags]
    public enum LanguageSelectorWidgetOperations
    {
        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, GuestDefaultAcess = true, OwnerDefaultAcess = true, UserDefaultAccess = true)]
        View = 1,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        Manage = 2,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        [Description("Add to page")]
        AddToPage = 4,

        [OperationDescription(PermissionArea.Applications, PermissionOperationLevel.ObjectType, OwnerDefaultAcess = true)]
        Permissions = 8
    }
}