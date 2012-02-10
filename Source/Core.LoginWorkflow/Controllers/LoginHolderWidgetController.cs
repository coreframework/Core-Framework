using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Plugins.Helpers;
using Core.Framework.Plugins.Web;
using Core.LoginWorkflow.Models;
using Core.LoginWorkflow.NHibernate.Contracts;
using Core.LoginWorkflow.Widgets;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.LoginWorkflow.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LoginHolderWidget")]
    public class LoginHolderWidgetController : CoreWidgetController
    {
        #region Fields

        private ILoginHolderWidgetService loginHolderWidgetService;

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return LoginHolderWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructors

        public LoginHolderWidgetController()
        {
            loginHolderWidgetService = ServiceLocator.Current.GetInstance<ILoginHolderWidgetService>();
        }

        #endregion

        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            var coreWidgetInstanceBuilder = ServiceLocator.Current.GetInstance<ICoreWidgetInstanceBuilder>();
            var model = new LoginHolderWidgetViewModel
                            {
                                PageWidgetId = instance.PageWidgetId
                            };
            NHibernate.Models.LoginHolderWidget widget = null;
            if (instance.InstanceId != null)
            {
                var existingWidget = loginHolderWidgetService.Find((long)instance.InstanceId);

                if (existingWidget != null)
                    widget = existingWidget;
            }
            if (widget == null)
            {
                widget = new NHibernate.Models.LoginHolderWidget();
            }
            model.FormLoginWidgetInstance = coreWidgetInstanceBuilder.Build(widget.FormLoginWidget.Id, String.Empty,
                                                                            null, null);
            model.OpenIdLoginWidgetInstance = coreWidgetInstanceBuilder.Build(widget.OpenIdLoginWidget.Id, String.Empty,
                                                                            null, null);

            return PartialView(model);
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            var widgetModel = new LoginHolderWidgetEditModel();
            if (instance != null)
            {
                NHibernate.Models.LoginHolderWidget widget = null;
                if (instance.InstanceId != null)
                {
                    var existingWidget = loginHolderWidgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }
                if (widget == null)
                {
                    widget = new NHibernate.Models.LoginHolderWidget();
                    loginHolderWidgetService.Save(widget);
                    IWidgetHelper widgetHelper = ServiceLocator.Current.GetInstance<IWidgetHelper>();
                    widgetHelper.UpdatePageWidgetInstance(instance.PageWidgetId ?? 0, widget.Id, this.CorePrincipal());
                }

                widgetModel = widgetModel.MapFrom(widget);
            }

            return PartialView(widgetModel);
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult UpdateWidget(LoginHolderWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                var widget = new NHibernate.Models.LoginHolderWidget();
                if (model.Id > 0)
                {
                    widget = loginHolderWidgetService.Find(model.Id);
                }
                widget = model.MapTo(widget);
                loginHolderWidgetService.Save(widget);
                model.MapFrom(widget);
                Success(HttpContext.Translate("Messages.Success",
                                              ResourceHelper.GetControllerScope(this)));
            }

            return PartialView("EditWidget", model);
        }

    }
}
