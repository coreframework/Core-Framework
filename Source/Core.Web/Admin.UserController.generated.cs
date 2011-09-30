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
namespace Core.Web.Areas.Admin.Controllers {
    public partial class UserController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected UserController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult Create() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Create);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Edit() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Update() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Update);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Remove() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ConfirmRemove() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ConfirmRemove);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UserGroups() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.UserGroups);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult UserGroupsDynamicGridData() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.UserGroupsDynamicGridData);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult UpdateUserGroups() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.UpdateUserGroups);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public UserController Actions { get { return MVC.Admin.User; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly String Name = "User";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly String Index = "Index";
            public readonly String DynamicGridData = "DynamicGridData";
            public readonly String New = "New";
            public readonly String Create = "Create";
            public readonly String Edit = "Edit";
            public readonly String Update = "Update";
            public readonly String Remove = "Remove";
            public readonly String ConfirmRemove = "ConfirmRemove";
            public readonly String UserGroups = "UserGroups";
            public readonly String UserGroupsDynamicGridData = "UserGroupsDynamicGridData";
            public readonly String UpdateUserGroups = "UpdateUserGroups";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly String Edit = "~/Areas/Admin/Views/User/Edit.aspx";
            public readonly String Index = "~/Areas/Admin/Views/User/Index.aspx";
            public readonly String New = "~/Areas/Admin/Views/User/New.aspx";
            public readonly String Remove = "~/Areas/Admin/Views/User/Remove.aspx";
            public readonly String UserGroups = "~/Areas/Admin/Views/User/UserGroups.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_UserController: Core.Web.Areas.Admin.Controllers.UserController {
        public T4MVC_UserController() : base(Dummy.Instance) { }

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

        public override System.Web.Mvc.ActionResult New() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.New);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Create(Core.Web.Areas.Admin.Models.UserViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Create);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Update(long id, Core.Web.Areas.Admin.Models.UserViewModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Update);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Remove(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Remove);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ConfirmRemove(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ConfirmRemove);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserGroups(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.UserGroups);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult UserGroupsDynamicGridData(int id, int page, int rows, string search, string sidx, string sord) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.UserGroupsDynamicGridData);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("page", page);
            callInfo.RouteValueDictionary.Add("rows", rows);
            callInfo.RouteValueDictionary.Add("search", search);
            callInfo.RouteValueDictionary.Add("sidx", sidx);
            callInfo.RouteValueDictionary.Add("sord", sord);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult UpdateUserGroups(long id, System.Collections.Generic.IEnumerable<string> ids, System.Collections.Generic.IEnumerable<string> selids) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.UpdateUserGroups);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("ids", ids);
            callInfo.RouteValueDictionary.Add("selids", selids);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
