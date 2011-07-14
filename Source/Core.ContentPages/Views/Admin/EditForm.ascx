<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.ContentPages.Models.ContentPageLocaleViewModel>" %>
<% using (Html.BeginForm())
   {%>
<div class="cols clrfix">
    <div class="fst_col colls_i" style="width: 60%;">
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Title)%>
        </div>
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Content)%>
        </div>
        <div class="i_form_i">
            <%:Html.AntiForgeryToken()%>
        </div>
    </div>
</div>
<div class="i_buttons clrfix">
    <div class="btn1 clrfix">
        <em></em>
        <%: Html.Submit("Save",new { @class="button"})%>
        <strong></strong>
    </div>
    <span>
        <%:Html.RouteLink("Cancel", new { controller = "ContentPage", action = "ShowAll"})%></span>
</div>
<% }%>