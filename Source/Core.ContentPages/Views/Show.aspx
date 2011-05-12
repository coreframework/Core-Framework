<%@ Assembly Name="Core.ContentPages" %>
<%@ Import Namespace="Core.ContentPages.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContentPage>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:Model.Title%></h2>
    <%=Model.Content%>
</asp:Content>
