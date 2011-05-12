<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.Areas.Navigation.Models.BreadcrumbsItem>>" %>

<ul class="breadcrumbs">
    <%foreach (var item in Model) {%>
        <li <%=item==Model.Last()?"class=\"last\"":""%>>
            <%=Html.ActionLink(Html.Encode(item.Title), item.IsHomePage?MVC.Home.Index():MVC.Pages.Show(item.Url))%>
        </li>
    <% }%>
</ul>
