<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.NHibernate.Models.Page>>" %>
<ul class="menu">
    <%foreach (var item in Model) {%>
       <li>
            <%=Html.ActionLink(Html.Encode(item.Title), MVC.Pages.Show(item.Url))%>
       </li>
    <%} %>
</ul>