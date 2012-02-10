<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Models.PageViewModel>" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>
<%@ Import Namespace="Core.Web.Models" %>
<%@ Import Namespace="Core.Web.NHibernate.Models.Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:Model.Title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <table class="widgets pageSection" pagesection="<%=(int)PageSection.Header %>">
        <tr>
            <td class="column">
                <%foreach (var widget in Model.Widgets.FindAll(wd => wd.Widget.PageSection == PageSection.Header))
                  {%>
                <%if (widget.Widget == null || !(widget.SystemWidget is BaseWidget) || widget.Access[((BaseWidget)widget.SystemWidget).ViewOperationCode])
                  {%>
                <%Html.RenderPartial(
                MVC.Shared.Views.Widgets.WidgetContentHolder,
                widget);%>
                <%}%>
                <%}%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if ((Model.Access[(int)PageOperations.Update] || Model.Access[(int)PageOperations.Permissions]) && Model.PageMode == PageMode.Edit)
      {%>
    <%=Html.Partial(MVC.Pages.Views.ManagePageMenu, Model)%>
    <% }%>
    <div id="tblLayoutHolder" class="pageSection" pagesection="<%=(int)PageSection.Body %>">
        <%Html.RenderPartial(MVC.Shared.Views.Layouts.Layout, Model);%>
    </div>
    <script type="text/javascript">
        var $stickyFooter;
        $(function () {
            $stickyFooter = $('.footer').stickyFooter();
        <%if (Model.Access[(int)PageOperations.Update] && Model.PageMode == PageMode.Edit)
      {%>
            iNettutsInit($stickyFooter);
            <% }%>            
            $('.widget_title a.edit').click(function() {editWidgetClicked(this, '<%=Url.Action(MVC.Pages.ShowSettings())%>?pageWidgetId=', '.widget');});
        });
        function iNettutsInit($stickyFooter) {
            iNettuts.init('<%=Url.Action(MVC.Pages.ShowSettings())%>?pageWidgetId=', '<%=Url.Action(MVC.Pages.UpdateWidgetsPositions()) %>', $stickyFooter);
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" runat="server">
    <table class="widgets pageSection" pagesection="<%=(int)PageSection.Footer %>">
        <tr>
            <td class="column">
                <%foreach (var widget in Model.Widgets.FindAll(wd => wd.Widget.PageSection == PageSection.Footer))
                  {%>
                <%if (widget.Widget == null || !(widget.SystemWidget is BaseWidget) || widget.Access[((BaseWidget)widget.SystemWidget).ViewOperationCode])
                  {%>
                <%Html.RenderPartial(
                MVC.Shared.Views.Widgets.WidgetContentHolder,
                widget);%>
                <%}%>
                <%}%>
            </td>
        </tr>
    </table>
</asp:Content>
