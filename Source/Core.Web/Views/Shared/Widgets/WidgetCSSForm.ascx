<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetCSSModel>" %>
<%@ Import Namespace="Framework.Core" %>
<%@ Import Namespace="Core.Web.Helpers" %>

<div id="CSSForm">
    <% using (Ajax.BeginForm("UpdateWidgetCSS", "Pages", new AjaxOptions { UpdateTargetId = "CSSForm", OnSuccess = "updateCSSForm" }))
       { %>
     <%:Html.Messages()%>
    <div class="form_area">
        <input id="SettingId" name="SettingId" type="hidden" value="<%=Model.SettingId %>" />
        <%:Html.HiddenFor(model => model.WidgetId)%>
        <%:Html.HiddenFor(model => model.PageCssModel.PageId)%>
        <input id="PageCssModel_SettingId" name="PageCssModel.SettingId" type="hidden" value="<%=Model.PageCssModel.SettingId %>" />
        <div class="form_i">
            <label>Custom CSS class names</label><br/>
            <%:Html.TextBoxFor(model => model.CustomCSSClasses, new { Class = "inp_txt" })%>
        </div>
        <div class="form_i">
            <label> Custom CSS</label><br/>
            <%:Html.TextAreaFor(model => model.PageCssModel.CustomCSS, new { Class = "inp_txt", Rows = 15 })%>
        </div>
        <%:Html.AntiForgeryToken()%>
        <div class="css-rule-constructor">
            <a href="javascript:void(0);" rule-for="#<%=WidgetHelper.GetWidgetClientId(Model.WidgetId) %>"><%=Html.Translate(".WidgetRule")%></a><br />
            <a href="javascript:void(0);" rule-for=".widget"><%=Html.Translate(".AllWidgetsRule")%></a>
       </div>
    </div>
     <div class="p_footer clrfix">
		<div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
		<div class="cancel_l"><a class="reset" href="javascript:void(0)"><%:Html.Translate("Actions.Reset") %></a></div>
	 </div>
    <% }%>
</div>
