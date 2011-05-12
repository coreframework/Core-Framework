﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetCSSModel>" %>
<%@ Import Namespace="Framework.Core" %>
<%@ Import Namespace="Core.Web.Helpers" %>

<div id="CSSForm" style="margin-top:10px">
    <% using (Ajax.BeginForm("UpdateWidgetCSS", "Pages", new AjaxOptions { UpdateTargetId = "CSSForm", OnSuccess = "updateCSSForm" }))
       { %>
    <%if (TempData.ContainsKey(Constants.ActionResult) && TempData.ContainsKey(Constants.ActionResultMessage))
      {%>
    <div class="ui-widget">
        <div class="ui-state-highlight ui-corner-all" style="padding: 0pt 0.7em;">
            <p>
                <span class="ui-icon <%=TempData[Constants.ActionResult] is bool && (bool)TempData[Constants.ActionResult] ? "ui-icon-info" : "ui-icon-alert" %>"
                    style="float: left; margin-right: 0.3em;"></span>
                <%:Html.Translate(TempData[Constants.ActionResultMessage].ToString())%>
            </p>
        </div>
    </div>
    <%} %>
    <div class="form_area">
        <input id="SettingId" name="SettingId" type="hidden" value="<%=Model.SettingId %>" />
        <%:Html.HiddenFor(model => model.WidgetId)%>
        <%:Html.HiddenFor(model => model.PageCssModel.PageId)%>
        <input id="PageCssModel_SettingId" name="PageCssModel.SettingId" type="hidden" value="<%=Model.PageCssModel.SettingId %>" />
        <fieldset>
            <label>
                Custom CSS class names</label>
            <%:Html.TextBoxFor(model => model.CustomCSSClasses, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all" })%>
            <label>
                Custom CSS</label>
            <%:Html.TextAreaFor(model => model.PageCssModel.CustomCSS, new { Class = "text-400 text ui-widget-content ui-corner-all", Rows = 15 })%>
        </fieldset>
        <%:Html.AntiForgeryToken()%>
    </div>
    <div class="css-rule-constructor">
        <a href="javascript:void(0);" rule-for="#<%=WidgetHelper.GetWidgetClientId(Model.WidgetId) %>"><%=Html.Translate(".WidgetRule")%></a><br />
        <a href="javascript:void(0);" rule-for=".widget"><%=Html.Translate(".AllWidgetsRule")%></a>
    </div>
    <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
        <div class="ui-dialog-buttonset">
            <%: Html.Submit("Save", new { Class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" })%>
            <button type="button" role="button" aria-disabled="false" class="reset ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only">
                <span class="ui-button-text">Reset</span>
            </button>
        </div>
    </div>
    <% }%>
</div>
