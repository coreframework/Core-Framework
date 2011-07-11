<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Models.PageViewModel>" %>

<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>

<%@ Import Namespace="Core.Web.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:Model.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if ((Model.Access[(int)PageOperations.Update] || Model.Access[(int)PageOperations.Permissions]) && Model.PageMode == PageMode.Edit)
      {%>
        <%=Html.Partial(MVC.Pages.Views.ManagePageMenu, Model)%>
    <% }%>
    <div id="tblLayoutHolder">
        <%Html.RenderPartial(MVC.Shared.Views.Layouts.Layout, Model);%>
    </div>
    <%if (Model.Access[(int)PageOperations.Update] && Model.PageMode==PageMode.Edit)
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
