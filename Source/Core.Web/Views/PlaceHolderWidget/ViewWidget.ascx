<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PlaceHolderWidgetViewModel>" %>
<%if (Model.IsOnTemplate)
  { %>
<%:Html.Translate(".PlaceHolder")%>
<%}
  else
  {
      var containerId = String.Format("placeHolder_{0}", Model.Id);
      %>
<div id="<%=containerId %>">
    <% using (Ajax.BeginForm("ReplaceWidget", "PlaceHolderWidget", new AjaxOptions { UpdateTargetId = containerId, OnComplete = "replaceWidget" }))
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
</div>
<script type="text/javascript">
    function replaceWidget(result) {
        $('#<%=containerId %>').parents('.widget').replaceWith(result);
    }
</script>
<%} %>