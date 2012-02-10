using System;
using System.Web.Mvc;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Plugins.Web;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Mvc.ElementsTypes;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Helpers
{
    public static class RegistrationWidgetHelper
    {
        public static RegistrationWidgetViewModel BindWidgetModel(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IRegistrationWidgetService>();
            var widget = widgetService.Find(instance.InstanceId ?? 0);
            RegistrationWidgetViewModel model = null;
            if (widget != null)
            {
                model = new RegistrationWidgetViewModel
                            {
                                PageWidgetId = widget.Id, 
                                ProfileTypeId = widget.ProfileType.Id,
                                Widget = widget
                            };
            }

            return model;
        }

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

        public static void Validate(RegistrationWidget model, FormCollection collection, ModelStateDictionary modelState)
        {
            foreach (var item in model.ProfileType.ProfileHeaders)
            {
                foreach (var element in item.ProfileElements)
                {
                    var elementName = String.Format("{0}_{1}", (ElementType)element.Type, element.Id);
                    var value = collection[elementName];
                    if (value == null) continue;

                    // check if the item is required
                    if (element.IsRequired && String.IsNullOrEmpty(value))
                    {
                        modelState.AddModelError(elementName, String.Format("The {0} field is required.", element.Title));
                    }
                    else
                    {
                        ElementTypeUtility.ValidateElement((ElementType)element.Type, elementName, value, modelState);
                    }
                }
            }
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static bool RegisterUser(RegistrationWidget widget, RegistrationWidgetViewModel model, FormCollection collection, out User user)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var userProfileService = ServiceLocator.Current.GetInstance<IUserProfileService>();
            
            user = new User();
            model.MapTo(user);
            userService.SetPassword(user, model.Password);

            var isSuccess = userService.Save(user);

            if (isSuccess)
            {
                var profile = new UserProfile
                                  {
                                      User = user,
                                      ProfileType = widget.ProfileType
                                  };

                foreach (var item in widget.ProfileType.ProfileHeaders)
                {
                    foreach (var element in item.ProfileElements)
                    {
                        var elementName = String.Format("{0}_{1}", (ElementType)element.Type, element.Id);
                        var value = collection[elementName];
                        
                        if (value == null) continue;

                        profile.AddProfileElement(new UserProfileElement
                        {
                            UserProfile = profile,
                            ProfileElement = element,
                            Value = value
                        });
                    }
                }

                isSuccess = userProfileService.Save(profile);
            }

            return isSuccess;
        }
    }
}