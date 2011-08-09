<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.Areas.Navigation.Models.BreadcrumbsItem>>" %>

<ul class="breadcrumbs">
    <%foreach (var item in Model) {%>
        <li>
            <%=Html.ActionLink(String.IsNullOrEmpty(item.Title) ? " " : Html.Encode(item.Title) , item.IsHomePage?MVC.Home.Index():MVC.Pages.Show(item.Url))%>
            <%=item != Model.Last() ? "&raquo;" : ""%>
        </li>
    <% }%>
</ul>
