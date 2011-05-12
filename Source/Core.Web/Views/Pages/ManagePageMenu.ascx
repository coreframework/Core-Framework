<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>

<ul class="menu manage-page-menu">
 <%if (Model.Access[(int)PageOperations.Update]) {%>
    <li>
        <a href="javascript:void();"><%=Html.Translate(".Add")%></a>
        <ul>
            <li>
                 <a class="show-widgets" href="javascript:void();"><%=Html.Translate(".Widget")%></a>
            </li>
        </ul>
    </li>
    <% }%>
    <li>
        <a href="javascript:void();"><%=Html.Translate(".Manage") %></a>
        <ul>
            <%if (Model.Access[(int)PageOperations.Update]) {%>
                <li>
                      <a class="change-settings" href="javascript:void();"><%=Html.Translate(".PageSettings")%></a>
                </li>
                <li>
                      <a class="change-layout" href="javascript:void();"><%=Html.Translate(".PageLayout")%></a>
                </li>
                <li>
                      <a class="change-lookAndFeel" href="javascript:void();"><%=Html.Translate(".PageLookAndFeel")%></a>
                </li>
                <li>
                      <a class="change-css" href="javascript:void();"><%=Html.Translate(".CSS")%></a>
                </li>
                <script type="text/javascript">
                    jQuery(function () {
                        $('a.show-widgets').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowAvailableWidgets(Model.Id??0))%>';
                            var dialog = $('<div title="Add widget" style="display:none"/>').appendTo('body');
                            dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                            dialog.dialog({ width: 500, resizable: false, modal: true });
                            return false;
                        });
                        $('a.change-layout').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowChangeLayoutForm(Model.Id??0))%>';
                            var dialog = $('<div title="Change page layout" style="display:none"/>').appendTo('body');
                            dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                            dialog.dialog({ width: 500, resizable: false, modal: true, close: function (ev, ui) { $(this).remove(); } });
                            return false;
                        });
                        $('a.change-settings').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowPageCommonSettings(Model.Id??0))%>';
                            var dialog = $('<div title="Change Page Common Settings" style="display:none"/>').appendTo('body');
                            dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                            dialog.dialog({ width: 460, resizable: false, modal: true });
                            return false;
                        });
                        $('a.change-lookAndFeel').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowPageLookAndFeel(Model.Id??0))%>';
                            var dialog = $('<div title="Page Look And Feel" style="display:none"/>').appendTo('body');
                            dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                            dialog.dialog({ width: 460, resizable: false, modal: true, position: 'top', close: function (ev, ui) { $(this).remove(); } });
                            return false;
                        });
                        $('a.change-css').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowPageCSS(Model.Id??0))%>';
                            var dialog = $('<div title="Page CSS" style="display:none"/>').appendTo('body');
                            dialog.load(
                        url,
                        {},
                        function (responseText, textStatus, XMLHttpRequest) {
                            dialog.dialog();
                        }
                    );
                            dialog.dialog({ width: 460, resizable: false, modal: true, position: 'top', close: function (ev, ui) { $(this).remove(); } });
                            return false;
                        });
                    });
                </script>
            <% }%>
             <%if (Model.Access[(int)PageOperations.Permissions]) {%>
                 <li>
                      <a class="permissions" href="javascript:void();"><%=Html.Translate(".Permissions")%></a>
                </li>
                <script type="text/javascript">
                    jQuery(function () {
                        $('a.permissions').click(function () {
                            var url = '<%=Url.Action(MVC.Pages.ShowPagePermissions(Model.Id??0))%>';
                            var dialog = $('<div title="Permissions" style="display:none"/>').appendTo('body');
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
                    });
                </script>
            <% }%>
        </ul>
    </li>
</ul>
<div class="clear"></div>

<script type="text/javascript">
    jQuery(function () {
        $('ul.manage-page-menu').superfish();
    });
</script>