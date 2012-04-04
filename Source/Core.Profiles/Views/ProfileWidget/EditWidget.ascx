﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Profiles.Models.ProfileWidgetEditModel>" %>

<%@ Assembly Name="Core.Profiles" %>
<%@ Assembly Name="Core.Profiles.NHibernate" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.DisplayMode)%>
        <%:Html.DropDownListFor("DisplayMode", Model.DisplayMode, new { })%>
        <%:Html.ValidationMessageFor(model => model.DisplayMode)%>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>