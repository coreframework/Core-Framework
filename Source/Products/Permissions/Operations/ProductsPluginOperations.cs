using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Products.Permissions.Operations
{
    [Flags]
    public enum ProductsPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage products")]
        ManageProducts = 1,

        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage category")]
        ManageCategory = 2
    }
}