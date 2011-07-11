<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="r_info">
<% if (Request.IsAuthenticated) { %>
    <span class="cp_link"><%: Html.ActionLink(Html.Translate(".ControlPanel"), MVC.Admin.AdminHome.Index()) %></span>
	<span><%: Page.User.Identity.Name %></span> 
	<a href="<%= Url.Action(MVC.Users.DeleteUserSession()) %>"><%: Html.Translate(".SignOut") %></a>
<% } else { %> 
     <%: Html.ActionLink(Html.Translate(".SignIn"), MVC.Users.NewUserSession()) %>
<% } %>
</div>
