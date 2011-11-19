<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PlaceHolderWidgetViewModel>" %>
<%if (Model.IsOnTemplate)
  { %>
<%:Html.Translate(".PlaceHolder")%>
<%}
  else
  { %>
<% using (Ajax.BeginForm("ReplaceWidget", "PlaceHolderWidget", new AjaxOptions { UpdateTargetId = "commonSettings", OnComplete = "completeUpdates" }))
   { %>
<%:Html.Messages() %>
<div class="form_area">
    <%:Html.HiddenFor(model => model.Id)%>
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model=>model.WidgetId) %><br />
        <%:Html.DropDownListFor(model => model.WidgetId, new SelectList(Model.AvailableWidgets, "Id", "Title", Model.WidgetId), Html.Translate("Actions.PleaseSelect"), new { })%>
        <%:Html.ValidationMessageFor(model => model.WidgetId)%>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
<div class="p_footer clrfix">
    <div class="btn1">
        <em></em>
        <%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
</div>
<%} %>
<%} %>