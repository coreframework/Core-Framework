using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.ContentPages.Permissions.Operations
{
    [Flags]
    public enum ContentPagePluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage content pages")]
        ManageContentPages = 1
    }
}