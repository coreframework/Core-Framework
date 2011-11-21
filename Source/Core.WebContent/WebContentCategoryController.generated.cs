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
    public partial class WebContentCategoryController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected WebContentCategoryController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult LoadData() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.LoadData);
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
        public System.Web.Mvc.ActionResult Save() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Save);
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
        public System.Web.Mvc.ActionResult Remove() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public WebContentCategoryController Actions { get { return WebContentMVC.WebContentCategory; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String  Name = "WebContentCategory";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String  Show = "Show";
            public readonly String  LoadData = "LoadData";
            public readonly String  New = "New";
            public readonly String  Edit = "Edit";
            public readonly String  ChangeLanguage = "ChangeLanguage";
            public readonly String  Save = "Save";
            public readonly String  ShowPermissions = "ShowPermissions";
            public readonly String  ApplyPermissions = "ApplyPermissions";
            public readonly String  Remove = "Remove";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly String  CategoryDetails = "~/Views/WebContentCategory/CategoryDetails.ascx";
            public readonly String  Edit = "~/Views/WebContentCategory/Edit.aspx";
            public readonly String  New = "~/Views/WebContentCategory/New.aspx";
            public readonly String  Show = "~/Views/WebContentCategory/Show.aspx";
            public readonly String  ShowPermissions = "~/Views/WebContentCategory/ShowPermissions.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_WebContentCategoryController: Core.WebContent.Controllers.WebContentCategoryController {
        public T4MVC_WebContentCategoryController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Show() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Show);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult LoadData(int page, int rows, string search, string sidx, string sord) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.LoadData);
            callInfo.RouteValueDictionary.Add("page", page);
            callInfo.RouteValueDictionary.Add("rows", rows);
            callInfo.RouteValueDictionary.Add("search", search);
            callInfo.RouteValueDictionary.Add("sidx", sidx);
            callInfo.RouteValueDictionary.Add("sord", sord);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult New() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult New(Core.WebContent.Models.CategoryViewModel category) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            callInfo.RouteValueDictionary.Add("category", category);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(long categoryId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("categoryId", categoryId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangeLanguage(long categoryId, string culture) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
            callInfo.RouteValueDictionary.Add("categoryId", categoryId);
            callInfo.RouteValueDictionary.Add("culture", culture);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Save(Core.WebContent.Models.CategoryViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Save);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ShowPermissions(long categoryId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ShowPermissions);
            callInfo.RouteValueDictionary.Add("categoryId", categoryId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ApplyPermissions(Core.Framework.Permissions.Models.PermissionsModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPermissions);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Remove(long categoryId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
            callInfo.RouteValueDictionary.Add("categoryId", categoryId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591