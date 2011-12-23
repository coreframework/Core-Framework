<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.LoginWorkflow.Models.LoginHolderWidgetViewModel>" %>
<%@ Assembly Name="Core.FormLogin" %>
<%@ Assembly Name="Core.OpenIDLogin" %>
<%@ Import Namespace="Core.FormLogin.Verbs.Widgets" %>
<%@ Import Namespace="Core.OpenIDLogin.Verbs.Widgets" %>
<div id="login-holder<%=Model.PageWidgetId %>">
    <div class="login-holder">
    </div>
    <% if (!Request.IsAuthenticated)
       { %>
    <div class="additional-logins">
        <ul>
            <li id="formLogin" style="display: none"><a href="javascript:void(0);" class="changeLoginForm">
                Sign in with form login</a><div style="display: none">
                    <% Html.RenderAction(LoginWidgetViewerVerb.Instance.Action,
                                LoginWidgetViewerVerb.Instance.Controller,
                                new
                                    {
                                        instance = Model.FormLoginWidgetInstance,
                                        Area = LoginWidgetViewerVerb.Instance.Area
                                    });%></div>
            </li>
            <li id="openIDLogin"><a href="javascript:void(0);" class="changeLoginForm"><span></span>
                Sign in with openID</a><div style="display: none">
                    <% Html.RenderAction(OpenIDLoginWidgetViewerVerb.Instance.Action,
                                OpenIDLoginWidgetViewerVerb.Instance.Controller,
                                new
                                    {
                                        instance = Model.OpenIdLoginWidgetInstance,
                                        Area = OpenIDLoginWidgetViewerVerb.Instance.Area
                                    });%></div>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(function () {
            var $loginHolder = $('#login-holder<%=Model.PageWidgetId %> .login-holder');
            var $formLogin = $('#login-holder<%=Model.PageWidgetId %> .additional-logins #formLogin');
            var $openIDLogin = $('#login-holder<%=Model.PageWidgetId %> .additional-logins #openIDLogin');
            $loginHolder.html($('div', $formLogin).html());
            $('a.changeLoginForm', $openIDLogin).click(function () {
                $loginHolder.html($('div', $openIDLogin).html());
                $openIDLogin.hide();
                $formLogin.show();
            });
            $('a.changeLoginForm', $formLogin).click(function () {
                $loginHolder.html($('div', $formLogin).html());
                $formLogin.hide();
                $openIDLogin.show();
            });
        });
    </script>
    <%}
       else
       {%>
    <% Html.RenderAction(LoginWidgetViewerVerb.Instance.Action,
                                LoginWidgetViewerVerb.Instance.Controller,
                                new
                                    {
                                        instance = Model.FormLoginWidgetInstance,
                                        Area = LoginWidgetViewerVerb.Instance.Area
                                    });%>
    <%
       }%>
</div>
