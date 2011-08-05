using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.Validation;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Framework.Core.Extensions;
using Framework.MVC.Captcha;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

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
            var widget = new FormBuilderWidget();
            if (model.Id>0)
                widget = widgetService.Find(model.Id);

            var contentViewer = model.MapTo(widget);

            if (widget!=null)
            {
                widgetService.Save(contentViewer);
            }
          
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

        /// <summary>
        /// Handles the form data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static bool HandleFormData(FormBuilderWidget model, FormCollection collection, ICorePrincipal user)
        {
            bool result = true;
            if (model.SaveData || model.SendEmail)
            {
                //bind model

                var widgetAnswersService = ServiceLocator.Current.GetInstance<IFormWidgetAnswerService>();
                var answer = new FormWidgetAnswer
                                 {
                                     CreateDate = DateTime.Now,
                                     FormBuilderWidget = model,
                                     User = new BaseUser {Id = user.PrincipalId},
                                     Title = model.Title
                                 };

                //save answer values
                foreach (FormElement item in model.Form.FormElements.ToList().OrderBy(val=>val.OrderNumber))
                {
                    if (item.Type != FormElementType.Captcha)
                    {
                        string elementName = String.Format("{0}_{1}", item.Type, item.Id);
                        string value = collection[elementName];
                        if (value != null)
                        {
                            if (item.Type==FormElementType.RadioButtons)
                            {
                                value = GetRadioButtonValue(item.ElementValues, elementName, value);
                            }

                            if (!String.IsNullOrEmpty(value))
                            {
                                ((List<FormWidgetAnswerValue>) answer.AnswerValues).Add(new FormWidgetAnswerValue
                                                                                        {
                                                                                            Field = item.Title,
                                                                                            Value = value,
                                                                                            Answer = answer
                                                                                        });
                            }
                        }
                    }
                }

                //send email
                if (model.SendEmail)
                {
                    result = FormsMailer.SendFormAnswerEmail(model, answer);
                }

                //save answer
                if (model.SaveData)
                {
                    result = result && widgetAnswersService.Save(answer);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the radio button value.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="selectedElementName">Name of the selected element.</param>
        /// <returns></returns>
        private static String GetRadioButtonValue(String values, String elementName, String selectedElementName)
        {
            var valuesArray = values.Trim().Split(',');
            var result = new Dictionary<String, String>();

            for (var i = 0; i < valuesArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valuesArray[i]))
                {
                    if (selectedElementName.Equals(String.Format("{0}_{1}", elementName, i)))
                        return valuesArray[i];
                    result.Add(String.Format("{0}_{1}", elementName, i), valuesArray[i]);
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Removes the specified core widget instance.
        /// </summary>
        /// <param name="coreWidgetInstance">The core widget instance.</param>
        public static void Remove(ICoreWidgetInstance coreWidgetInstance)
        {
            if (coreWidgetInstance != null && coreWidgetInstance.InstanceId != null && coreWidgetInstance.InstanceId>0)
            {
                var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
                var widget = widgetService.Find((long) coreWidgetInstance.InstanceId);
                widgetService.Delete(widget);
            }
        }

        /// <summary>
        /// Clones the forms builder widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneFormsBuilderWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
            var widget = BindWidgetModel(instance);

            if (widget != null)
            {
                var clone = (FormBuilderWidget) new FormBuilderWidget().InjectFrom<CloneEntityInjection>(widget);
                if (widgetService.Save(clone))
                {
                    return clone.Id;
                }
            }
            return null;
        }
    }
}