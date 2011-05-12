<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageLookAndFeelModel>" %>
<%@ Import Namespace="Framework.Core" %>
<div id="lookAndFeelForm">
    <% using (Ajax.BeginForm("UpdatePageLookAndFeel", "Pages", new AjaxOptions { UpdateTargetId = "lookAndFeelForm", OnSuccess = "updateLookAndFeelForm" }))
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
        <%:Html.HiddenFor(model => model.PageId)%>
        <fieldset>
            <label>
                Background color</label>
            <%:Html.TextBoxFor(model => model.BackgroundColor, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all", Attr = "background-color", Readonly = "readonly" })%>
            <label>
                Font</label>
            <%:Html.DropDownListFor(model => model.FontFamily, new SelectList(new List<String> { "Arial", "Tahoma", "Verdana" }), "Please select", new { Class = "select-400 text ui-widget-content ui-corner-all", Attr = "font-family" })%>
            <label>
                Font size</label>
            <%:Html.TextBoxFor(model => model.FontSizeValue, new { Class = "text-282 text ui-widget-content ui-corner-all", Attr = "font-size" })%>
            <%:Html.DropDownListFor(model => model.FontSizeUnit, new SelectList(new List<String> { "px", "em", "pt", "%" }), "Please select", new { Class=" text ui-widget-content ui-corner-all", Attr = "font-size", style = "margin-left:5px;" })%>
            <label>
                Font color</label>
            <%:Html.TextBoxFor(model => model.Color, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all", Attr = "color", Readonly = "readonly" })%>
            <label>
                Other</label>
            <%:Html.TextAreaFor(model => model.OtherStyles, new { Class = "text-400 text ui-widget-content ui-corner-all" })%>
        </fieldset>
        <%:Html.AntiForgeryToken()%>
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
