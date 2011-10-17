using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Framework.Mvc.Routing;

namespace Core.Forms
{
    [Export(typeof(AreaRegistration)), ExportMetadata("Order", 1)]
    public class ModuleAreaRegistration : AreaRegistration
    {
        public override String AreaName
        {
            get { return "Forms"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin.Forms", "admin/forms", FormsMVC.Forms.ShowAll(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.Forms.DynamicGridData", "admin/forms/DynamicGridData", new { controller = "Forms", action = "DynamicGridData", id = String.Empty, area = AreaName });
            context.MapRoute(null, "admin/form/permissions/{formId}", new { controller = "Forms", action = "ShowPermissions", area = AreaName, formId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/forms/apply-permissions", FormsMVC.Forms.ApplyPermissions(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute(null, "admin/form/form-elements/{formId}", new { controller = "Forms", action = "ShowFormElements", area = AreaName, formId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/form-{formId}/FormElementsDynamicGridData", new { controller = "Forms", action = "FormElementsDynamicGridData", id = String.Empty , area=AreaName });
            context.MapRoute(null, "admin/forms/update-form-elements-positions", FormsMVC.Forms.UpdateFormElementPosition());

            context.MapRoute(null, "admin/forms/edit-{formId}", new { controller = "Forms", action = "Edit", area = AreaName, formId = UrlParameter.Optional }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute("Admin.ChangeFormLanguage", "admin/forms/change-language", new { controller = "Forms", action = "ChangeLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.SaveForm", "admin/forms/save", FormsMVC.Forms.Save(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "admin/forms/new", FormsMVC.Forms.New());
            context.MapRoute("Admin.RemoveForm", "admin/forms/remove-{id}", FormsMVC.Forms.Remove());

            context.MapRoute(null, "admin/form/new-element/{formId}", new { controller = "Forms", action = "NewElement", area = AreaName, formId = UrlParameter.Optional });
            context.MapRoute(null, "admin/form-{formId}/edit-element-{formElementId}", new { controller = "Forms", action = "EditElement", area = AreaName, formId = UrlParameter.Optional, formElementId = UrlParameter.Optional });
            context.MapRoute("Admin.ChangeFormElementLanguage", "admin/forms/form-element/change-language", new { controller = "Forms", action = "ChangeFormElementLanguage", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.SaveFormElement", "admin/forms-{formId}/save-element", new { controller = "Forms", action = "SaveElement", id = String.Empty }, new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute("Admin.RemoveFormElement", "admin/forms-element/remove-{id}", FormsMVC.Forms.RemoveElement());

            context.MapRoute(null, "admin/form-tabs", FormsMVC.Forms.FormTabs(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });

            context.MapRoute("Admin.FormsAnswers", "admin/forms-answers", FormsMVC.FormAnswers.ShowAll(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Get) });
            context.MapRoute(null, "admin/forms-answers/DynamicGridData", FormsMVC.FormAnswers.FormAnswersDynamicGridData());

            context.MapRoute("Admin.FormAnswers", "admin/form-answers/{formWidgetId}", new { controller = "FormAnswers", action = "ShowAnswers", area = AreaName, formWidgetId = UrlParameter.Optional });
            context.MapRoute(null, "admin/forms-answer-details/{answerId}", new { controller = "FormAnswers", action = "ShowAnswerDetails", area = AreaName, answerId = UrlParameter.Optional });

            //widget routs
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.ViewWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "form-widget/submit-form", FormsMVC.FormsBuilderWidget.SubmitWidgetForm(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, String.Empty, FormsMVC.FormsBuilderWidget.EditWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
            context.MapRoute(null, "form-widget/update", FormsMVC.FormsBuilderWidget.UpdateWidget(), new { httpVerbs = new HttpVerbConstraint(HttpVerbs.Post) });
        }
    }
}