<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Models.PageViewModel>" %>

<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:Model.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.Access[(int)PageOperations.Update] || Model.Access[(int)PageOperations.Permissions])
      {%>
    <%=Html.Partial(MVC.Pages.Views.ManagePageMenu, Model)%>
    <% }%>
    <table id="tblLayoutHolder" style="width: 100%; height: 100%">
        <%Html.RenderPartial(MVC.Shared.Views.Layouts.Layout, Model);%>
    </table>
    <%if (Model.Access[(int)PageOperations.Update])
      {%>
    <script type="text/javascript">
        $(function () {
            iNettutsInit();
        });
        function iNettutsInit() {
            iNettuts.init('<%=Url.Action(MVC.Pages.ShowSettings())%>?pageWidgetId=', '<%=Url.Action(MVC.Pages.UpdateWidgetsPositions()) %>');
        }
    </script>
    <% }%>
</asp:Content>
