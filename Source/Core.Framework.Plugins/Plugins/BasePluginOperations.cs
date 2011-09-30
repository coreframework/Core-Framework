using System;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Plugins.Plugins
{
    [Flags]
    public enum BasePluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        Manage = 1,

        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        ShowInControlPanel = 2
    }
}
