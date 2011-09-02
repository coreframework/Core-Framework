<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.ContentPages.Models.ContentViewerWidgetModel>" %>
<%:Html.RegisterScript("test.js")%>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
       <label>Content page</label><br/>
        <%:Html.DropDownListFor(model => model.ContentPageId, new SelectList(Model.ContentPages, "Id", "Title", Model.ContentPageId), "Please select", new {})%>
        <%:Html.ValidationMessageFor(model=>model.ContentPageId) %>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
