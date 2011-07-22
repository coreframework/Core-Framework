<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetLookAndFeelModel>" %>
<div id="lookAndFeelForm">
    <% using (Ajax.BeginForm("UpdateWidgetLookAndFeel", "Pages", new AjaxOptions { UpdateTargetId = "lookAndFeelForm", OnSuccess = "updateLookAndFeelForm" }))
       { %>
    <%:Html.Messages()%>
    <div class="form_area">
        <div class="form_i">
             <input id="SettingId" name="SettingId" type="hidden" value="<%=Model.SettingId %>" />
             <%:Html.HiddenFor(model => model.WidgetId)%>
             <label>Background color</label><br/>
              <%:Html.TextBoxFor(model => model.BackgroundColor, new { Class = "inp_txt colorPicker", Attr = "background-color", Readonly = "readonly" })%>
        </div>
        <div class="form_i">
            <label>Font</label><br/>
            <%:Html.DropDownListFor(model => model.FontFamily, new SelectList(new List<String> { "Arial", "Tahoma", "Verdana" }), "Please select", new {Attr = "font-family" })%>
        </div>
        <div class="form_i">
            <label>Font size</label><br/>
            <%:Html.TextBoxFor(model => model.FontSizeValue, new { Class = "inp_txt w_220", Attr = "font-size" })%>
            <%:Html.DropDownListFor(model => model.FontSizeUnit, new SelectList(new List<String> { "px", "em", "pt", "%" }), "Please select", new { Class = "w_232", Attr = "font-size", style = "margin-left:5px;" })%>
        </div>
        <div class="form_i">
             <label>Font color</label><br/>
                <%:Html.TextBoxFor(model => model.Color, new { Class = "inp_txt colorPicker", Attr = "color", Readonly = "readonly" })%>
        </div>
        <div class="form_i">
            <label>Width</label><br/>
            <%:Html.TextBoxFor(model => model.WidthValue, new { Class = "inp_txt w_220", Attr = "width" })%>
            <%:Html.DropDownListFor(model => model.WidthUnit, new SelectList(new List<String> { "px", "em", "pt", "%" }), "Please select", new { Class = "w_232", Attr = "width", style = "margin-left:5px;" })%>
        </div>
        <div class="form_i">
            <label>Height</label><br/>
            <%:Html.TextBoxFor(model => model.HeightValue, new { Class = "inp_txt w_220", Attr = "height" })%>
            <%:Html.DropDownListFor(model => model.HeightUnit, new SelectList(new List<String> { "px", "em", "pt", "%" }), "Please select", new {Class="w_232", Attr = "height", style = "margin-left:5px;" })%>
        </div>
        <div class="form_i">
           <label>Other</label><br/>
            <%:Html.TextAreaFor(model => model.OtherStyles, new { Class = "inp_txt" })%>
        </div>
      </div>
     <%:Html.AntiForgeryToken()%>
     <div class="p_footer clrfix">
		<div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
		<div class="cancel_l"><a class="reset" href="javascript:void(0)"><%:Html.Translate("Actions.Reset")%></a></div>
	 </div>
    <% }%>
</div>
