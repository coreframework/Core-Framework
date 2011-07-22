<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" 
Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:Html.Translate("Titles.Modules") %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%:Html.Translate("Titles.Modules") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
    </div>
</asp:Content>
