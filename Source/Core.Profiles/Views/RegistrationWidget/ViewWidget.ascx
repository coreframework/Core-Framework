<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.RegistrationWidgetViewModel>" %>
<%@ Assembly Name="Core.Profiles" %>
<div id="register-form<%=Model.PageWidgetId %>" class="register-widget">
    <% if (!Request.IsAuthenticated)
       { %>
    <div class="form_area">
        <% using (Ajax.BeginForm(
                        "RegisterUser",
                        "RegistrationWidget",
                        new { area = "Profiles" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("register-form{0}", Model.PageWidgetId)
                            }))
           {%>
        <h3>
            <%: Html.Translate(".Title")%></h3>
        <%:Html.Messages() %>
        <%:Html.HiddenFor(model => model.PageWidgetId) %>
        <%:Html.HiddenFor(model => model.ProfileTypeId) %>
        <div class="form_i">
            <label>
                <%:Html.Translate(".Email")%></label><br />
            <%: Html.TextBoxFor(model => model.Email, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.Email)%>
        </div>
        <div class="form_i">
            <label>
                <%:Html.Translate(".Username")%></label><br />
            <%: Html.TextBoxFor(model => model.Username, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.Username)%>
        </div>
        <div class="form_i">
            <label>
                <%:Html.Translate(".Password")%></label><br />
            <%: Html.PasswordFor(model => model.Password, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.Password)%>
        </div>
        <div class="form_i">
            <label>
                <%:Html.Translate(".PasswordConfirmation")%></label><br />
            <%: Html.PasswordFor(model => model.PasswordConfirmation, new { Class = "inp_txt w_365" })%>
            <%:Html.ValidationMessageFor(model => model.PasswordConfirmation)%>
        </div>
        <p>
            <div class="btn1">
                <em></em>
                <%: Html.Submit(Html.Translate(".Register"), new { Class = "button" })%><strong></strong></div>
        </p>
        <%: Html.AntiForgeryToken() %>
        <% } %>
        <div class="clear">
        </div>
    </div>
    <% } %>
</div>
