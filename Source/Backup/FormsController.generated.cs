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
namespace Core.Forms.Controllers {
    public partial class FormsController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected FormsController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowPermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowPermissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ApplyPermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPermissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Edit() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Save() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Save);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ShowFormElements() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ShowFormElements);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateFormElementPosition() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UpdateFormElementPosition);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult NewElement() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.NewElement);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public FormsController Actions { get { return FormsMVC.Forms; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Forms";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string ShowAll = "ShowAll";
            public readonly string ShowPermissions = "ShowPermissions";
            public readonly string ApplyPermissions = "ApplyPermissions";
            public readonly string New = "New";
            public readonly string Edit = "Edit";
            public readonly string Save = "Save";
            public readonly string ShowFormElements = "ShowFormElements";
            public readonly string UpdateFormElementPosition = "UpdateFormElementPosition";
            public readonly string NewElement = "NewElement";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_FormsController: Core.Forms.Controllers.FormsController {
        public T4MVC_FormsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ShowAll() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowAll);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPermissions(long formId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPermissions);
            callInfo.RouteValueDictionary.Add("formId", formId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ApplyPermissions(Core.Framework.Permissions.Models.PermissionsModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPermissions);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult New() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(long formId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("formId", formId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Save(Core.Forms.Models.FormViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Save);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowFormElements(long formId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowFormElements);
            callInfo.RouteValueDictionary.Add("formId", formId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateFormElementPosition(long formElementId, int orderNumber) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UpdateFormElementPosition);
            callInfo.RouteValueDictionary.Add("formElementId", formElementId);
            callInfo.RouteValueDictionary.Add("orderNumber", orderNumber);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult NewElement(long formId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.NewElement);
            callInfo.RouteValueDictionary.Add("formId", formId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
