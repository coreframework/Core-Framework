<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.RegistrationWidgetViewModel>" %>
<%@ Assembly Name="Core.Profiles" %>
<%@ Import Namespace="Core.Profiles.Extensions" %>

<div id="register-form<%=Model.PageWidgetId %>" class="register-widget">
    <div>
    <% if (!Request.IsAuthenticated) { %>
        <% using (Ajax.BeginForm(
                        "RegisterUser",
                        "RegistrationWidget",
                        new { area = "Profiles" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("register-form{0}", Model.PageWidgetId),
                                OnComplete = "registrationCompleted"
                            }))
           {%>
        <h3>
            <%: Html.Translate(".Title")%></h3>
        <%:Html.Messages() %>
        <%:Html.HiddenFor(model => model.PageWidgetId) %>
        <%:Html.HiddenFor(model => model.ProfileTypeId) %>
        <input id="IsSuccessfulRegistration" type="hidden" value="<%=Model.IsSuccessfulRegistration %>" />
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
        <%foreach (var item in (Model.Widget.ProfileType.ProfileHeaders.OrderBy(header=>header.OrderNumber))) {%>
            <div <%=item.ShowOnMemberRegistration? "class=accordion":String.Empty%>>
                <%if (item.ShowOnMemberRegistration) { %>
                    <%:Html.ProfileHeaderRenderer(item)%>
                <%}%>
                <div>
                    <%foreach (var element in (item.ProfileElements.OrderBy(element=>element.OrderNumber))) {%>
                        <%if (element.ShowOnMemberRegistration)
                          { %>
                           <div class="form_i">
                                <%:Html.ProfileElementRenderer(element, ViewData[String.Format("FormCollection{0}", Model.Widget.Id)] as FormCollection)%>
                            </div>
                        <%}%>   
                    <%}%>
                </div>
              </div>
          <%}%>
        <script type="text/javascript">
            $(function () {
                $(".accordion").accordion('destroy').accordion({
                    collapsible: true,
                    autoHeight: false
                });
            });
        </script>
        <div class="btn1">
            <em></em>
            <%: Html.Submit(Html.Translate(".Register"), new { Class = "button" })%>
            <strong></strong>
        </div>
        <%: Html.AntiForgeryToken() %>
        <% } %>
        <div class="clear">
        </div>
        <script type="text/javascript">
              function registrationCompleted(result) {
                  if ($('input[type=hidden]#IsSuccessfulRegistration', result.get_data()).val().toLowerCase() === 'true') {
                      location.href = location.href;
                  }
              }
       </script>
   <% } %>
    </div>
</div>
