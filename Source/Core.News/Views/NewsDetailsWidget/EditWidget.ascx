<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsDetailsWidgetEditModel>" %>

<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
       <label><%=Html.Translate(".LinkMode") %></label>
        <%:Html.CheckBoxFor(model => model.LinkByUrl)%>
        <%:Html.ValidationMessageFor(model => model.LinkByUrl)%>        
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
