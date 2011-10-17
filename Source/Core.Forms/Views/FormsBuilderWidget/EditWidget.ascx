<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.Models.FormBuilderWidgetViewModel>" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.Title)%><br/>
        <%:Html.TextBoxFor(model => model.Title, new { Class = "inp_txt" })%>
        <%:Html.ValidationMessageFor(model=>model.Title) %>
    </div>
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.FormId)%><br/>
        <%:Html.DropDownListFor(model => model.FormId, new SelectList(Model.Forms, "Id", "Title", Model.FormId), "Please select", new {})%>
        <%:Html.ValidationMessageFor(model=>model.FormId) %><br/>
    </div>
     <div class="form_i">
        <%:Html.CheckBoxFor(model => model.SendEmail)%>
        <%:Html.LocalizedLabelFor(model => model.SendEmail, new { Class = "checkbx-label" })%>
     </div>
     <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.RecipientEmail)%><br/>
        <%:Html.TextBoxFor(model => model.RecipientEmail, new { Class = "inp_txt" })%>
        <%:Html.ValidationMessageFor(model => model.RecipientEmail)%>
     </div>
      <div class="form_i" style="margin:0;">
         <%:Html.CheckBoxFor(model => model.SaveData)%>
         <%:Html.LocalizedLabelFor(model => model.SaveData, new { Class = "checkbx-label" })%>
      </div>
    <%:Html.AntiForgeryToken()%>
</div>


