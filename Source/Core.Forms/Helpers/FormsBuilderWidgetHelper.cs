using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Framework.Plugins.Web;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Helpers
{
    public class FormsBuilderWidgetHelper
    {
        /// <summary>
        /// Saves the form builder widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static FormBuilderWidgetViewModel SaveFormBuilderWidget(FormBuilderWidgetViewModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
            var contentViewer = model.MapTo(new FormBuilderWidget());
            widgetService.Save(contentViewer);
            return new FormBuilderWidgetViewModel().MapFrom(contentViewer);
        }

        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static FormBuilderWidget BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();

            return widgetService.Find(instance.InstanceId ?? 0);
        }
    }
}