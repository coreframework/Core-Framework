using Core.Framework.Plugins.Helpers;
using Core.Framework.Plugins.Web;
using Core.Web.Models;

namespace Core.Web.Helpers
{
    public class CoreWidgetInstanceBuilder : ICoreWidgetInstanceBuilder
    {
        public ICoreWidgetInstance Build(long? instanceId, string widgetIdentifier, IPageSettings pageSettings, long? pageWidgetId)
        {
            return new CoreWidgetInstance
                       {
                           InstanceId = instanceId,
                           WidgetIdentifier = widgetIdentifier,
                           PageSettings = pageSettings,
                           PageWidgetId = pageWidgetId
                       };
        }
    }
}