<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<MvcSiteMapProvider.Web.Html.Models.SiteMapHelperModel>" %>

<ul class="siteMap">
<% foreach (var node in Model.Nodes) { %>
    <li><%=Html.DisplayFor(m => node)%>
    <% if (node.Children.Any()) { %>
        <%=Html.DisplayFor(m => node.Children)%>
    <% } %>
    </li>
<% } %>
</ul>