<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.NavigationMenuModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>
<%@ Import Namespace="Core.Web.Models" %>

<div class="pages clrfix">
    <div class="pages-menu" id="pagesMenu">
     <%=Html.RenderTree(Model.MenuItems, "clrfix",
         model=>Html.Partial(MVC.Pages.Views.NavigationMenuItem, model).ToString())%>
    </div>
</div>
<div class="clear"></div>

 <script type="text/javascript">
     jQuery(function () {
         $('a.add-page').parent('li').addClass('add_new');
         $('div.btn2').parent('li').addClass('add_new');
         $('.pages-menu ul:first').children('li.add_new').removeClass('add_new').addClass('add_new_pg');
     });

     jQuery(function () {
         ddsmoothmenu.init({
             mainmenuid: "pagesMenu", //menu DIV id
             orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
             classname: 'pages-menu', //class added to menu's outer DIV
             contentsource: "markup",
             arrowimages: { down: ['downarrowclass', '<%:Links.Content.Images.ico_open_png %>', 17], right: ['rightarrowclass', '<%:Links.Content.Images.ico_right_png %>'] }
         });
     });
</script>
<%if (Model.PageMode==PageMode.Edit && Model.ManageAccess) {%>
    <script type="text/javascript">
     jQuery(function () {
         $("div.pages-menu ul, div.pages-menu ul ul,div.pages-menu ul ul ul").sortable(
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
           $("div.pages-menu ul, div.pages-menu ul ul,div.pages-menu ul ul ul").disableSelection();
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
            dialog.dialog({ width: 500, resizable: false, modal: true });
            return false;
        }
    </script>
<%} %>