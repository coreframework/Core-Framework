<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.SectionViewModel>" %>
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
                <%:Html.LocalizedLabelFor(model=>model.Description) %>
                <%:Html.TextAreaFor(model => model.Description)%>
                <%:Html.ValidationMessageFor(model => model.Description)%>
            </div>
            <h3><%:Html.Translate("Settings", "WebContent.Views.Section")%>:</h3>
            <%:Html.HiddenFor(model=>model.SectionSettings.Id) %> 
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowTitle)%>
                <%:Html.DropDownListFor("SectionSettings.ShowTitle", Model.SectionSettings.ShowTitle, new { })%>
            </div>
                <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionSettings.TitleLinkable)%>
                 <%:Html.DropDownListFor("SectionSettings.TitleLinkable", Model.SectionSettings.TitleLinkable, new { })%>
            </div>
                <div class="i_form_i">
                  <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowSummaryText)%>
                <%:Html.DropDownListFor("SectionSettings.ShowSummaryText", Model.SectionSettings.ShowSummaryText, new {})%>
            </div>
              <div class="i_form_i">
                  <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowContent)%>
                  <%:Html.DropDownListFor("SectionSettings.ShowContent", Model.SectionSettings.ShowContent, new { })%>
            </div>
                <div class="i_form_i">
                 <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowSection)%>
                <%:Html.DropDownListFor("SectionSettings.ShowSection", Model.SectionSettings.ShowSection, new { })%>
            </div>
                <div class="i_form_i">
                  <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowCategory)%>
                <%:Html.DropDownListFor("SectionSettings.ShowCategory", Model.SectionSettings.ShowCategory, new { })%>
            </div>
                <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowAuthor)%>
                 <%:Html.DropDownListFor("SectionSettings.ShowAuthor", Model.SectionSettings.ShowAuthor, new { })%>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowCreatedDate)%>
                 <%:Html.DropDownListFor("SectionSettings.ShowCreatedDate", Model.SectionSettings.ShowCreatedDate, new { })%>
            </div>
            <div class="i_form_i">
                 <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowModifiedDate)%>
                 <%:Html.DropDownListFor("SectionSettings.ShowModifiedDate", Model.SectionSettings.ShowModifiedDate, new { })%>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowDownloadLink)%>
                 <%:Html.DropDownListFor("SectionSettings.ShowDownloadLink", Model.SectionSettings.ShowDownloadLink, new { })%>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.SectionSettings.AlternativeReadMoreText) %>
                <%:Html.TextBoxFor(model => model.SectionSettings.AlternativeReadMoreText)%>
                <%:Html.ValidationMessageFor(model => model.SectionSettings.AlternativeReadMoreText)%>
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
                <span><%:Html.ActionLink(Html.Translate("Actions.Remove"), WebContentMVC.Section.Remove(Model.Id))%></span>
            <%}%>
         <%}%>
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Section", action = "Show" })%></span>
    </div>
</div>
