using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.Forms.Permissions.Operations
{
    public enum FormsPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage forms")]
        ManageForms = 1,

        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage forms answers")]
        ManageFormsAnswers = 2
    }
}