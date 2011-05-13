<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="Framework.MVC.Extensions" %>
<div id="site_links">
    <div class="user-menu">
        <% if (Request.IsAuthenticated)
           { %>
        <span class="button"><a class="icon icon-close" href="<%= Url.Action(MVC.Users.DeleteUserSession()) %>">
        </a></span>
        <%: Page.User.Identity.Name %>
        <% }
           else
           { %>
        <%: Html.ActionLink(Html.Translate(".SignIn"), MVC.Users.NewUserSession()) %>
        <% } %>
    </div>
</div>
