using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Core.Framework.Plugins.Web;
using Microsoft.Practices.ServiceLocation;

namespace Core.ContentPages.Helpers
{
    public class ContentViewerWidgetHelper
    {
        #region Methods

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static ContentPageWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }

        public static ContentPageWidget BindEditWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }

        /// <summary>
        /// Saves the content viewer widget.
        /// </summary>
        /// <param name="model">The model.</param>
        public static ContentViewerWidgetModel SaveContentViewerWidget(ContentViewerWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();
            var contentViewer = model.MapTo(new ContentPageWidget());
            widgetService.Save(contentViewer);
            return new ContentViewerWidgetModel().MapFrom(contentViewer);
        }

        #endregion
    }
}