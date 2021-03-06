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
namespace Core.Web.Controllers {
    public partial class UploadController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UploadController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult File() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.File);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Image() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Image);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UploadController Actions { get { return MVC.Upload; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Name = "Upload";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String File = "File";
            public readonly String Image = "Image";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_UploadController: Core.Web.Controllers.UploadController {
        public T4MVC_UploadController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult File(System.Web.Mvc.FormCollection form) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.File);
            callInfo.RouteValueDictionary.Add("form", form);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Image(System.Web.Mvc.FormCollection form) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Image);
            callInfo.RouteValueDictionary.Add("form", form);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
