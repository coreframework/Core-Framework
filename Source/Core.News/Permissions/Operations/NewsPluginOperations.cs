using System;
using System.ComponentModel;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;

namespace Core.News.Permissions.Operations
{
    [Flags]
    public enum NewsPluginOperations
    {
        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage news")]
        ManageNews = 1,

        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Manage Categories")]
        ManageCategories = 2,

        [OperationDescription(PermissionArea.ControlPanel, PermissionOperationLevel.Type)]
        [Description("Publishing News")]
        PublishingNews = 4
    }
}