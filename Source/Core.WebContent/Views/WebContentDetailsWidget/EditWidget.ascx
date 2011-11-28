<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.DetailsWidgetEditModel>" %>
<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.LinkByUrl)%>
        <%:Html.CheckBoxFor(model => model.LinkByUrl)%>
        <%:Html.ValidationMessageFor(model => model.LinkByUrl)%>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
