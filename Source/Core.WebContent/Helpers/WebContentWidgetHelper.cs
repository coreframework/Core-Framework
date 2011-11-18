using Core.Framework.Plugins.Web;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Helpers
{
    public static class WebContentWidgetHelper
    {
        #region Helper Methods

        public static WebContentWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentWidgetService>();
            return widgetService.Find(instance.InstanceId ?? 0);
        }

        public static WebContentWidgetViewModel SaveWidget(WebContentWidgetViewModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWebContentWidgetService>();
            var widget = new WebContentWidget();
            if (model.Id > 0)
            {
                widget = widgetService.Find(model.Id);
            }

            var viewModel = model.MapTo(widget);

            if (widget != null)
            {
                widgetService.Save(viewModel);
            }

            return new WebContentWidgetViewModel().MapFrom(viewModel);
        }

        #endregion
    }
}