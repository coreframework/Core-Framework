<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcSiteMapProvider.Web.Html.Models.SiteMapPathHelperModel>" %>
<ul>
<% foreach (var node in Model.Nodes) { %>
    <li>
    <% if (node==Model.Nodes.Last())
       { %>
        <%=node.Title.StartsWith("$t:") ? Html.Translate(node.Title.Replace("$t:", ""), node.Area) : Html.Encode(node.Title)%>
    <% }
       else
       {%>
       <a href="<%=node.Url%>"><%=node.Title.StartsWith("$t:") ? Html.Translate(node.Title.Replace("$t:", ""), node.Area) : Html.Encode(node.Title)%></a>
     <%}%>
    </li>
<% } %>
</ul>
