<%@ Assembly Name="Core.Languages.NHibernate" %>
<%@ Assembly Name="Core.Languages.NHibernate" %>
<%@ Import Namespace="Core.Languages.NHibernate.Models" %>
<%@ Import Namespace="System.Web.Mvc" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="ViewPage<Language>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:Model.Title%></h2>
     <%=Model.Code%>
     <%=Model.Culture%>
</asp:Content>
