<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>

<% using (Ajax.BeginForm(
                        Model.SystemWidget.SaveAction.Action,
                        Model.SystemWidget.SaveAction.Controller,
                        new { area = Model.SystemWidget.SaveAction.Area },
                        new AjaxOptions()
                                {
                                    UpdateTargetId = String.Format("widgetHolder{0}", Model.Widget.Id),
                                    OnSuccess = "function(){ updatePageWidgetInstance(this," + Model.Widget.Id + "); }"
                                }))
   { %>
    <div id="widgetHolder<%=Model.Widget.Id %>">
        <%Html.RenderAction(
                                Model.SystemWidget.EditAction.Action,
                                Model.SystemWidget.EditAction.Controller,
                                new
                                    {
                                        instance = Model.WidgetInstance,
                                        area = Model.SystemWidget.EditAction.Area
                                    });%>
    </div>
    <%if (Model.SystemWidget.SaveAction != null)
      {%>
         <div class="p_footer clrfix">
		    <div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
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
                    var widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.Widget.Id %>]').parents('.widget');
                    $(widget).replaceWith(data);
                    iNettutsInit($stickyFooter);
                    $('.widget_title a.edit').unbind('click').click(function () { editWidgetClicked(this, '<%=Url.Action(MVC.Pages.ShowSettings())%>?pageWidgetId=', '.widget'); });
                });
            }

        }
    }
</script>
