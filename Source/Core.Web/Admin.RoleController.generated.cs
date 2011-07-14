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
    public partial class RoleController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RoleController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult ChangeLanguage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
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
        public System.Web.Mvc.ActionResult Users() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Users);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult UsersDynamicGridData() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.UsersDynamicGridData);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.JsonResult UpdateUsers() {
            return new T4MVC_JsonResult(Area, Name, ActionNames.UpdateUsers);
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
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Permissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Permissions);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ApplyPermissions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPermissions);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RoleController Actions { get { return MVC.Admin.Role; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Role";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Index = "Index";
            public readonly string DynamicGridData = "DynamicGridData";
            public readonly string New = "New";
            public readonly string Create = "Create";
            public readonly string Edit = "Edit";
            public readonly string ChangeLanguage = "ChangeLanguage";
            public readonly string Update = "Update";
            public readonly string Remove = "Remove";
            public readonly string ConfirmRemove = "ConfirmRemove";
            public readonly string Users = "Users";
            public readonly string UsersDynamicGridData = "UsersDynamicGridData";
            public readonly string UpdateUsers = "UpdateUsers";
            public readonly string UserGroups = "UserGroups";
            public readonly string UserGroupsDynamicGridData = "UserGroupsDynamicGridData";
            public readonly string UpdateUserGroups = "UpdateUserGroups";
            public readonly string Permissions = "Permissions";
            public readonly string ApplyPermissions = "ApplyPermissions";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Edit = "~/Areas/Admin/Views/Role/Edit.aspx";
            public readonly string EditForm = "~/Areas/Admin/Views/Role/EditForm.ascx";
            public readonly string Index = "~/Areas/Admin/Views/Role/Index.aspx";
            public readonly string New = "~/Areas/Admin/Views/Role/New.aspx";
            public readonly string PermissionOperations = "~/Areas/Admin/Views/Role/PermissionOperations.ascx";
            public readonly string Permissions = "~/Areas/Admin/Views/Role/Permissions.aspx";
            public readonly string Remove = "~/Areas/Admin/Views/Role/Remove.aspx";
            public readonly string UserGroups = "~/Areas/Admin/Views/Role/UserGroups.aspx";
            public readonly string Users = "~/Areas/Admin/Views/Role/Users.aspx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_RoleController: Core.Web.Areas.Admin.Controllers.RoleController {
        public T4MVC_RoleController() : base(Dummy.Instance) { }

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

        public override System.Web.Mvc.ActionResult Create(Core.Web.Areas.Admin.Models.RoleViewModel roleView) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Create);
            callInfo.RouteValueDictionary.Add("roleView", roleView);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Edit(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Edit);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ChangeLanguage(long roleId, string culture) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ChangeLanguage);
            callInfo.RouteValueDictionary.Add("roleId", roleId);
            callInfo.RouteValueDictionary.Add("culture", culture);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Update(long id, Core.Web.Areas.Admin.Models.RoleLocaleViewModel roleView) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Update);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("roleView", roleView);
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

        public override System.Web.Mvc.ActionResult Users(long id) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Users);
            callInfo.RouteValueDictionary.Add("id", id);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult UsersDynamicGridData(int id, int page, int rows, string search, string sidx, string sord) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.UsersDynamicGridData);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("page", page);
            callInfo.RouteValueDictionary.Add("rows", rows);
            callInfo.RouteValueDictionary.Add("search", search);
            callInfo.RouteValueDictionary.Add("sidx", sidx);
            callInfo.RouteValueDictionary.Add("sord", sord);
            return callInfo;
        }

        public override System.Web.Mvc.JsonResult UpdateUsers(long id, System.Collections.Generic.IEnumerable<string> ids, System.Collections.Generic.IEnumerable<string> selids) {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.UpdateUsers);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("ids", ids);
            callInfo.RouteValueDictionary.Add("selids", selids);
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

        public override System.Web.Mvc.ActionResult Permissions(long roleId, string resource) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Permissions);
            callInfo.RouteValueDictionary.Add("roleId", roleId);
            callInfo.RouteValueDictionary.Add("resource", resource);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ApplyPermissions(Core.Web.Areas.Admin.Models.PermissionOperationsModel model) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ApplyPermissions);
            callInfo.RouteValueDictionary.Add("model", model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
