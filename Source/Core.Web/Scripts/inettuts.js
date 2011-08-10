/*
* Script from NETTUTS.com [by James Padolsey]
* @requires jQuery($), jQuery UI & sortable/draggable UI modules
*/

var iNettuts = {

    jQuery: $,

    settings: {
        columns: '.column',
        widgetSelector: '.widget',
        handleSelector: '.widget_title',
        contentSelector: '.widget_content',
        pageWidgetIdSelector: '#pageWidgetId',
        columnNumberSelector: '#column',
        orderNumberSelector: '#order',
        pageSectionNumberSelector: '#pageSection',
        pageSectionSelector: '.pageSection',
        widgetDefault: {
            movable: true,
            removable: true,
            collapsible: true,
            editable: true
        },
        widgetIndividual: {},
        settingURLTemplate: '',
        updateWidgetsURL: '',
        $stickyFooter: {}
    },

    init: function (settingURLTemplate, updateWidgetsURL, $stickyFooter) {
        this.settings.settingURLTemplate = settingURLTemplate;
        this.settings.updateWidgetsURL = updateWidgetsURL;
        this.settings.$stickyFooter = $stickyFooter;
        this.addWidgetControls();
        this.makeSortable();
    },

    getWidgetSettings: function (id) {
        var $ = this.jQuery,
            settings = this.settings;
        return (id && settings.widgetIndividual[id]) ? $.extend({}, settings.widgetDefault, settings.widgetIndividual[id]) : settings.widgetDefault;
    },

    addWidgetControls: function () {
        var iNettuts = this,
            $ = this.jQuery,
            settings = this.settings;

        $(settings.widgetSelector, $(settings.columns)).each(function () {
            if (!$(this).attr('inettuts')) {
                var thisWidgetSettings = iNettuts.getWidgetSettings(this.id);
                if (thisWidgetSettings.removable) {
                    $('a.remove', $(settings.handleSelector, this)).mousedown(function (e) {
                        e.stopPropagation();
                    });
                }

                if (thisWidgetSettings.editable) {
                    $('a.edit', $(settings.handleSelector, this)).mousedown(function (e) {
                        e.stopPropagation();
                    }).click(function () {
                        var url = settings.settingURLTemplate + $('#pageWidgetId', $(this).parents(settings.widgetSelector)).val();
                        var dialog = $('<div title="Widget settings" style="display:none"/>').appendTo('body');
                        dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                        dialog.dialog({ width: 500, resizable: false, modal: true, position: 'top', close: function (ev, ui) { $(this).remove(); } });
                        return false;
                    });
                }
                $(this).attr('inettuts', true);
            }
        });
    },

    makeSortable: function () {
        var iNettuts = this,
            $ = this.jQuery,
            settings = this.settings,
            $sortableItems = (function () {
                var notSortable;
                $(settings.widgetSelector, $(settings.columns)).each(function (i) {
                    if (!iNettuts.getWidgetSettings(this.id).movable) {
                        if (!this.id) {
                            this.id = 'widget-no-id-' + i;
                        }
                        if (!notSortable) {
                            notSortable = '';
                        }
                        notSortable += '#' + this.id + ',';
                    }
                });
                if (notSortable) {
                    return $('> div.' + settings.widgetSelector + ':not(' + notSortable + ')', settings.columns);
                }
                return $('> div.' + settings.widgetSelector, settings.columns);
            })();

        $sortableItems.find(settings.handleSelector).css({
            cursor: 'move'
        }).mousedown(function (e) {
            /*$sortableItems.css({ width: '' });
            $(this).parent().css({
                width: $(this).parent().width() + 'px'
            });*/
            $(this).bind('mousemove', function () { $(settings.columns + ':not(:has(' + settings.widgetSelector + '))').height('50px'); });
        }).mouseup(function () {
            if (!$(this).parent().hasClass('dragging')) {
            //    $(this).parent().css({ width: '' });
            } else {
                $(settings.columns).sortable('disable');
            }
            $(this).unbind('mousemove');
        });

        $(settings.columns).sortable({
            items: $sortableItems,
            connectWith: $(settings.columns),
            tolerance: 'pointer',
            handle: settings.handleSelector,
            placeholder: 'widget-placeholder',
            forcePlaceholderSize: true,
            revert: 300,
            delay: 100,
            opacity: 0.8,
            //containment: 'document',
            start: function (e, ui) {
                $(ui.helper).addClass('dragging');
                //$(settings.columns + ':not(:has(' + settings.widgetSelector + '))').height('50px');
                $(settings.columns).addClass('column-placeholder');
                settings.$stickyFooter.positionFooter();
            },
            stop: function (e, ui) {
                $(settings.handleSelector).unbind('mousemove');
                $(settings.columns).removeClass('column-placeholder');
                $(ui.item).css({ width: '' }).removeClass('dragging');
                $(settings.columns + ':not(:has(' + settings.widgetSelector + '))').height('0');
                $(settings.columns).sortable('enable');
                $widget = $(ui.item);
                var prevPageSection = $(settings.pageSectionNumberSelector, $widget).val();
                var prevColumnNumber = $(settings.columnNumberSelector, $widget).val();
                var prevOrderNumber = $(settings.orderNumberSelector, $widget).val();
                var $widgetColumn = $widget.parents(settings.columns);
                var $currentPageSection = $widgetColumn.parents(settings.pageSectionSelector);
                var currentPageSectionNumber = $currentPageSection.attr('pageSection');
                var currentColumnNumber = $(settings.columns, $currentPageSection).index($widgetColumn) + 1;
                var currentOrderNumber = $(settings.widgetSelector, $widgetColumn).index($widget) + 1;
                if (prevPageSection != currentPageSectionNumber || prevColumnNumber != currentColumnNumber || prevOrderNumber != currentOrderNumber) {
                    $(settings.columnNumberSelector, $widget).val(currentColumnNumber);
                    $(settings.orderNumberSelector, $widget).val(currentOrderNumber);
                    var postData = {};
                    postData.widgetId = $(settings.pageWidgetIdSelector, $widget).val();
                    postData.pageSection = currentPageSectionNumber;
                    postData.columnNumber = currentColumnNumber;
                    postData.orderNumber = currentOrderNumber;
                    $.ajax({
                        url: settings.updateWidgetsURL,
                        type: 'POST',
                        data: postData
                    });
                }
                settings.$stickyFooter.positionFooter();
            }
        });
    }

};

function updateAfterRemoving(removeLink, widgetSelector) {
    $(removeLink).parents(widgetSelector).animate({
        opacity: 0
    }, function () {
        $(this).wrap('<div/>').parent().slideUp(function () {
            $(this).remove();
            $stickyFooter.positionFooter();
        });
    });
}

