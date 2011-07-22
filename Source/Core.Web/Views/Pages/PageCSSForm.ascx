<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageCSSModel>" %>

<div id="CSSForm">
    <% using (Ajax.BeginForm("UpdatePageCSS", "Pages", new AjaxOptions { UpdateTargetId = "CSSForm", OnSuccess = "updateCSSForm" }))
       { %>
     <%:Html.Messages()%>
     <div class="form_area">
        <input id="SettingId" name="SettingId" type="hidden" value="<%=Model.SettingId %>" />
        <%:Html.HiddenFor(model => model.PageId)%>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.CustomCSS)%><br/>
            <%:Html.TextAreaFor(model => model.CustomCSS, new { Class = "inp_txt", Rows = 15 })%>
        </div>
        <%:Html.AntiForgeryToken()%>
        <div class="css-rule-constructor">
            <a href="javascript:void(0);" rule-for=".container"><%=Html.Translate(".PageRule")%></a><br />
            <a href="javascript:void(0);" rule-for=".widget"><%=Html.Translate(".AllWidgetsRule")%></a>
       </div>
    </div>
     <div class="p_footer clrfix">
		<div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
		<div class="cancel_l"><a class="reset" href="javascript:void(0)"><%:Html.Translate("Actions.Reset")%></a></div>
	 </div>
    <% }%>
</div>
