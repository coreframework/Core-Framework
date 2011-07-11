<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetCSSModel>" %>
<%@ Import Namespace="Core.Web.Helpers" %>
<div>
    <div class="validation_info">
        <span><%=Html.Translate(".CurrentWidgetInformation")%>:</span><br/>
        <span><%=Html.Translate(".WidgetID")%>: #<%=WidgetHelper.GetWidgetClientId(Model.WidgetId) %></span><br/>
        <span><%=Html.Translate(".WidgetClasses")%>: .widget</span>
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
            var customStyles = $(this).val();
            customStyles = customStyles.replace(/<script[^>]*>([\u0001-\uFFFF]*?)<\/script>/gim, '');
            customStyles = customStyles.replace(/<\/?[^>]+>/gi, '');
            var customStyleBlock = $('style#pageAdditionalStyles');
            if (!customStyleBlock) {
                // Do not modify. This is a workaround for an IE bug.
                var styleEl = document.createElement('style');
                styleEl.id = 'pageAdditionalStyles';
                styleEl.setAttribute('type', 'text/css');
                document.getElementsByTagName('head')[0].appendChild(styleEl);
            }
            else {
                styleEl = customStyleBlock[0];
            }
            if (styleEl.styleSheet) { // for IE only
                if (customStyles == '') {
                    // Do not modify. This is a workaround for an IE bug.
                    customStyles = '<!---->';
                }
                styleEl.styleSheet.cssText = customStyles;
            }
            else {
                customStyleBlock.text(customStyles);
            }
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