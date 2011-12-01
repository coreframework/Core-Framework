using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Profiles.Permissions.Operations
{
    [Flags]
    public enum ProfilesPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage profile types")]
        ManageProfileTypes = 1,
    }
}