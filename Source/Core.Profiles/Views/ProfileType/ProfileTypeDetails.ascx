<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.ProfileTypeViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <h3><%:Html.Translate("Common", "WebContent.Views.Section")%>:</h3>
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>        
		    <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Title) %>
                <%:Html.TextBoxFor(model => model.Title)%>
                <%:Html.ValidationMessageFor(model => model.Title) %>
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
            <%if (Model.Id > 0) {%>
                <span><%:Html.ActionLink(Html.Translate("Actions.Remove"), ProfilesMVC.ProfileType.Remove(Model.Id))%></span>
            <%}%>
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "ProfileType", action = "Show" })%></span>
    </div>
</div>

