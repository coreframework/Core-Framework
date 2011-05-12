<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Import Namespace="Core.ContentPages.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.ContentPages.Models.ContentViewerWidgetModel>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
        <label>
            Content page</label>
        <%:Html.DropDownListFor(model => model.ContentPageId, new SelectList(Model.ContentPages, "Id", "Title", Model.ContentPageId), "Please select", new { Class = "select-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.ContentPageId) %>
    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>
