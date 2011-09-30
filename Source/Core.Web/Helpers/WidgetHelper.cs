using System;
using System.Linq;
using System.Text;
using System.Web;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Helpers
{
    public class WidgetHelper : IWidgetHelper
    {
        /// <summary>
        /// Gets the widget view model.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <returns></returns>
        public static WidgetHolderViewModel GetWidgetViewModel(long pageWidgetId)
        {
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            PageWidget pageWidget = pageWidgetService.Find(pageWidgetId);

            return GetWidgetViewModel(pageWidget);
        }

        /// <summary>
        /// Gets the widget view model.
        /// </summary>
        /// <param name="pageWidget">The page widget.</param>
        /// <returns></returns>
        public static WidgetHolderViewModel GetWidgetViewModel(PageWidget pageWidget)
        {
            if (pageWidget == null || pageWidget.Widget == null)
                return null;

            ICoreWidget coreWidget =
                MvcApplication.Widgets.FirstOrDefault(wd => wd.Identifier == pageWidget.Widget.Identifier);

            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            ICorePrincipal currentPrincipal = HttpContext.Current.CorePrincipal();

            return new WidgetHolderViewModel
            {
                Widget = pageWidget,
                WidgetInstance = new CoreWidgetInstance
                {
                    InstanceId = pageWidget.InstanceId,
                    WidgetIdentifier = pageWidget.Widget.Identifier,
                    PageSettings = new CorePageSettings { PageId = pageWidget.Page.Id }

                },

                SystemWidget = coreWidget,

                Access = coreWidget is BaseWidget ?
                                        permissionService.GetAccess(((BaseWidget)coreWidget).Operations,
                                        HttpContext.Current.CorePrincipal(), coreWidget.GetType(), pageWidget.EntityId,
                                        currentPrincipal != null && pageWidget.User != null && pageWidget.User.PrincipalId == currentPrincipal.PrincipalId) : null,
                PageAccess = permissionService.GetAccess(pageWidget.Page.Operations, currentPrincipal, typeof(Page),
                                        pageWidget.Page.Id, currentPrincipal != null && pageWidget.Page.User != null
                                        && pageWidget.Page.User.PrincipalId == currentPrincipal.PrincipalId),


            };
        }

        /// <summary>
        /// Updates the page widget instance.
        /// </summary>
        /// <param name="pageWidgetId">The page widget id.</param>
        /// <param name="instanceId">The instance id.</param>
        /// <param name="user">The user.</param>
        public static void UpdatePageWidgetInstance(long pageWidgetId, long instanceId, ICorePrincipal user)
        {
            var pageWidgetService = ServiceLocator.Current.GetInstance<IPageWidgetService>();
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var pageWidget = pageWidgetService.Find(pageWidgetId);
            if (pageWidget != null)
            {
                bool isOwner = pageWidget.Page.User != null && user != null &&
                               pageWidget.Page.User.PrincipalId == user.PrincipalId;
                if (permissionService.IsAllowed((Int32)PageOperations.Update, user, typeof(Page),
                                                pageWidget.Page.Id, isOwner, PermissionOperationLevel.Object))
                {
                    if (IsManageWidgetAllowed(pageWidget, user, pageWidget.Id))
                    {
                        pageWidget.InstanceId = instanceId;
                        pageWidgetService.Save(pageWidget);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [is widget enabled] [the specified widget identifier].
        /// </summary>
        /// <param name="widgetIdentifier">The widget identifier.</param>
        /// <returns>
        /// 	<c>true</c> if [is widget enabled] [the specified widget identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsWidgetEnabled(String widgetIdentifier)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
            Widget widget = widgetService.FindWidgetByIdentifier(widgetIdentifier);

            return widgetService.IsWidgetEnable(widget);
        }

        /// <summary>
        /// Determines whether [is widget enabled] [the specified widget].
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <returns>
        /// 	<c>true</c> if [is widget enabled] [the specified widget]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsWidgetEnabled(Widget widget)
        {
            return widget != null && widget.Plugin != null && widget.Plugin.Status.Equals(PluginStatus.Installed) && widget.Status.Equals(WidgetStatus.Enabled);
        }

        public static String GetWidgetStyles(PageWidgetSettings settings)
        {
            var builder = new StringBuilder();
            if (settings != null)
            {
                if (settings.LookAndFeelSettings.WidthValue.HasValue && !String.IsNullOrEmpty(settings.LookAndFeelSettings.WidthUnit))
                {
                    builder.AppendFormat("width:{0}{1};", settings.LookAndFeelSettings.WidthValue, settings.LookAndFeelSettings.WidthUnit);
                }
            }
            return builder.ToString();
        }

        public static String GetWidgetHolderStyles(PageWidgetSettings settings)
        {
            var builder = new StringBuilder();
            if (settings != null)
            {
                AppendStyleString(builder, "background-color", settings.LookAndFeelSettings.BackgroundColor);
                AppendStyleString(builder, "font-family", settings.LookAndFeelSettings.FontFamily);
                AppendStyleString(builder, "color", settings.LookAndFeelSettings.Color);
                if (settings.LookAndFeelSettings.FontSizeValue.HasValue && !String.IsNullOrEmpty(settings.LookAndFeelSettings.FontSizeUnit))
                {
                    builder.AppendFormat("font-size:{0}{1};", settings.LookAndFeelSettings.FontSizeValue, settings.LookAndFeelSettings.FontSizeUnit);
                }
                if (settings.LookAndFeelSettings.HeightValue.HasValue && !String.IsNullOrEmpty(settings.LookAndFeelSettings.HeightUnit))
                {
                    builder.AppendFormat("height:{0}{1};", settings.LookAndFeelSettings.HeightValue, settings.LookAndFeelSettings.HeightUnit);
                }
            }
            return builder.ToString();
        }

        private static void AppendStyleString(StringBuilder builder, String styleName, String styleValue)
        {
            if (!String.IsNullOrEmpty(styleValue))
            {
                builder.AppendFormat("{0}:{1};", styleName, styleValue);
            }
        }

        /// <summary>
        /// Gets the widget client id.
        /// </summary>
        /// <param name="widgetId">The widget id.</param>
        /// <returns></returns>
        public static String GetWidgetClientId(long widgetId)
        {
            return String.Format("widget_{0}", widgetId);
        }

        /// <summary>
        /// Determines whether [is manage widget allowed] [the specified widget identifier].
        /// </summary>
        /// <param name="pageWidget">The page widget.</param>
        /// <param name="user">The user.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns>
        /// 	<c>true</c> if [is manage widget allowed] [the specified widget identifier]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsManageWidgetAllowed(PageWidget pageWidget, ICorePrincipal user, long entityId)
        {
            bool isAllowed = true;

            if (pageWidget.Widget!=null)
            {
                ICoreWidget widget =
                    MvcApplication.Widgets.FirstOrDefault(item => item.Identifier == pageWidget.Widget.Identifier);
                if (widget != null && widget is BaseWidget)
                {
                    bool isOwner = pageWidget.User != null && user != null &&
                                   pageWidget.User.PrincipalId == user.PrincipalId;
                    var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
                    isAllowed = permissionService.IsAllowed((widget as BaseWidget).ManageOperationCode, user,
                                                            widget.GetType(), entityId, isOwner,
                                                            PermissionOperationLevel.Object);
                }
            }

            return isAllowed;
        }
    }
}
