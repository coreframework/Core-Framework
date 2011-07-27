using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.News.Permissions.Operations
{
    public enum NewsPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage news")]
        ManageNews = 1
    }
}