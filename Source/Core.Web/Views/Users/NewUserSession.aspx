<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Models.LoginViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.Translate(".Title")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-form">
     <div class="form_area">
            <% using (Html.BeginForm(MVC.Users.CreateUserSession(), FormMethod.Post))
               {%>
                <h3><%: Html.Translate(".Title")%></h3>
                <%: Html.ValidationSummary(true) %>
                <div class="form_i">
                    <label><%:Html.Translate(".UsernameOrEmail")%></label><br/>
                    <%: Html.TextBoxFor(model => model.UsernameOrEmail, new { Class = "inp_txt w_365" })%>
                </div>
                <div class="form_i">
                    <label><%:Html.Translate(".Password")%></label><br/>
                    <%: Html.PasswordFor(model => model.Password, new { Class = "inp_txt w_365" })%>
                </div>
                 <div class="form_i">
                    <%: Html.CheckBoxFor(model => model.RememberMe) %>
                    <label><%:Html.Translate(".RememberMe")%></label>
                </div>
                <p>
                    <div class="btn1"><em></em><%: Html.Submit(Html.Translate(".SignIn"), new { Class = "button" })%><strong></strong></div>
                </p>
                <%: Html.HiddenFor(model => model.ReturnUrl) %>
                <%: Html.AntiForgeryToken() %>
          
            <% } %>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
