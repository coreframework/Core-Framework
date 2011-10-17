using System;
using System.Web;
using System.Web.Mvc;
using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Framework.Core.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Helpers
{
    public class FormsHelper
    {
        /// <summary>
        /// Updates the form elements positions.
        /// </summary>
        /// <param name="formElementId">The form element id.</param>
        /// <param name="user">The user.</param>
        /// <param name="orderNumber">The order number.</param>
        public static void UpdateFormElementsPositions(long formElementId, ICorePrincipal user, Int32 orderNumber)
        {
            var formService = ServiceLocator.Current.GetInstance<IFormService>();
            var formElementService = ServiceLocator.Current.GetInstance<IFormElementService>();
            FormElement formElement = formElementService.Find(formElementId);
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            if (formElement != null)
            {
                bool isFormOwner = user != null && formElement.Form.UserId != null &&
                                   formElement.Form.UserId == user.PrincipalId;

                if (permissionService.IsAllowed((Int32) FormOperations.Manage, user, typeof (Form), formElement.Form.Id,
                                                isFormOwner, PermissionOperationLevel.Object))
                {
                    formElement.Form.FormElements.Update(
                        el =>
                            {
                                el.OrderNumber =
                                    el.OrderNumber > formElement.OrderNumber
                                         ? el.OrderNumber - 1
                                         : el.OrderNumber;
                            }
                        );
                    formElement.Form.FormElements.Update(
                        el =>
                            {
                                el.OrderNumber =
                                    el.OrderNumber >= orderNumber
                                         ? el.OrderNumber + 1
                                         : el.OrderNumber;
                            }
                        );
                    formService.Save(formElement.Form);
                    formElement.OrderNumber = orderNumber;
                    formElementService.Save(formElement);
                }
            }
        }

        /// <summary>
        /// Validates the form element.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState">State of the model.</param>
        public static void ValidateFormElement(FormElementViewModel model,  ModelStateDictionary modelState)
        {
            var currentType = model.Types.Find(type => type.Type == model.Type.ToString());

            if (currentType.IsValuesEnabled && String.IsNullOrEmpty(model.Values))
            {
                modelState.AddModelError(PropertyName.For<FormElementViewModel>(item => item.Values),
                    ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), typeof(FormElementViewModel),
                    PropertyName.For<FormElementViewModel>(item => item.Values), "required", null));
            }
        }

        /// <summary>
        /// Validates the form model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState">State of the model.</param>
        public static void ValidateForm(FormViewModel model, ModelStateDictionary modelState)
        {
            if (model.ShowSubmitButton && String.IsNullOrEmpty(model.SubmitButtonText))
            {
                modelState.AddModelError(PropertyName.For<FormViewModel>(item => item.SubmitButtonText),
                    ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), typeof(FormViewModel),
                    PropertyName.For<FormViewModel>(item => item.SubmitButtonText), "required", null));
            }
            if (model.ShowResetButton && String.IsNullOrEmpty(model.ResetButtonText))
            {
                modelState.AddModelError(PropertyName.For<FormViewModel>(item => item.ResetButtonText),
                     ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), typeof(FormViewModel),
                     PropertyName.For<FormViewModel>(item => item.ResetButtonText), "required", null));
            }
        }

        /// <summary>
        /// Updates the form element.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static FormElementViewModel UpdateFormElement(FormElementViewModel model)
        {
            var currentType = model.Types.Find(type => type.Type == model.Type.ToString());
            if (currentType!=null)
            {
                if (!currentType.IsValuesEnabled)
                    model.Values = null;
                if (!currentType.IsMaxLengthEnabled)
                    model.MaxLength = null;
                if (!currentType.IsValidationEnabled)
                    model.RegexTemplate = null;
            }
            return model;
        }
    }
}