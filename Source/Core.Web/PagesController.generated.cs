// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Framework.MVC.T4MVC;
using T4MVC;
namespace Core.Web.Controllers {
    public partial class PagesController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PagesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Index() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Show() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Show);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RemovePage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RemovePage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CreateNewPage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.CreateNewPage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdatePagePosition() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePagePosition);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ChangePageMode() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ChangePageMode);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ChangeLayout() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLayout);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowChangeLayoutForm() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowChangeLayoutForm);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowLayoutSettingsForm() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowLayoutSettingsForm);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateLayoutSettingsForm() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateLayoutSettingsForm);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowPageLookAndFeel() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageLookAndFeel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdatePageLookAndFeel() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageLookAndFeel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowAvailableWidgets() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowAvailableWidgets);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult AddWidget() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.AddWidget);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdatePageWidgetInstance() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageWidgetInstance);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult RemovePageWidget() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.RemovePageWidget);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateWidgetsPositions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetsPositions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowPageCommonSettings() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageCommonSettings);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ChangeLanguage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SavePageCommonSettings() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SavePageCommonSettings);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdatePageCommonSettings() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageCommonSettings);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowPageCSS() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageCSS);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdatePageCSS() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageCSS);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowPagePermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowPagePermissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ApplyPagePermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPagePermissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowSettings() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowSettings);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowWidgetLookAndFeel() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetLookAndFeel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateWidgetLookAndFeel() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetLookAndFeel);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowWidgetCSS() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetCSS);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateWidgetCSS() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetCSS);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowWidgetPermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetPermissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ApplyWidgetPermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ApplyWidgetPermissions);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PagesController Actions { get { return MVC.Pages; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Pages";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string Show = "Show";
            public readonly string RemovePage = "RemovePage";
            public readonly string CreateNewPage = "CreateNewPage";
            public readonly string UpdatePagePosition = "UpdatePagePosition";
            public readonly string ChangePageMode = "ChangePageMode";
            public readonly string ChangeLayout = "ChangeLayout";
            public readonly string ShowChangeLayoutForm = "ShowChangeLayoutForm";
            public readonly string ShowLayoutSettingsForm = "ShowLayoutSettingsForm";
            public readonly string UpdateLayoutSettingsForm = "UpdateLayoutSettingsForm";
            public readonly string ShowPageLookAndFeel = "ShowPageLookAndFeel";
            public readonly string UpdatePageLookAndFeel = "UpdatePageLookAndFeel";
            public readonly string ShowAvailableWidgets = "ShowAvailableWidgets";
            public readonly string AddWidget = "AddWidget";
            public readonly string UpdatePageWidgetInstance = "UpdatePageWidgetInstance";
            public readonly string RemovePageWidget = "RemovePageWidget";
            public readonly string UpdateWidgetsPositions = "UpdateWidgetsPositions";
            public readonly string ShowPageCommonSettings = "ShowPageCommonSettings";
            public readonly string ChangeLanguage = "ChangeLanguage";
            public readonly string SavePageCommonSettings = "SavePageCommonSettings";
            public readonly string UpdatePageCommonSettings = "UpdatePageCommonSettings";
            public readonly string ShowPageCSS = "ShowPageCSS";
            public readonly string UpdatePageCSS = "UpdatePageCSS";
            public readonly string ShowPagePermissions = "ShowPagePermissions";
            public readonly string ApplyPagePermissions = "ApplyPagePermissions";
            public readonly string ShowSettings = "ShowSettings";
            public readonly string ShowWidgetLookAndFeel = "ShowWidgetLookAndFeel";
            public readonly string UpdateWidgetLookAndFeel = "UpdateWidgetLookAndFeel";
            public readonly string ShowWidgetCSS = "ShowWidgetCSS";
            public readonly string UpdateWidgetCSS = "UpdateWidgetCSS";
            public readonly string ShowWidgetPermissions = "ShowWidgetPermissions";
            public readonly string ApplyWidgetPermissions = "ApplyWidgetPermissions";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string ManagePageMenu = "~/Views/Pages/ManagePageMenu.ascx";
            public readonly string NavigationMenu = "~/Views/Pages/NavigationMenu.ascx";
            public readonly string NavigationMenuItem = "~/Views/Pages/NavigationMenuItem.ascx";
            public readonly string PageCommonSettings = "~/Views/Pages/PageCommonSettings.ascx";
            public readonly string PageCreateForm = "~/Views/Pages/PageCreateForm.ascx";
            public readonly string PageCSSForm = "~/Views/Pages/PageCSSForm.ascx";
            public readonly string PageCSSSettings = "~/Views/Pages/PageCSSSettings.ascx";
            public readonly string PageLookAndFeelForm = "~/Views/Pages/PageLookAndFeelForm.ascx";
            public readonly string PageLookAndFeelSettings = "~/Views/Pages/PageLookAndFeelSettings.ascx";
            public readonly string PagePermissions = "~/Views/Pages/PagePermissions.ascx";
            public readonly string Show = "~/Views/Pages/Show.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_PagesController: Core.Web.Controllers.PagesController {
        public T4MVC_PagesController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index(Core.Web.Models.PageViewModel currentPage) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            callInfo.RouteValueDictionary.Add("currentPage", currentPage);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Show(string url) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Show);
            callInfo.RouteValueDictionary.Add("url", url);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RemovePage(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RemovePage);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateNewPage(long? parentPageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreateNewPage);
            callInfo.RouteValueDictionary.Add("parentPageId", parentPageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePagePosition(long pageId, int orderNumber) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePagePosition);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            callInfo.RouteValueDictionary.Add("orderNumber", orderNumber);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangePageMode(Core.Web.Models.PageMode pageMode) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangePageMode);
            callInfo.RouteValueDictionary.Add("pageMode", pageMode);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangeLayout(long pageId, long layoutTemplateId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLayout);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            callInfo.RouteValueDictionary.Add("layoutTemplateId", layoutTemplateId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowChangeLayoutForm(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowChangeLayoutForm);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowLayoutSettingsForm(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowLayoutSettingsForm);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateLayoutSettingsForm(Core.Web.Models.LayoutSettingsModel layoutSettings) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateLayoutSettingsForm);
            callInfo.RouteValueDictionary.Add("layoutSettings", layoutSettings);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPageLookAndFeel(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageLookAndFeel);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePageLookAndFeel(Core.Web.Models.PageLookAndFeelModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageLookAndFeel);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowAvailableWidgets(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowAvailableWidgets);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddWidget(long pageId, long widgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.AddWidget);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            callInfo.RouteValueDictionary.Add("widgetId", widgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePageWidgetInstance(long pageWidgetId, long instanceId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageWidgetInstance);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            callInfo.RouteValueDictionary.Add("instanceId", instanceId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RemovePageWidget(long pageWidgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.RemovePageWidget);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateWidgetsPositions(long widgetId, int pageSection, int columnNumber, int orderNumber) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetsPositions);
            callInfo.RouteValueDictionary.Add("widgetId", widgetId);
            callInfo.RouteValueDictionary.Add("pageSection", pageSection);
            callInfo.RouteValueDictionary.Add("columnNumber", columnNumber);
            callInfo.RouteValueDictionary.Add("orderNumber", orderNumber);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPageCommonSettings(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageCommonSettings);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangeLanguage(long pageId, string culture) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            callInfo.RouteValueDictionary.Add("culture", culture);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SavePageCommonSettings(Core.Web.Models.PageViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SavePageCommonSettings);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePageCommonSettings(Core.Web.Models.PageLocaleViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageCommonSettings);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPageCSS(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPageCSS);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdatePageCSS(Core.Web.Models.PageCSSModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdatePageCSS);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPagePermissions(long pageId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPagePermissions);
            callInfo.RouteValueDictionary.Add("pageId", pageId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ApplyPagePermissions(Core.Framework.Permissions.Models.PermissionsModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPagePermissions);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowSettings(long pageWidgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowSettings);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowWidgetLookAndFeel(long pageWidgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetLookAndFeel);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateWidgetLookAndFeel(Core.Web.Models.WidgetLookAndFeelModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetLookAndFeel);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowWidgetCSS(long pageWidgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetCSS);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateWidgetCSS(Core.Web.Models.WidgetCSSModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidgetCSS);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowWidgetPermissions(long pageWidgetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowWidgetPermissions);
            callInfo.RouteValueDictionary.Add("pageWidgetId", pageWidgetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ApplyWidgetPermissions(Core.Framework.Permissions.Models.PermissionsModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ApplyWidgetPermissions);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
