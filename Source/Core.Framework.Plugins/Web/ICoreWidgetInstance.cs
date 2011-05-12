using System;

namespace Core.Framework.Plugins.Web
{
    public interface ICoreWidgetInstance
    {
        long? InstanceId { get; set; }

        String WidgetIdentifier { get; set; }

        IPageSettings PageSettings { get; set; }
    }
}
