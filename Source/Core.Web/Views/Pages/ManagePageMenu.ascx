<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Framework.Core" %>
<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>
<div class="clear">
</div>
<div class="curr_pg">
    <div class="title">
        <%: Model.Title %></div>
    <div class="functions">
        <div class="functions_i clrfix">
            <div class="menu manage-page-menu" id="managePageMenu">
                <ul class="clrfix">
                    <%if (Model.Access[(int)PageOperations.Update])
                      {%>
                    <li><a href="javascript:void(0);">
                        <%=Html.Translate("Actions.Add")%></a>
                        <ul>
                            <li><a class="show-widgets" href="javascript:void(0);">
                                <%=Html.Translate(".Widget")%></a> </li>
                        </ul>
                    </li>
                    <% }%>
                    <li><a href="javascript:void(0);">
                        <%=Html.Translate("Actions.Manage") %></a>
                        <ul>
                            <%if (Model.Access[(int)PageOperations.Update])
                              {%>
                            <li><a class="change-settings" href="javascript:void(0);">
                                <%=Html.Translate(".PageSettings")%></a> </li>
                            <li><a class="change-layout" href="javascript:void(0);">
                                <%=Html.Translate(".PageLayout")%></a> </li>
                            <li><a class="change-lookAndFeel" href="javascript:void(0);">
                                <%=Html.Translate(".PageLookAndFeel")%></a> </li>
                            <li><a class="change-css" href="javascript:void(0);">
                                <%=Html.Translate(".CSS")%></a> </li>
                            <script type="text/javascript">
                                jQuery(function () {
                                    $('a.show-widgets').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowAvailableWidgets(Model.Id??0, Model.IsTemplate))%>';
                                        var dialog = $('<div title="<%=Html.Translate("Actions.AddWidget")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, position: ['center', 150] });
                                        return false;
                                    });
                                    $('a.change-layout').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowChangeLayoutForm(Model.Id??0))%>';
                                        var dialog = $('<div title="<%=Html.Translate(".PageLayout")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, close: function (ev, ui) { $(this).remove(); }, position: ['center', 150] });
                                        return false;
                                    });
                                    $('a.change-settings').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowPageCommonSettings(Model.Id??0))%>';
                                        var dialog = $('<div title="<%=Html.Translate(".PageSettings")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, position: ['center', 150] });
                                        return false;
                                    });
                                    $('a.change-lookAndFeel').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowPageLookAndFeel(Model.Id??0))%>';
                                        var dialog = $('<div title="<%=Html.Translate(".PageLookAndFeel")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, close: function (ev, ui) { $(this).remove(); }, position: ['center', 150] });
                                        return false;
                                    });
                                    $('a.change-css').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowPageCSS(Model.Id??0))%>';
                                        var dialog = $('<div title="<%=Html.Translate(".CSS")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, close: function (ev, ui) { $(this).remove(); }, position: ['center', 150] });
                                        return false;
                                    });
                                });
                            </script>
                            <% }%>
                            <%if (Model.HasTemplate && Model.Access[PageTemplate.UnlinkOperationCode])
                              {%>
                            <li>
                                <%= Ajax.ActionLink(Html.Translate(".UnlinkPage"), 
                                        MVC.Pages.Unlink(Model.Id.Value),
                                        new AjaxOptions
                                                    {
                                                        Confirm = Html.Translate("Messages.UnlinkPage"),
                                                        OnSuccess = "function() {window.location.href=window.location.href;}"
                                                    })%>
                            </li>
                            <%
                              }%>
                            <%if (!Model.IsTemplate && !Model.IsServicePage && Model.Access[(int)PageOperations.Delete])
                              {%>
                            <li>
                                <%= Ajax.ActionLink(Html.Translate(".DeletePage"), 
                                        MVC.Pages.RemovePage(Model.Id.Value),
                                        new AjaxOptions
                                                    {
                                                        Confirm = Html.Translate("Messages.DeleteConfirm"),
                                                        OnSuccess = "function() {location.href='" + ApplicationUtility.Path + "';}"
                                                    })%>
                            </li>
                            <%
                              }%>
                            <%if (Model.Access[(int)PageOperations.Permissions])
                              {%>
                            <li><a class="permissions" href="javascript:void(0);">
                                <%=Html.Translate(".Permissions")%></a> </li>
                            <script type="text/javascript">
                                jQuery(function () {
                                    $('a.permissions').click(function () {
                                        var url = '<%=Url.Action(MVC.Pages.ShowPagePermissions(Model.Id??0))%>';
                                        var dialog = $('<div title="<%=Html.Translate(".Permissions")%>" style="display:none"/>').appendTo('body');
                                        dialog.load(
                                        url,
                                        {},
                                        function (responseText, textStatus, XMLHttpRequest) {
                                            dialog.dialog();
                                        }
                                    );
                                        dialog.dialog({ width: 500, resizable: false, modal: true, close: function (ev, ui) { $(this).remove(); }, position: ['center', 150] });
                                        return false;
                                    });
                                });
                            </script>
                            <% }%>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="btm_line">
    </div>
</div>
<script type="text/javascript">
    jQuery(function () {
        ddsmoothmenu.init({
            mainmenuid: "managePageMenu", //menu DIV id
            orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
            classname: 'manage-page-menu', //class added to menu's outer DIV
            contentsource: "markup",
            arrowimages: { down: ['downarrowclass', '<%:Links.Content.Images.ico_open_png %>', 17], right: ['rightarrowclass', '<%:Links.Content.Images.ico_right_png %>'] }
        });
    });
</script>
