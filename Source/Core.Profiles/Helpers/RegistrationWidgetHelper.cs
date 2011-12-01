using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Helpers
{
    public static class RegistrationWidgetHelper
    {
        /// <summary>
        /// Saves the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static RegistrationWidgetEditModel SaveWidget(RegistrationWidgetEditModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IRegistrationWidgetService>();
            var widget = new RegistrationWidget();
            if (model.Id > 0)
            {
                widget = widgetService.Find(model.Id);
            }

            var viewModel = model.MapTo(widget);

            if (widget != null)
            {
                widgetService.Save(viewModel);
            }

            return new RegistrationWidgetEditModel().MapFrom(viewModel);
        }
    }
}