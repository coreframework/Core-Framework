using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.WebContent.Permissions.Operations
{
    [Flags]
    public enum WebContentPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage sections")]
        ManageSections = 1
    }
}