<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.OpenIDLogin.Models.OpenIDLoginWidgetViewModel>" %>
<%@ Assembly Name="Core.OpenIDLogin" %>
<div id="openID-login-form<%=Model.PageWidgetId %>" class="openID-login-widget">
    <% if (Request.IsAuthenticated)
       { %>
    <div class="r_info">
        <span class="cp_link">
            <%: Html.ActionLink(Html.Translate(".ControlPanel"), MVC.Admin.AdminHome.Index()) %></span>
        <span>
            <%: Page.User.Identity.Name %></span> <a href="<%= Url.Action("DeleteUserSession", "OpenIDLoginWidget", new { area = "OpenIDLogin" } ) %>">
                <%: Html.Translate(".SignOut") %></a>
    </div>
    <% }
       else
       { %>
    <div class="form_area">
        <% using (Ajax.BeginForm(
                        "CreateUserSession",
                        "OpenIDLoginWidget",
                        new { area = "OpenIDLogin" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("openID-login-form{0}", Model.PageWidgetId),
                                OnComplete = "openIDLoginCompleted",
                                OnFailure = "onFailure"
                            }))
           {%>
        <%if (Model.ShowTitle)
          {%>
        <h3>
            <%:Html.Translate(".Title")%></h3>
        <%
            }%>
        <%:Html.Messages() %>
        <%:Html.HiddenFor(model => model.ShowTitle) %>
        <%:Html.HiddenFor(model => model.PageWidgetId) %>
        <input id="IsSuccessfulLogin" type="hidden" value="<%=Model.IsSuccessfulLogin %>" />
        <div class="form_i">
            <label>
                <%:Html.Translate(".UserOpenId")%></label><br />
            <%: Html.TextBoxFor(model => model.UserOpenId, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.UserOpenId)%>
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
        function openIDLoginCompleted(result) {
            alert(result.status);
            if ($('input[type=hidden]#IsSuccessfulLogin', result.get_data()).val().toLowerCase() === 'true') {
                location.href = location.href;
            }
        }

        function onFailure(ajaxContext) {
            alert('failure=' + ajaxContext.get_response().get_statusCode());
        }
    </script>
    <% } %>
</div>
