using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Plugins.Plugins
{
    public enum BasePluginOperations
    {
        [OperationDescription(PermissionArea.Modules, PermissionOperationLevel.Type)]
        Manage = 1,

        [OperationDescription(PermissionArea.Modules, PermissionOperationLevel.Type)]
        ShowInControlPanel = 2
    }
}
