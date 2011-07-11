<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.Models.FormBuilderWidgetViewModel>" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <%:Html.LabelFor(model => model.Title)%><br/>
        <%:Html.TextBoxFor(model => model.Title, new { Class = "inp_txt" })%>
        <%:Html.ValidationMessageFor(model=>model.Title) %>
    </div>
    <div class="form_i">
       <label><%: Html.Translate(".Form") %></label><br/>
        <%:Html.DropDownListFor(model => model.FormId, new SelectList(Model.Forms, "Id", "Title", Model.FormId), "Please select", new {})%>
        <%:Html.ValidationMessageFor(model=>model.FormId) %><br/>
    </div>
     <div class="form_i">
        <%:Html.CheckBoxFor(model => model.SendEmail)%>
         <label class="checkbx-label"><%: Html.Translate(".SendEmail") %></label>
     </div>
     <div class="form_i">
        <%:Html.LabelFor(model => model.SenderEmail)%><br/>
        <%:Html.TextBoxFor(model => model.SenderEmail, new { Class = "inp_txt" })%>
        <%:Html.ValidationMessageFor(model=>model.SenderEmail) %>
     </div>
      <div class="form_i" style="margin:0;">
         <%:Html.CheckBoxFor(model => model.SaveData)%>
         <label class="checkbx-label"><%: Html.Translate(".SaveData") %></label>
      </div>
    <%:Html.AntiForgeryToken()%>
</div>


