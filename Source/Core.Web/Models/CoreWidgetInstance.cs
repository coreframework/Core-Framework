using Core.Framework.Plugins.Web;

namespace Core.Web.Models
{
    public class CoreWidgetInstance: ICoreWidgetInstance
    {
        public long? InstanceId { get; set; }

        public string WidgetIdentifier { get; set; }

        public IPageSettings PageSettings { get; set; }
    }
}