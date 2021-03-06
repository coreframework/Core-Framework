﻿using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.MVC.Routing;

namespace Core.Forms
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Forms"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.Forms", "admin/forms", FormsMVC.Forms.ShowAll(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/form-{formId}/permissions", FormsMVC.Forms.ShowPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/forms/apply-permissions", FormsMVC.Forms.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, "admin/form-{formId}/form-elements", FormsMVC.Forms.ShowFormElements(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/forms/update-form-elements-positions", FormsMVC.Forms.UpdateFormElementPosition());

            context.MapRoute(null, "admin/forms/edit-{formId}", FormsMVC.Forms.Edit(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.SaveForm", "admin/forms/save", FormsMVC.Forms.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/forms/new", FormsMVC.Forms.New());
            context.MapRoute(null, "admin/form-{formId}/new-element", FormsMVC.Forms.NewElement());

            //widget routs
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "form-widget/update", FormsMVC.FormsBuilderWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}