using System;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Contracts
{
    public interface IPermissionsHelper
    {
        PermissionsModel BindPermissionsModel(long entityId, Type entityType, bool includeEntityNull);

        bool ApplyPermissions(PermissionsModel model, Type type);
    }
}
