<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.Models.NavigationMenuModel>>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>

 <%=Html.RenderTree(Model,"menu pages-menu",
     model=>Html.Partial(MVC.Pages.Views.NavigationMenuItem,model).ToString())%>

 <script type="text/javascript">
     jQuery(function () {
         $('ul.pages-menu').superfish();
         $('a.add-page').parent('li').addClass('last');
         $("ul.pages-menu,ul.pages-menu ul,ul.pages-menu ul ul").sortable(
            {
                items: "li:not(.last)",
                stop: function (e, ui) {
                    var curItem = $(ui.item);
                    var currentOrderNumber = curItem.parent().children().index(curItem) + 1;
                    var postData = {};
                    postData.pageId = $('#menuPageId', curItem).val();
                    postData.orderNumber = currentOrderNumber;
                    $.ajax({
                    url: '<%=Url.Action(MVC.Pages.UpdatePagePosition()) %>',
                    type: 'POST',
                    data: postData
                    });
                }
            }
        );
         $("ul.pages-menu, ul.pages-menu ul, ul.pages-menu ul ul").disableSelection();
     });

    function updateNavigationMenu(item) {
        $(item).parents('li').first().animate({
            opacity: 0
        }, function () {
            $(this).slideUp(function () {
                $(this).remove();
            });
        });
    }
    function addNewPage(url) {
        var dialog = $('<div title="Add new page" style="display:none"/>').appendTo('body');
        dialog.load(
                                url,
                                {},
                                function (responseText, textStatus, XMLHttpRequest) {
                                    dialog.dialog();
                                }
                            );
        dialog.dialog({ width: 450, resizable: false, modal: true });
        return false;
    }
</script>