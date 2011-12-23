using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Models
{
    public class LoginHolderWidgetViewModel
    {
        public long? PageWidgetId { get; set; }

        public ICoreWidgetInstance FormLoginWidgetInstance { get; set; }

        public ICoreWidgetInstance OpenIdLoginWidgetInstance { get; set; }
    }
}