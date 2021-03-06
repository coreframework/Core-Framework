﻿using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Forms.Helpers;
using Core.Forms.Models;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Web;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "FormsBuilderWidget")]
    public partial class FormsBuilderWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return FormsBuilderWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
                var widget = FormsBuilderWidgetHelper.BindWidgetModel(instance);

                if (widget != null)
                    return PartialView(widget);
            }
            return Content(HttpContext.Translate("Messages.SetupForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
                var widget = new FormBuilderWidget();

                if (instance.InstanceId != null)
                {
                    var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
                    var existingWidget = widgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }

                return PartialView(new FormBuilderWidgetViewModel().MapFrom(widget));
            }

            return Content(HttpContext.Translate("Messages.SetupForm", ResourceHelper.GetControllerScope(this)));
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(FormBuilderWidgetViewModel model)
        {
            if (model.SendEmail && String.IsNullOrEmpty(model.RecipientEmail))
            {
                ModelState.AddModelError("RecipientEmail", HttpContext.Translate("Messages.EmailRequired", ResourceHelper.GetControllerScope(this)));
            }
            if (ModelState.IsValid)
            {
                model = FormsBuilderWidgetHelper.SaveFormBuilderWidget(model);
            }

            return PartialView("EditWidget", model);
        }

        /// <summary>
        /// Submits the widget form.
        /// </summary>
        /// <param name="instanceId">The instance id.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SubmitWidgetForm(long instanceId, FormCollection collection)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IFormBuilderWidgetService>();
            var model = widgetService.Find(instanceId);

            if (model!=null)
            {
                FormsBuilderWidgetHelper.Validate(model, collection, ModelState);

                if (ModelState.IsValid)
                {
                    FormsBuilderWidgetHelper.HandleFormData(model, collection, this.CorePrincipal());
                    Success(HttpContext.Translate("Messages.SuccessFormSubmit",
                                                                ResourceHelper.GetControllerScope(this)));
                }
                else
                {
                    Error(HttpContext.Translate("Messages.ValidationError",
                                                                ResourceHelper.GetControllerScope(this)));
                    ViewData[String.Format("FormCollection{0}", model.Id)] = collection;
                }

                return PartialView("ViewWidget", model);
            }

            return Content(HttpContext.Translate("Messages.Error", ResourceHelper.GetControllerScope(this)));
        }

        #endregion
    }
}
