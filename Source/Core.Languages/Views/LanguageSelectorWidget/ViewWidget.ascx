<%@ Assembly Name="Core.Languages.NHibernate" %>
<%@ Import Namespace="Core.Languages.NHibernate.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Language>>" %>

<div class="list-menu-widget horizontal">
    <%foreach (var item in Model)  {%>
        <%=Html.ActionLink(Html.Encode(item.Title), "javascript:void(0);")%>
    <% }%>
</div>