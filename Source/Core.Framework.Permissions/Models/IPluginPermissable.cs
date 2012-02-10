using System;

namespace Core.Framework.Permissions.Models
{
    /// <summary>
    /// Specifies entities applicable for plugin permissions.
    /// </summary>
    public interface IPluginPermissable : IPermissible
    {
        String PluginIdentifier { get; }

        PluginPermissionLevel PluginPermissionLevel { get; }
    }
}