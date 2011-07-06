<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="site_links" class="right_side clrfix">
        <% if (Request.IsAuthenticated)
           { %>
			    <span><%: Page.User.Identity.Name %></span>
                <div class="btn_login">
                    <em></em>
                        <%: Html.LinkButton("Sign Out", MVC.Users.DeleteUserSession())%>
                    <strong></strong>
                </div>
        <% } else { %>
                <div class="btn_login">
                    <em></em>
                        <%: Html.Submit(Html.Translate(".SignIn"), MVC.Users.NewUserSession())%>
                    <strong></strong>
                </div>
        <% } %>
</div>
