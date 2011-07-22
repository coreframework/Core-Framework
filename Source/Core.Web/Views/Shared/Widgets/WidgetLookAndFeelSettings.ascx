<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WidgetLookAndFeelModel>" %>
<%@ Import Namespace="Core.Web.Models" %>
<div>
    <% Html.RenderPartial(MVC.Shared.Views.Widgets.WidgetLookAndFeelForm, Model); %>
</div>
<script type="text/javascript">
    $(function () {
        updateLookAndFeelForm();
    });

    function updateLookAndFeelForm() {
        $(".colorPicker").ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(100);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(100);
                return false;
            },
            onChange: function ($input, hex, rgb) {
                var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
                try {
                    $('.widget_content', $widget).css($input.attr('Attr'), $input.val());
                }
                catch (err) { }
            }
        });
        $('select[attr=font-family]').change(function () {
            var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
            $('.widget_content', $widget).css($(this).attr('Attr'), $(this).val());
        });
        $('input[attr=font-size]').keyup(function () {
            changeTwinFieldAttribute('font-size');
        });
        $('select[attr=font-size]').change(function () {
            changeTwinFieldAttribute('font-size');
        });
        $('input[attr=width]').keyup(function () {
            changeTwinFieldAttribute('width');
        });
        $('select[attr=width]').change(function () {
            changeTwinFieldAttribute('width');
        });
        $('input[attr=height]').keyup(function () {
            changeTwinFieldAttribute('height');
        });
        $('select[attr=height]').change(function () {
            changeTwinFieldAttribute('height');
        });
        $('textarea', $('.form_area')).keyup(function () {
            var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
            var $stylesHolder = $('.widget_content div', $widget).first();
            $stylesHolder.removeAttr('style');
            try {
                var styles = $(this).val().split(";");
                var i;
                for (i = 0; i < styles.length; i++) {
                    var styleParts = styles[i].split(":");
                    if (styleParts.length == 2) {
                        $stylesHolder.css($.trim(styleParts[0]), $.trim(styleParts[1]));
                    }
                }
            }
            catch (err) { }
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
                    if ($(this).hasClass('colorPicker')) {
                        var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
                        $('.widget_content', $widget).css($(this).attr('Attr'), '');
                    }
                }
            });
        });
    }

    function changeTwinFieldAttribute(attrName) {
        var $widget = $('input[type=hidden][name=pageWidgetId][value=<%=Model.WidgetId %>]').parents('.widget');
        var value = parseFloat($('input[attr=' + attrName + ']').val());
        var unit = $('select[attr=' + attrName + ']').val();
        if (value && unit) {
            $('.widget_content', $widget).css(attrName, value + unit);
        } else {
            $('.widget_content', $widget).css(attrName, '');
        }
    }
    
</script>
