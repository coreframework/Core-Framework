<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.BreadcrumbsWidgetModel>" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
        <%:Html.CheckBoxFor(model => model.ShowHomePage)%>
        <label class="checkbx-label">Show Home Page</label>
        <%:Html.ValidationMessageFor(model => model.ShowHomePage)%>
    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>

