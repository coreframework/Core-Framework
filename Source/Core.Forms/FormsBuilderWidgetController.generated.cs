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
namespace Core.Forms.Controllers {
    public partial class FormsBuilderWidgetController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FormsBuilderWidgetController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FormsBuilderWidgetController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult SubmitWidgetForm() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SubmitWidgetForm);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FormsBuilderWidgetController Actions { get { return FormsMVC.FormsBuilderWidget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Name = "FormsBuilderWidget";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String ViewWidget = "ViewWidget";
            public readonly String EditWidget = "EditWidget";
            public readonly String UpdateWidget = "UpdateWidget";
            public readonly String SubmitWidgetForm = "SubmitWidgetForm";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly String EditWidget = "~/Views/FormsBuilderWidget/EditWidget.ascx";
            public readonly String ViewWidget = "~/Views/FormsBuilderWidget/ViewWidget.ascx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_FormsBuilderWidgetController: Core.Forms.Controllers.FormsBuilderWidgetController {
        public T4MVC_FormsBuilderWidgetController() : base(Dummy.Instance) { }

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

        public override System.Web.Mvc.ActionResult UpdateWidget(Core.Forms.Models.FormBuilderWidgetViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateWidget);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SubmitWidgetForm(long instanceId, System.Web.Mvc.FormCollection collection) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SubmitWidgetForm);
            callInfo.RouteValueDictionary.Add("instanceId", instanceId);
            callInfo.RouteValueDictionary.Add("collection", collection);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
