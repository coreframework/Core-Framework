<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.NavigationMenuWidgetModel>"  %>
<%@ Import Namespace="Framework.Mvc.Helpers" %>
<%@ Import Namespace="Framework.Mvc.Extensions" %>
<div class="form_area">
  <%: Html.ValidationSummary(true) %>
   <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
   
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model=>model.Orientation) %><br/>
        <%:Html.DropDownListFor("Orientation", Model.Orientation, new {})%>
        <%:Html.ValidationMessageFor(model=>model.Orientation) %>
    </div>
  <%:Html.AntiForgeryToken()%>
</div>

