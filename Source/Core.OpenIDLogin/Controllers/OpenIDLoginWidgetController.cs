using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Security;
using Core.Framework.MEF.Web;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Authentication;
using Core.Framework.Plugins.Web;
using Core.OpenIDLogin.Models;
using Core.OpenIDLogin.NHibernate.Contracts;
using Core.OpenIDLogin.Widgets;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.OpenIDLogin.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "OpenIDLoginWidget")]
    public partial class OpenIDLoginWidgetController : CoreWidgetController
    {
        #region Fields

        private IOpenIDLoginWidgetService openIdLoginWidgetService;

        private static OpenIdRelyingParty openIdProvider = new OpenIdRelyingParty();

        #endregion

        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return OpenIDLoginWidget.Instance.Identifier;
            }
        }

        #endregion

        #region Constructors

        public OpenIDLoginWidgetController()
        {
            openIdLoginWidgetService = ServiceLocator.Current.GetInstance<IOpenIDLoginWidgetService>();
        }

        #endregion

        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            Response.AppendHeader("X-XRDS-Location", new Uri(Request.Url, Response.ApplyAppPathModifier("~/openid-login-widget/xrds")).AbsoluteUri);
            bool showTitle = true;
            if (instance != null && instance.InstanceId != null)
            {
                var existingWidget = openIdLoginWidgetService.Find((long)instance.InstanceId);
                if (existingWidget != null)
                {
                    showTitle = existingWidget.ShowTitle;
                }
            }

            return PartialView(new OpenIDLoginWidgetViewModel { PageWidgetId = instance.PageWidgetId ?? 0, ShowTitle = showTitle });
        }

        public virtual ActionResult Xrds()
        {
            return View("Xrds");
        }

        /// <summary>
        /// Creates new user session.
        /// </summary>
        /// <param name="model">The login details.</param>
        /// <returns>Authentication result.</returns>
        public virtual ActionResult CreateUserSession(OpenIDLoginWidgetViewModel model)
        {
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            //            if (ModelState.IsValid)
            //            {
            IAuthenticationResponse response = openIdProvider.GetResponse();
            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(model.UserOpenId, out id))
                {
                    try
                    {
                        return openIdProvider.CreateRequest(model.UserOpenId).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        Error(ex.Message);
                    }
                }
                else
                {
                    Error(HttpContext.Translate("Messages.InvalidIdentifier",
                                          ResourceHelper.GetControllerScope(this)));
                }
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        {
                            //Session["FriendlyIdentifier"] = response.FriendlyIdentifierForDisplay;
                            var user = userService.FindByEmailOrUsername(response.FriendlyIdentifierForDisplay);
                            if (user == null)
                            {
                                user = new User
                                           {
                                               Username = response.FriendlyIdentifierForDisplay,
                                               Email = response.FriendlyIdentifierForDisplay
                                           };
                                userService.SetPassword(user, Guid.NewGuid().ToString());
                            }
                            authenticationHelper.LoginUser(user, true);
                            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
                            Success(response.FriendlyIdentifierForDisplay);
                            break;
                        }
                    case AuthenticationStatus.Canceled:
                        {
                            Error(HttpContext.Translate("Messages.CanceledAtProvider",
                                          ResourceHelper.GetControllerScope(this)));
                            break;
                        }
                    case AuthenticationStatus.Failed:
                        {
                            Error(response.Exception.Message);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                return Redirect(Url.Action("Index", "Home", new { area = "" }));
            }
            //            }

            return PartialView("ViewWidget", model);
        }

        /// <summary>
        /// Close current authentication session.
        /// </summary>
        /// <returns>Redirects to root url.</returns>
        [HttpGet]
        public virtual ActionResult DeleteUserSession()
        {
            var authenticationHelper = ServiceLocator.Current.GetInstance<IAuthenticationHelper>();
            authenticationHelper.LogoutUser();
            return Redirect(Url.Action("Index", "Home", new { area = "" }));
        }

        /// <summary>
        /// Edits the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult EditWidget(ICoreWidgetInstance instance)
        {
            var widgetModel = new OpenIDLoginWidgetEditModel();
            if (instance != null)
            {
                NHibernate.Models.OpenIDLoginWidget widget = null;
                if (instance.InstanceId != null)
                {
                    var existingWidget = openIdLoginWidgetService.Find((long)instance.InstanceId);

                    if (existingWidget != null)
                        widget = existingWidget;
                }
                if (widget == null)
                {
                    widget = new NHibernate.Models.OpenIDLoginWidget();
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
        public virtual ActionResult UpdateWidget(OpenIDLoginWidgetEditModel model)
        {
            if (ModelState.IsValid)
            {
                var widget = new NHibernate.Models.OpenIDLoginWidget();
                if (model.Id > 0)
                {
                    widget = openIdLoginWidgetService.Find(model.Id);
                }
                widget = model.MapTo(widget);
                openIdLoginWidgetService.Save(widget);
                model.MapFrom(widget);
                Success(HttpContext.Translate("Messages.Success",
                                              ResourceHelper.GetControllerScope(this)));
            }

            return PartialView("EditWidget", model);
        }
    }
}
