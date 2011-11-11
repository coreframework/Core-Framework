<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.ArticleViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionId)%>
                <%:Html.DropDownListFor(model => model.SectionId, new SelectList(Model.Sections, "Id", "CurrentLocale.Title", Model.SectionId), "Please select", new { })%>
                <%:Html.ValidationMessageFor(model=>model.SectionId) %>
            </div>  
		    <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Title) %>
                <%:Html.TextBoxFor(model => model.Title)%>
                <%:Html.ValidationMessageFor(model => model.Title) %>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Summary) %>
                <%:Html.TextAreaFor(model => model.Summary)%>
                <%:Html.ValidationMessageFor(model => model.Summary)%>
            </div>
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.Content)%>
            </div> 
               <div class="i_form_i">
                <%:Html.EditorFor(model => model.PublishedDate)%>
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
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Article", action = "Show" })%></span>
    </div>
</div>
