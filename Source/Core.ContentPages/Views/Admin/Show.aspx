<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.ContentPages.NHibernate.Models.ContentPage>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
   <h1><%:Model.Title%></h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p> <%=Model.Content%></p>
</asp:Content>
