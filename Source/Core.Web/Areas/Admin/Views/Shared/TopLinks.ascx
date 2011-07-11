<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="site_links" class="right_side clrfix">
        <% if (Request.IsAuthenticated)
           { %>
			    <span><%: Page.User.Identity.Name %></span>
                <a class="sign" href="<%= Url.Action(MVC.Users.DeleteUserSession()) %>">Sign Out</a>
<%--                <div class="btn_login">
                    <em></em> 
                        <input type="button" id="SignOut" value="Sign Out" />
                    <strong></strong>
                </div>--%>
        <% } else { %>
                <a class="sign" href="<%= Url.Action(MVC.Users.NewUserSession()) %>">Sign In</a>
<%--                <div class="btn_login">
                    <em></em>
                    <input type="button" id="SignIn" value="<%= Html.Translate(".SignIn") %>" />
                    <strong></strong>
                </div>--%>
        <% } %>
</div>
<%--
<script type="text/javascript">
    $(function () {
        $('#SignOut').click(function () { window.location = "<%= Url.Action(MVC.Users.DeleteUserSession()) %>"; });
        $('#SignIn').click(function () { window.location = "<%= Url.Action(MVC.Users.NewUserSession()) %>"; });
    });
</script>--%>