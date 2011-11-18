<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.CategoryViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionId)%>
                <%if (Model.Id>0) {%>
                    <%:Html.HiddenFor(model=>model.SectionId)%>
                    <%:Html.Label(Model.SectionName) %>
                <% } else {%>
                    <%:Html.DropDownListFor(model => model.SectionId,
                                                             new SelectList(Model.Sections, "Id", "CurrentLocale.Title",
                                                                            Model.SectionId), "Please select", new {})%>
                <% }%>
                <%:Html.ValidationMessageFor(model=>model.SectionId) %>
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
                <%:Html.LocalizedLabelFor(model => model.Status)%>
                <%:Html.DropDownListFor(model => model.Status)%>
                <%:Html.ValidationMessageFor(model=>model.Status) %>
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
                <span><%:Html.ActionLink(Html.Translate("Actions.Remove"), WebContentMVC.WebContentCategory.Remove(Model.Id))%></span>
            <%}%>
         <%}%>
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "WebContentCategory", action = "Show" })%></span>
    </div>
</div>
