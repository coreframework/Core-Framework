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
namespace Core.ContentPages.Controllers {
    public partial class ContentPageController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ContentPageController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult Edit() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ChangeLanguage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Remove() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ContentPageController Actions { get { return ContentPagesMVC.ContentPage; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Name = "ContentPage";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String  ShowAll = "ShowAll";
            public readonly String  DynamicGridData = "DynamicGridData";
            public readonly String  Edit = "Edit";
            public readonly String  ChangeLanguage = "ChangeLanguage";
            public readonly String  New = "New";
            public readonly String  Remove = "Remove";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_ContentPageController: Core.ContentPages.Controllers.ContentPageController {
        public T4MVC_ContentPageController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ShowAll() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowAll);
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

        public override System.Web.Mvc.ActionResult Edit(long? id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangeLanguage(long contentPageId, string culture) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
            callInfo.RouteValueDictionary.Add("contentPageId", contentPageId);
            callInfo.RouteValueDictionary.Add("culture", culture);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(long? id, Core.ContentPages.Models.ContentPageLocaleViewModel contentPageModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("contentPageModel", contentPageModel);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult New() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult New(Core.ContentPages.Models.ContentPageViewModel contentPage) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            callInfo.RouteValueDictionary.Add("contentPage", contentPage);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Remove(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
