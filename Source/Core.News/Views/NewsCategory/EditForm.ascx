<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.CategoryLocaleViewModel>" %>
<% using (Html.BeginForm("Edit", "Category", new { id = Model.CategoryId }))
   {%>
<div class="cols clrfix">
    <div class="fst_col colls_i">        
        <%:Html.HiddenFor(model => model.SelectedCulture) %>
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Title)%>
        </div>
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Description)%>
        </div>
        <div class="i_form_i">
            <%:Html.AntiForgeryToken()%>
        </div>
    </div>
</div>
<div class="i_buttons clrfix">
    <div class="btn1 clrfix">
        <em></em>
        <%: Html.Submit(Html.Translate(".Save"),new { @class="button"})%>
        <strong></strong>
    </div>
    <span>
        <%:Html.RouteLink(Html.Translate(".Cancel"), new { controller = "NewsCategory", action = "ShowAll" })%></span>
</div>
<% }%>
