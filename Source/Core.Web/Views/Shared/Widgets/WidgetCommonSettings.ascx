<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>

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
         <div class="p_footer clrfix">
		    <div class="btn1"><em></em><%: Html.Submit("Save", new { Class = "button" })%><strong></strong></div>
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
                    $('.widget_content_i', widget).html($(data).find('.widget_content_i').html());
                });
            }
        }
    }
</script>
