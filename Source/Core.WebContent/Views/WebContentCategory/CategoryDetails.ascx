<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.CategoryViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <h3><%:Html.Translate("Common", "WebContent.Views.Category")%>:</h3>
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>
            <div class="form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionId)%><br/>
                <%:Html.DropDownListFor(model => model.SectionId, new SelectList(Model.Sections, "Id", "Title", Model.SectionId), "Please select", new { })%>
                <%:Html.ValidationMessageFor(model=>model.SectionId) %><br/>
            </div>  
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
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Category", action = "Show" })%></span>
    </div>
</div>
