﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Admin.Models.PluginViewModel>" %>

<% using (Html.BeginForm("Update", "Module", new { id = Model.Id }))
   {%>
<div class="cols clrfix">
    <div class="fst_col colls_i" style="width: 60%;">
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
        <%: Html.Submit("Save",new { @class="button"})%>
        <strong></strong>
    </div>
    <span>
        <%:Html.RouteLink("Cancel", new { controller = "Module", action = "Index" })%></span>
</div>
<% }%>