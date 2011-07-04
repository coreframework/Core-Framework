using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.Validation;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Framework.MVC.Captcha;
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

        /// <summary>
        /// Validates the form model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="modelState">State of the model.</param>
        public static void Validate(FormBuilderWidget model, FormCollection collection, ModelStateDictionary modelState)
        {
            foreach (var item in model.Form.FormElements)
            {
                var elementName = String.Format("{0}_{1}", item.Type, item.Id);
                var value = collection[elementName];
                if (value != null)
                {
                    // check if the item is required
                    if (item.IsRequired && String.IsNullOrEmpty(value))
                    {
                        modelState.AddModelError(elementName, String.Format("The {0} field is required.", item.Title));
                    }
                    // check if the item has max length limitation
                    else if (!String.IsNullOrEmpty(value) && item.MaxLength!=null)
                    {
                        if (value.Length > item.MaxLength)
                        {
                            modelState.AddModelError(elementName, String.Format("The {0} field length is more then {1}.", item.Title, item.MaxLength));
                        }
                    }
                    // check if the item has regular expression validator
                    if (!String.IsNullOrEmpty(value) && item.RegexTemplate != RegexTemplate.None && RegexTemplatesConfig.ValidationTemplates.ContainsKey(item.RegexTemplate))
                    {
                        if (!Regex.IsMatch(value, RegexTemplatesConfig.ValidationTemplates[item.RegexTemplate]))
                        {
                            modelState.AddModelError(elementName, String.Format("The {0} field is not in valid format.", item.Title));
                        }
                    }
                    // validate if the item is Captcha
                    if (item.Type == FormElementType.Captcha)
                    {
                        ValidateCaptcha(collection, modelState, elementName, value);
                    }
                }
            }
        }

        /// <summary>
        /// Validates the captcha.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="modelState">State of the model.</param>
        /// <param name="captchaKey">The captcha key.</param>
        /// <param name="captchaValue">The captcha value.</param>
        private static void ValidateCaptcha(FormCollection collection, ModelStateDictionary modelState, String captchaKey, String captchaValue)
        {
            bool isValid = true;

            // get the guid from form collection
            string guid =  collection[CaptchaImage.CaptchaImageGuidKey];

            // check for the guid because it is required from the rest of the opperation
            if (String.IsNullOrEmpty(guid))
            {
                isValid = false;
            }
            else
            {
                // get values
                CaptchaImage image = CaptchaImage.GetCachedCaptcha(guid);
                string expectedValue = image == null ? String.Empty : image.Text;

                // removes the captch from cache so it cannot be used again
                HttpContext.Current.Cache.Remove(guid);

                // validate the captch
                if (String.IsNullOrEmpty(captchaValue) || String.IsNullOrEmpty(expectedValue) || !String.Equals(captchaValue, expectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                modelState.AddModelError(captchaKey, @"The code you typed does not match the code the image.");
            }
        }

        public static bool HandleFormData(FormBuilderWidget model, FormCollection collection, ICorePrincipal user)
        {
            if (model.SaveData)
            {
                var widgetAnswersService = ServiceLocator.Current.GetInstance<IFormWidgetAnswerService>();
                var widgetAnswersValueService = ServiceLocator.Current.GetInstance<IFormWidgetAnswerValueService>();
                var answer = new FormWidgetAnswer
                                 {
                                     CreateDate = DateTime.Now,
                                     FormBuilderWidget = model,
                                     UserId = user.PrincipalId,
                                     AnswerValues = new List<FormWidgetAnswerValue>(),
                                     Title = model.Title
                                 };

                if (widgetAnswersService.Save(answer))
                {
                    //save answer values
                    foreach (FormElement item in model.Form.FormElements)
                    {
                        string elementName = String.Format("{0}_{1}", item.Type, item.Id);
                        string value = collection[elementName];
                        if (value != null)
                        {
                            widgetAnswersValueService.Save(new FormWidgetAnswerValue
                            {
                                Field = item.Title,
                                Value = value,
                                Answer = answer
                            });
                        }
                    }
                }  
            }

            if (model.SendEmail)
            {
                FormsMailer.SendFormAnswerEmail(model, new FormWidgetAnswer());
            }

            return true;
        }
    }
}