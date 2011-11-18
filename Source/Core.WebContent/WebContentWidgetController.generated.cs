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
using Framework.Mvc.T4MVC;
using T4MVC;
namespace Core.WebContent.Controllers {
    public partial class WebContentWidgetController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected WebContentWidgetController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ViewWidget() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ViewWidget);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult EditWidget() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.EditWidget);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateWidget() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidget);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult LoadCategories() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.LoadCategories);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult LoadArticles() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.LoadArticles);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public WebContentWidgetController Actions { get { return WebContentMVC.WebContentWidget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Name = "WebContentWidget";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String  ViewWidget = "ViewWidget";
            public readonly String  EditWidget = "EditWidget";
            public readonly String  UpdateWidget = "UpdateWidget";
            public readonly String  LoadCategories = "LoadCategories";
            public readonly String  LoadArticles = "LoadArticles";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly String  EditWidget = "~/Views/WebContentWidget/EditWidget.ascx";
            public readonly String  ViewWidget = "~/Views/WebContentWidget/ViewWidget.ascx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_WebContentWidgetController: Core.WebContent.Controllers.WebContentWidgetController {
        public T4MVC_WebContentWidgetController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ViewWidget(Core.Framework.Plugins.Web.ICoreWidgetInstance instance) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ViewWidget);
            callInfo.RouteValueDictionary.Add("instance", instance);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditWidget(Core.Framework.Plugins.Web.ICoreWidgetInstance instance) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.EditWidget);
            callInfo.RouteValueDictionary.Add("instance", instance);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateWidget(Core.WebContent.Models.WebContentWidgetViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidget);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult LoadCategories(string categoriesId, long sectionId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.LoadCategories);
            callInfo.RouteValueDictionary.Add("categoriesId", categoriesId);
            callInfo.RouteValueDictionary.Add("sectionId", sectionId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult LoadArticles(string categoriesId, long? articleId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.LoadArticles);
            callInfo.RouteValueDictionary.Add("categoriesId", categoriesId);
            callInfo.RouteValueDictionary.Add("articleId", articleId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
