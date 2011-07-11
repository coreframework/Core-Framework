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
namespace Core.Languages.Controllers {
    public partial class LanguageSelectorWidgetController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public LanguageSelectorWidgetController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected LanguageSelectorWidgetController(Dummy d) { }

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

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public LanguageSelectorWidgetController Actions { get { return LanguagesMVC.LanguageSelectorWidget; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "LanguageSelectorWidget";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string ViewWidget = "ViewWidget";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string ViewWidget = "~/Views/LanguageSelectorWidget/ViewWidget.ascx";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_LanguageSelectorWidgetController: Core.Languages.Controllers.LanguageSelectorWidgetController {
        public T4MVC_LanguageSelectorWidgetController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ViewWidget(Core.Framework.Plugins.Web.ICoreWidgetInstance instance) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.ViewWidget);
            callInfo.RouteValueDictionary.Add("instance", instance);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
