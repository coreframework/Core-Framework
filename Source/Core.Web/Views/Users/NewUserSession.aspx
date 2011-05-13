﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Models.LoginViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.Translate(".Title")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="span-12 prepend-6 prepend-top">
        <div class="form login-form">
            <% using (Html.BeginForm(MVC.Users.CreateUserSession(), FormMethod.Post))
               {%>
            <fieldset style="padding: 1.4em">
                <legend>
                    <%: Html.Translate(".Title")%></legend>
                <div>
                    <label>
                        <%:Html.Translate(".UsernameOrEmail")%>
                    </label>
                    <%: Html.TextBoxFor(model => model.UsernameOrEmail) %>
                </div>
                <div>
                    <label>
                        <%:Html.Translate(".Password")%>
                    </label>
                    <%: Html.PasswordFor(model => model.Password)%>
                </div>
                <p>
                    <label>
                        <%:Html.Translate(".RememberMe")%>
                    </label>
                    <%: Html.CheckBoxFor(model => model.RememberMe) %>
                </p>
                <p>
                    <%: Html.HiddenSubmit(Html.Translate(".SignIn")) %>
                    <%: Html.LinkSubmitButton(Html.Translate(".SignIn"))%>
                </p>
                <%: Html.HiddenFor(model => model.ReturnUrl) %>
                <%: Html.AntiForgeryToken() %>
            </fieldset>
            <% } %>
        </div>
    </div>
</asp:Content>
