<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>
<h2 class="settings-header">
    <%=Html.Translate(".CommonSettings") %></h2>
<% using (Ajax.BeginForm(
                        Model.Widget.SaveAction.Action,
                        Model.Widget.SaveAction.Controller,
                        new { area = Model.Widget.SaveAction.Area },
                        new AjaxOptions()
                                {
                                    UpdateTargetId = String.Format("widgetHolder{0}", Model.Id),
                                    OnSuccess = "function(){ updatePageWidgetInstance(this," + Model.Id + "); }"
                                }))
   { %>
<div id="widgetHolder<%=Model.Id %>">
    <%Html.RenderAction(
                            Model.Widget.EditAction.Action,
                            Model.Widget.EditAction.Controller,
                            new
                                {
                                    instance = Model.WidgetInstance,
                                    area = Model.Widget.EditAction.Area
                                });%>
</div>
<%if (Model.Widget.SaveAction != null)
  {%>
<div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
    <div class="ui-dialog-buttonset">
        <%: Html.Submit(Html.Translate(".Save"), new { Class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" })%>
    </div>
</div>
<%} %>
<%} %>
<script type="text/javascript">
    function updatePageWidgetInstance(form, widgetId) {

        if ($('input[type=hidden]#widgetId', form).length > 0) {

            var instanceId = $('input[type=hidden]#widgetId', form).val();
            if (instanceId != null) {
                var params = {
                    pageWidgetId: widgetId,
                    instanceId: $('input[type=hidden]#widgetId', form).val()
                };
                var url = '<%=Url.Action(MVC.Pages.UpdatePageWidgetInstance()) %>';

                $.post(url, params, function (data) {
                    var widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.Id %>]').parents('.widget');
                    $('.widget-content', widget).html($(data).find('.widget-content').html());
                });
            }
        }
    }
</script>
