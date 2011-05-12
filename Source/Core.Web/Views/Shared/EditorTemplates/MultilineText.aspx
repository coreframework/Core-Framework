<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
  <div class="wrapper"><%= Html.TextAreaFor(model => model) %></div>
</asp:Content>