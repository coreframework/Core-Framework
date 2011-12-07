﻿// <auto-generated />
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

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static class ProfilesMVC {
    public static Core.Profiles.Controllers.LoginWidgetController LoginWidget = new Core.Profiles.Controllers.T4MVC_LoginWidgetController();
    public static Core.Profiles.Controllers.ProfileElementController ProfileElement = new Core.Profiles.Controllers.T4MVC_ProfileElementController();
    public static Core.Profiles.Controllers.ProfileHeaderController ProfileHeader = new Core.Profiles.Controllers.T4MVC_ProfileHeaderController();
    public static Core.Profiles.Controllers.ProfileTypeController ProfileType = new Core.Profiles.Controllers.T4MVC_ProfileTypeController();
    public static Core.Profiles.Controllers.RegistrationWidgetController RegistrationWidget = new Core.Profiles.Controllers.T4MVC_RegistrationWidgetController();
}

namespace T4MVC {
}

namespace System.Web.Mvc {
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class T4Extensions {
        public static IT4MVCActionResult GetT4MVCResult(this ActionResult result) {
            var t4MVCResult = result as IT4MVCActionResult;
            if (t4MVCResult == null) {
                throw new InvalidOperationException("T4MVC methods can only be passed pseudo-action calls (e.g. MVC.Home.About()), and not real action calls.");
            }
            return t4MVCResult;
        }
        
        public static void InitMVCT4Result(this IT4MVCActionResult result, String  area, String  controller, String  action) {
            result.Area = area;
            result.Controller = controller;
            result.Action = action;
            result.RouteValueDictionary = new RouteValueDictionary();
             
            result.RouteValueDictionary.Add("Controller", controller);
            result.RouteValueDictionary.Add("Action", action);
        }
    }
}

  

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public class T4MVC_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult {
    public T4MVC_ActionResult(String  area, String  controller, String  action): base()  {
        this.InitMVCT4Result(area, controller, action);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public String  Area { get; set; }
    public String  Controller { get; set; }
    public String  Action { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public class T4MVC_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult {
    public T4MVC_JsonResult(String  area, String  controller, String  action): base()  {
        this.InitMVCT4Result(area, controller, action);
    }
    
    public String  Area { get; set; }
    public String  Controller { get; set; }
    public String  Action { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links {
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const String  URLPATH = "~/Scripts";
        public static String  Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static String  Url(String  fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const String  URLPATH = "~/Content";
        public static String  Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static String  Url(String  fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Css {
            private const String  URLPATH = "~/Content/Css";
            public static String  Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static String  Url(String  fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly String  profiles_css = Url("profiles.css");
        }
    
    }

}

static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query String :
    //      return "http://localhost" + path + "?foo=bar";
    private static String  ProcessVirtualPathDefault(String  virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        String  path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<String , String > ProcessVirtualPath = ProcessVirtualPathDefault;


    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}




namespace T4MVC {
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

#endregion T4MVC
#pragma warning restore 1591


