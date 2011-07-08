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
namespace Core.Web.Areas.Admin.Controllers {
    public partial class WidgetController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected WidgetController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult DynamicGridData() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.DynamicGridData);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Enable() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Enable);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Disable() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Disable);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public WidgetController Actions { get { return MVC.Admin.Widget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Widget";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string DynamicGridData = "DynamicGridData";
            public readonly string Enable = "Enable";
            public readonly string Disable = "Disable";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Index = "~/Areas/Admin/Views/Widget/Index.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_WidgetController: Core.Web.Areas.Admin.Controllers.WidgetController {
        public T4MVC_WidgetController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Index() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Index);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.DynamicGridData);
            callInfo.RouteValueDictionary.Add("page", page);
            callInfo.RouteValueDictionary.Add("rows", rows);
            callInfo.RouteValueDictionary.Add("search", search);
            callInfo.RouteValueDictionary.Add("sidx", sidx);
            callInfo.RouteValueDictionary.Add("sord", sord);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Enable(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Enable);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Disable(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Disable);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
