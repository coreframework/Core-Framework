using System;
using Core.Framework.Permissions.Helpers;

namespace Core.Framework.Permissions.Models
{
    [Flags]
    public enum BaseEntityOperations
    {
        [OperationDescription(PermissionArea.ControlPanel,PermissionOperationLevel.Type)]
        Manage = 1
    }
}
