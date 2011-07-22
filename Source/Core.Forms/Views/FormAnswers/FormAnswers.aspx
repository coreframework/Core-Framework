<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%:Html.Translate(".FormAnswers") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
   </div>
</asp:Content>