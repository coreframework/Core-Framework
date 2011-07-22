<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageLookAndFeelModel>" %>
<div id="lookAndFeelForm">
<% using (Ajax.BeginForm("UpdatePageLookAndFeel", "Pages", new AjaxOptions { UpdateTargetId = "lookAndFeelForm", OnSuccess = "updateLookAndFeelForm" }))
    { %>
    <%:Html.Messages() %>
    <div class="form_area">
        <input id="SettingId" name="SettingId" type="hidden" value="<%=Model.SettingId %>" />
        <%:Html.HiddenFor(model => model.PageId)%>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.BackgroundColor)%><br/>
            <%:Html.TextBoxFor(model => model.BackgroundColor, new { Class = "inp_txt colorPicker", Attr = "background-color", Readonly = "readonly" })%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model => model.FontFamily)%><br/>
            <%:Html.DropDownListFor(model => model.FontFamily, new SelectList(new List<String> { "Arial", "Tahoma", "Verdana" }), "Please select", new {Attr = "font-family" })%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model => model.FontSizeValue)%><br/>
            <%:Html.TextBoxFor(model => model.FontSizeValue, new { Class = "inp_txt w_220", Attr = "font-size" })%>
            <%:Html.DropDownListFor(model => model.FontSizeUnit, new SelectList(new List<String> { "px", "em", "pt", "%" }), "Please select", new { Class = "w_232", Attr = "font-size", style = "margin-left:5px;" })%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model => model.Color)%><br/>
            <%:Html.TextBoxFor(model => model.Color, new { Class = "inp_txt colorPicker", Attr = "color", Readonly = "readonly" })%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model => model.OtherStyles)%><br/>
            <%:Html.TextAreaFor(model => model.OtherStyles, new { Class = "inp_txt" })%>
        </div>
        <%:Html.AntiForgeryToken()%>
    </div>
     <div class="p_footer clrfix">
		<div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
		<div class="cancel_l"><a class="reset" href="javascript:void(0)"><%:Html.Translate("Actions.Reset") %></a></div>
	 </div>
    <% }%>
</div>
