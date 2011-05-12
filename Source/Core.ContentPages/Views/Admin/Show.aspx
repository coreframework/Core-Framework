<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.ContentPages.NHibernate.Models.ContentPage>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:Model.Title%></h2>
     <%=Model.Content%>
</asp:Content>
