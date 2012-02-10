<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.FormLogin.Models.LoginWidgetViewModel>" %>
<%@ Assembly Name="Core.FormLogin" %>
<%@ Import Namespace="Framework.Mvc.Extensions" %>
<div id="login-form<%=Model.PageWidgetId %>" class="login-widget test-login">
    <% if (Request.IsAuthenticated)
       { %>
    <div class="r_info">
        <span class="cp_link">
            <%: Html.ActionLink(Html.Translate(".ControlPanel"), MVC.Admin.AdminHome.Index()) %></span>
        <span>
            <%: Page.User.Identity.Name %></span> <a href="<%= Url.Action("DeleteUserSession", "LoginWidget", new { area = "FormLogin" } ) %>">
                <%: Html.Translate(".SignOut") %></a>
    </div>
    <% }
       else
       { %>
    <div class="form_area">
        <% using (Ajax.BeginForm(
                        "CreateUserSession",
                        "LoginWidget",
                        new { area = "FormLogin" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("login-form{0}", Model.PageWidgetId),
                                OnComplete = "loginCompleted"
                            }))
           {%>
        <%if (Model.ShowTitle)
          {%>
        <h3>
            <%:Html.Translate(".Title")%></h3>
        <%
}%>
        <%:Html.Messages() %>
        <%:Html.HiddenFor(model => model.PageWidgetId) %>
        <%:Html.HiddenFor(model => model.ShowTitle) %>
        <input id="IsSuccessfulLogin" type="hidden" value="<%=Model.IsSuccessfulLogin %>" />
        <div class="form_i">
            <label>
                <%:Html.Translate(".UsernameOrEmail")%></label><br />
            <%: Html.TextBoxFor(model => model.UsernameOrEmail, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.UsernameOrEmail)%>
        </div>
        <div class="form_i">
            <label>
                <%:Html.Translate(".Password")%></label><br />
            <%: Html.PasswordFor(model => model.Password, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.Password)%>
        </div>
        <div class="form_i">
            <%: Html.CheckBoxFor(model => model.RememberMe) %>
            <label>
                <%:Html.Translate(".RememberMe")%></label>
        </div>
        <p>
            <div class="btn1">
                <em></em>
                <%: Html.Submit(Html.Translate(".SignIn"), new { Class = "button" })%><strong></strong></div>
        </p>
        <%: Html.AntiForgeryToken() %>
        <% } %>
        <div class="clear">
        </div>
    </div>
    <script type="text/javascript">
        function loginCompleted(result) {
            if ($('input[type=hidden]#IsSuccessfulLogin', result.get_data()).val().toLowerCase() === 'true') {
                location.href = location.href;
            }
        }
    </script>
    <% } %>
</div>
