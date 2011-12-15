<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.ProfileWidgetViewModel>" %>
<%@ Assembly Name="Core.Profiles" %>
<%@ Import Namespace="Core.Profiles.Extensions" %>
<%@ Import Namespace="Core.Profiles.NHibernate.Static" %>

<div id="profile-form<%=Model.PageWidgetId %>" class="register-widget">
    <div>
    <% if (Request.IsAuthenticated) { %>
        <% using (Ajax.BeginForm(
                        "SaveUser",
                        "ProfileWidget",
                        new { area = "Profiles" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("profile-form{0}", Model.PageWidgetId),
                            }))
           {%>
        <h3>
        <%: Html.Translate(".Profile")%></h3>
        <%:Html.Messages() %>
        <%:Html.HiddenFor(model => model.PageWidgetId) %>
        <%:Html.HiddenFor(model => model.ProfileTypeId) %>

        <%if (Model.Widget.DisplayMode != ProfileWidgetDisplayMode.ProfileDetails) {%>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.Email)%><br />
            <%:Html.TextBoxFor(model => model.Email, new {Class = "inp_txt w_365"})%>
            <%:Html.ValidationMessageFor(model => model.Email)%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.Username)%><br />
            <%:Html.TextBoxFor(model => model.Username, new {Class = "inp_txt w_365"})%>
            <%:Html.ValidationMessageFor(model => model.Username)%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.Password)%><br />
            <%:Html.PasswordFor(model => model.Password, new {Class = "inp_txt w_365"})%>
            <%:Html.ValidationMessageFor(model => model.Password)%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model => model.PasswordConfirmation)%><br />
            <%:Html.PasswordFor(model => model.PasswordConfirmation, new {Class = "inp_txt w_365"})%>
            <%:Html.ValidationMessageFor(model => model.PasswordConfirmation)%>
        </div>
        <% } %>
        <%if (Model.Widget.DisplayMode != ProfileWidgetDisplayMode.CommonDetails)
          {%>
            <%if (Model.Profile != null && Model.Profile.ProfileType != null)
              {%>
                <%foreach (var item in (Model.Profile.ProfileType.ProfileHeaders.OrderBy(header => header.OrderNumber)))
                  {%>
                <div <%=item.ShowOnMemberProfile? "class=accordion":String.Empty%>>
                    <%if (item.ShowOnMemberProfile)
                      { %>
                        <%:Html.ProfileHeaderRenderer(item)%>
                    <%}%>
                    <div>
                        <%foreach (var element in (item.ProfileElements.OrderBy(element => element.OrderNumber)))
                          {%>
                            <%if (element.ShowOnMemberProfile)
                              { %>
                               <div class="form_i">
                                    <%:Html.ProfileElementRenderer(element, ViewData[String.Format("FormCollection{0}", Model.Widget.Id)] as FormCollection, Model.Profile.ProfileElements.FirstOrDefault(el => el.ProfileElement.Id == element.Id))%>
                                </div>
                            <%}%>   
                        <%}%>
                    </div>
                  </div>
              <%}%>
            <%}%>
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
            <%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%>
            <strong></strong>
        </div>
        <%: Html.AntiForgeryToken() %>
        <% } %>
        <div class="clear">
        </div>
   <% } %>
    </div>
</div>
