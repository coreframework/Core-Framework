<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="site_links" class="right_side clrfix">
        <% if (Request.IsAuthenticated)
           { %>
			    <span><%: Page.User.Identity.Name %></span>
                <a href="<%= Url.Action(MVC.Users.DeleteUserSession()) %>"><span class="sign"><%:Html.Translate(".SignOut")%></span></a>
        <% } else { %>
                <a class="sign" href="<%= Url.Action(MVC.Users.NewUserSession()) %>"><%:Html.Translate(".SignIn")%></a>
        <% } %>
</div>
