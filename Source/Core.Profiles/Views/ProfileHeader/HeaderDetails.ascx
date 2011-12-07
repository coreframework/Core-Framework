<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.ProfileHeaderViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>
		    <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Title) %>
                <%:Html.TextBoxFor(model => model.Title)%>
                <%:Html.ValidationMessageFor(model => model.Title) %>
            </div>
            <div class="i_form_i">
                <%:Html.CheckBoxFor(model => model.ShowOnMemberProfile)%>
                <%:Html.LocalizedLabelFor(model => model.ShowOnMemberProfile)%>
            </div>
             <div class="i_form_i">
                <%:Html.CheckBoxFor(model => model.ShowOnMemberRegistration)%>
                <%:Html.LocalizedLabelFor(model => model.ShowOnMemberRegistration)%>
            </div>
		    <div class="i_form_i">
            <%:Html.AntiForgeryToken()%>
            </div>
        </div>
    </div>
    <div class="i_buttons clrfix">
        <div class="btn1 clrfix">
            <em></em>
            <%:Html.Submit(Html.Translate("Actions.Save"), new {@class = "button"})%>
            <strong></strong>
        </div>
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "ProfileElement", action = "Show" })%></span>
    </div>
</div>
