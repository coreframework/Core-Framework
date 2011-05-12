<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetCSSModel>" %>
<%@ Import Namespace="Core.Web.Helpers" %>
<div>
    <h2 class="settings-header">
        <%=Html.Translate(".CSS") %></h2>
    <div class="ui-widget">
        <div class="ui-state-highlight ui-corner-all" style="padding: 0pt 0.7em;">
            <p>
                <span class="ui-icon ui-icon-lightbulb" style="float: left; margin-right: 0.3em;">
                </span><span><%=Html.Translate(".CurrentWidgetInformation")%>:</span><br/>
                <span style="padding-left:19px;"><%=Html.Translate(".WidgetID")%>: #<%=WidgetHelper.GetWidgetClientId(Model.WidgetId) %></span><br/>
                <span style="padding-left:19px;"><%=Html.Translate(".WidgetClasses")%>: .widget</span>
            </p>
        </div>
    </div>
    <% Html.RenderPartial(MVC.Shared.Views.Widgets.WidgetCSSForm); %>
</div>
<script type="text/javascript">
    $(function () {
        updateCSSForm();
    });

    function updateCSSForm() {
        $('input', $('#CSSForm .form_area')).keyup(function () {
           var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
           $widget.removeClass().addClass('widget ' + $(this).val());
        });
       $('textarea', $('#CSSForm .form_area')).keyup(function () {
            $('style#pageAdditionalStyles').text($(this).val());
        });
        $('.css-rule-constructor a').click(function () {
            $textarea = $('textarea', $('#CSSForm .form_area'));
            $textarea.val($textarea.val() + '\n' + $(this).attr('rule-for') + '{\n}');
        });
        $('.reset', $('#CSSForm .form_area').parent()).click(function () {
            $('#CSSForm .form_area').find(':input').each(function () {
                if (this.type != 'hidden') {
                    $(this).val('');
                    if (this.type == 'textarea') {
                        $(this).trigger("keyup");
                    } else {
                        $(this).trigger("change");
                    }
                }
            });
        });
    }
</script>
