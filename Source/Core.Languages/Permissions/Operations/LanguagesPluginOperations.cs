using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Languages.Permissions.Operations
{
    public enum LanguagesPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage languages")]
        ManageLanguages = 1
    }
}