<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%if (Model.Id > 0) {%>
        <%:Html.Translate(".EditForm") %>
    <%} else {%>
         <%:Html.Translate(".NewForm") %>
    <% }%>
  </h1>
 <%Html.RenderAction(FormsMVC.Forms.FormTabs(Model.Id, true, false, false));%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm())
    {%>    
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
                <%:Html.HiddenFor(model => model.Id)%>     
			    <div class="i_form_i">
                    <%:Html.LocalizedLabelFor(model=>model.Title) %>
                    <%:Html.TextBoxFor(model => model.Title)%>
                    <%:Html.ValidationMessageFor(model => model.Title) %>
                </div>
                <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.ShowSubmitButton)%>
                    <%:Html.LocalizedLabelFor(model=>model.ShowSubmitButton) %>
                </div>
                <div class="i_form_i" id="submit_button_area" style="display:none;">
                    <%:Html.LocalizedLabelFor(model=>model.SubmitButtonText) %>
                    <%:Html.TextBoxFor(model => model.SubmitButtonText)%>
                    <%:Html.ValidationMessageFor(model => model.SubmitButtonText)%>
                </div>
                <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.ShowResetButton)%>
                    <%:Html.LocalizedLabelFor(model => model.ShowResetButton)%>
                </div>
                <div class="i_form_i" id="reset_button_area" style="display:none;">
                    <%:Html.LocalizedLabelFor(model=>model.ResetButtonText) %>
                    <%:Html.TextBoxFor(model => model.ResetButtonText)%>
                    <%:Html.ValidationMessageFor(model => model.ResetButtonText)%>
                </div>
			    <div class="i_form_i">
                <%:Html.AntiForgeryToken()%>
                </div>
            </div>
        </div>
	    <div class="i_buttons clrfix">
        <%if (Model.AllowManage){%>
		    <div class="btn1 clrfix">
                <em></em>
                <%:Html.Submit(Html.Translate("Actions.Save"), new {@class = "button"})%>
                <strong></strong>
            </div>
            <%if (Model.Id > 0) {%>
                <span style="margin-right:10px;"><%:Html.ActionLink(Html.Translate("Actions.Remove"), FormsMVC.Forms.Remove(Model.Id))%></span>
            <%}%>
            <%}%>
		    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Forms", action = "ShowAll" })%></span>
	    </div>
    </div>
        <script type="text/javascript">
            jQuery(function () {
                function checkForm() {
                    if ($('#ShowSubmitButton').attr('checked')) {
                        $('#submit_button_area').show();
                    }
                    else {
                        $('#submit_button_area').hide();
                    }
                    if ($('#ShowResetButton').attr('checked')) {
                        $('#reset_button_area').show();
                    }
                    else {
                        $('#reset_button_area').hide();
                    }
                }
                checkForm();
                $('#ShowSubmitButton').bind('change', checkForm);
                $('#ShowResetButton').bind('change', checkForm);
            });
    </script>
<% }%>
</asp:Content>