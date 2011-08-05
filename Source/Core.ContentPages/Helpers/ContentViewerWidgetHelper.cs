using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Core.Framework.Plugins.Web;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

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

        public static long? CloneContentPageWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IContentPageWidgetService>();
            var widget = BindWidgetModel(instance);

            if (widget!=null)
            {
                var clone = (ContentPageWidget)new ContentPageWidget().InjectFrom<CloneEntityInjection>(widget);

                if (widgetService.Save(clone))
                {
                    return clone.Id;
                }
            }
            return null;
        }

        #endregion
    }
}