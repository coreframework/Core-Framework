using System;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Helpers
{
    public interface ICoreWidgetInstanceBuilder
    {
        ICoreWidgetInstance Build(long? instanceId, String widgetIdentifier, IPageSettings pageSettings,
                                  long? pageWidgetId);
    }
}
