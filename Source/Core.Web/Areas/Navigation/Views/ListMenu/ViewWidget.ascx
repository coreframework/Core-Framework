<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.NHibernate.Models.Widgets.ListMenuWidget>" %>

<div class="list-menu-widget <%=Model.Orientation.ToString().ToLower()%>">
    <%foreach (var item in Model.Pages)  {%>
        <%=Html.ActionLink(Html.Encode(item.Title), MVC.Pages.Show(item.Url))%>
    <% }%>
</div>