// sticky footer plugin
(function ($) {
    var footer;

    $.fn.extend({
        stickyFooter: function (options) {
            footer = this;
            this.intialize = function () {
                return this;
            };
            this.positionFooter = function () {
                var docHeight = $(document.body).height() - $("#sticky-footer-push").height();
                if (docHeight < $(window).height()) {
                    var diff = $(window).height() - docHeight;
                    if (!$("#sticky-footer-push").length > 0) {
                        $(footer).before('<div id="sticky-footer-push"></div>');
                    }
                    $("#sticky-footer-push").height(diff);
                }
            };
            this.positionFooter();
            $(window).scroll(this.positionFooter).resize(this.positionFooter);
            return this.intialize();
        }
    });
})(jQuery);