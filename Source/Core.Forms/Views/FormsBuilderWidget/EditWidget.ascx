<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Import Namespace="Core.Forms.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.Models.FormBuilderWidgetViewModel>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
        <%:Html.LabelFor(model => model.Title)%>
        <%:Html.TextBoxFor(model => model.Title, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.Title) %>

        <label><%: Html.Translate(".Form") %></label>
        <%:Html.DropDownListFor(model => model.FormId, new SelectList(Model.Forms, "Id", "Title", Model.FormId), "Please select", new { Class = "select-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.FormId) %><br/>

         <%:Html.CheckBoxFor(model => model.SaveData)%>
         <label class="checkbx-label">Save data</label><br/>

         <%:Html.CheckBoxFor(model => model.SendEmail)%>
         <label class="checkbx-label">Send email</label><br/>

        <%:Html.LabelFor(model => model.SenderEmail)%>
        <%:Html.TextBoxFor(model => model.SenderEmail, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.SenderEmail) %>

    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>


