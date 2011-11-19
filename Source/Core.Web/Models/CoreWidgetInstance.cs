using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Models
{
    public class CoreWidgetInstance: ICoreWidgetInstance
    {
        public long? InstanceId { get; set; }

        public String WidgetIdentifier { get; set; }

        public IPageSettings PageSettings { get; set; }

        public long? PageWidgetId { get; set; }
    }
}