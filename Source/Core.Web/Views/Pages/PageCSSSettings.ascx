<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageCSSModel>" %>
<div style="height: 500px">
    <h2 class="settings-header">
        <%=Html.Translate(".CSS") %></h2>
    <div class="ui-widget">
        <div class="ui-state-highlight ui-corner-all" style="padding: 0pt 0.7em;">
            <p>
                <span class="ui-icon ui-icon-lightbulb" style="float: left; margin-right: 0.3em;">
                </span><span>
                    <%=Html.Translate(".CurrentPageInformation")%>:</span><br />
                <span style="padding-left: 19px;">
                    <%=Html.Translate(".PageClass")%>: .container</span><br />
                <span style="padding-left: 19px;">
                    <%=Html.Translate(".WidgetClasses")%>: .widget</span>
            </p>
        </div>
    </div>
    <% Html.RenderPartial(MVC.Pages.Views.PageCSSForm, Model); %>
</div>
<script type="text/javascript">
    $(function () {
        updateCSSForm();
    });

    function updateCSSForm() {
        $('textarea', $('.form_area')).keyup(function () {
            $('style#pageAdditionalStyles').text($(this).val());
        });
        $('.css-rule-constructor a').click(function () {
            $textarea = $('textarea', $('.form_area'));
            $textarea.val($textarea.val() + '\n' + $(this).attr('rule-for') + '{\n}');
        });
        $('.reset', $('.form_area').parent()).click(function () {
            $('.form_area').find(':input').each(function () {
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
