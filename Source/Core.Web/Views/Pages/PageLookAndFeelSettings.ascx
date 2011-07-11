<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageLookAndFeelModel>" %>

<% Html.RenderPartial(MVC.Pages.Views.PageLookAndFeelForm, Model); %>
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
                $('.container').css($input.attr('Attr'), $input.val());
            }
        });
        $('select[attr=font-family]').change(function () {
            $('.container').css($(this).attr('Attr'), $(this).val());
        });
        $('input[attr=font-size]').keyup(function () {
            changeFontSize();
        });
        $('select[attr=font-size]').change(function () {
            changeFontSize();
        });
        $('textarea', $('.form_area')).keyup(function () {
            var $stylesHolder = $('.inner-container');
            $stylesHolder.removeAttr('style');
            var styles = $(this).val().split(";");
            var i;
            for (i = 0; i < styles.length; i++) {
                var styleParts = styles[i].split(":");
                if (styleParts.length == 2) {
                    $stylesHolder.css($.trim(styleParts[0]), $.trim(styleParts[1]));
                }
            }
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
                        $('.container').css($(this).attr('Attr'), '');
                    }
                }
            });
        });
    }

    function changeFontSize() {
        var fontSizeValue = parseFloat($('input[attr=font-size]').val());
        var fontSizeUnit = $('select[attr=font-size]').val();
        if (fontSizeValue && fontSizeUnit) {
            $('.container').css('font-size', fontSizeValue + fontSizeUnit);
        } else {
            $('.container').css('font-size', '');
        }
    }
    
</script>
