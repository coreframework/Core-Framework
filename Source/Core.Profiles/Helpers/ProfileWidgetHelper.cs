using System;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Profiles.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Mvc.ElementsTypes;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Helpers
{
    public static class ProfileWidgetHelper
    {
        public static ProfileWidgetViewModel BindWidgetModel(ICoreWidgetInstance instance, ICorePrincipal user)
        {
            if (user == null)
                return null;

            var widgetService = ServiceLocator.Current.GetInstance<IProfileWidgetService>();
            var userProfileService = ServiceLocator.Current.GetInstance<IUserProfileService>();
            var userService = ServiceLocator.Current.GetInstance<IUserService>();

            var currentUser = userService.Find(user.PrincipalId);

            if (currentUser == null)
                return null;

            var userProfile = userProfileService.GetUserProfile(user);
            var widget = widgetService.Find(instance.InstanceId ?? 0);

            var model = new ProfileWidgetViewModel().MapFrom(currentUser);
            if (widget != null)
            {
                model.PageWidgetId = widget.Id;
                model.Profile = userProfile;
                model.Widget = widget;
            }

            return model;
        }

        /// <summary>
        /// Saves the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ProfileWidgetEditModel SaveWidget(ProfileWidgetEditModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IProfileWidgetService>();
            var widget = new ProfileWidget();
            if (model.Id > 0)
            {
                widget = widgetService.Find(model.Id);
            }

            var viewModel = model.MapTo(widget);

            if (widget != null)
            {
                widgetService.Save(viewModel);
            }

            return new ProfileWidgetEditModel().MapFrom(viewModel);
        }

        /// <summary>
        /// Validates the user profile.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="modelState">State of the model.</param>
        /// <param name="user">The user.</param>
        /// <param name="userProfile">The user profile.</param>
        public static void Validate(FormCollection collection, ModelStateDictionary modelState, UserProfile userProfile)
        {
            if (userProfile == null) return;

            foreach (var item in userProfile.ProfileType.ProfileHeaders)
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
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="userProfile">The user profile.</param>
        /// <param name="currentUser">The current user.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static bool SaveUser(ProfileWidgetViewModel model, FormCollection collection, UserProfile userProfile, ICorePrincipal currentUser, out User user)
        {
            user = null;

            if (currentUser == null)
                return false;

            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            user = userService.Find(currentUser.PrincipalId);
            if (user == null)
                return false;

            model.MapTo(user);
            userService.SetPassword(user, model.Password);

            var isSuccess = userService.Save(user);

            if (isSuccess)
            {
                if (userProfile != null)
                {
                    foreach (var item in userProfile.ProfileType.ProfileHeaders)
                    {
                        foreach (var element in item.ProfileElements)
                        {
                            var elementName = String.Format("{0}_{1}", (ElementType)element.Type, element.Id);
                            var value = collection[elementName];

                            var existingValue = userProfile.ProfileElements.FirstOrDefault(el=>el.ProfileElement.Id == element.Id);

                            if (existingValue !=null)
                            {
                                existingValue.Value = value;
                            }
                            else
                            {
                                userProfile.ProfileElements.Add(new UserProfileElement
                                {
                                    UserProfile = userProfile,
                                    ProfileElement = element,
                                    Value = value
                                });
                            }
                        }
                    }
                    var userProfileService = ServiceLocator.Current.GetInstance<IUserProfileService>();
                    isSuccess = userProfileService.Save(userProfile);
                }
            }

            return isSuccess;
        }
    }
}