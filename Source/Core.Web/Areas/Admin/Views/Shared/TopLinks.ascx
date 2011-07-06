<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="site_links" class="right_side clrfix">
        <% if (Request.IsAuthenticated)
           { %>
			    <span><%: Page.User.Identity.Name %></span>
                <div class="btn_login">
                    <em></em> 
                        <input type="button" id="SignOut" value="Sign Out" />
                       <%-- <%: Html.LinkButton("Sign Out", Url.Action(MVC.Users.DeleteUserSession()), new { @id = "SignOutLink", @style = "display:none;" })%>--%>
                    <strong></strong>
                </div>
        <% } else { %>
                <div class="btn_login">
                    <em></em>
                    <input type="button" id="SignIn" value="<%= Html.Translate(".SignIn") %>" />
                    <%--<%: Html.LinkButton(Html.Translate(".SignIn"), Url.Action(MVC.Users.NewUserSession()),new { @id = "SignInLink", @style="display:none;" })%>--%>
                    <strong></strong>
                </div>
        <% } %>
</div>

<script type="text/javascript">
    $(function () {
        $('#SignOut').click(function () { window.location = "<%= Url.Action(MVC.Users.DeleteUserSession()) %>"; });
        $('#SignIn').click(function () { window.location = "<%= Url.Action(MVC.Users.NewUserSession()) %>"; });
    });
</script>