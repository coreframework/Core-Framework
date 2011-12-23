using System;
using Core.Framework.Permissions.Models;

namespace Core.Framework.MEF.Web
{
    public interface IWidgetHelper
    {
        bool IsWidgetEnabled(String pluginIdentified);

        void UpdatePageWidgetInstance(long pageWidgetId, long instanceId, ICorePrincipal user);
    }
}
