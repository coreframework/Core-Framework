<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="user-menu">
<% if (Request.IsAuthenticated) { %>
    <span class="button">
      <a class="icon icon-close" href="<%= Url.Action(MVC.Users.DeleteUserSession()) %>"></a>
    </span>
    <%: Page.User.Identity.Name %>
<% } else { %> 
     <%: Html.ActionLink(Html.Translate(".SignIn"), MVC.Users.NewUserSession()) %>
<% } %>
</div>