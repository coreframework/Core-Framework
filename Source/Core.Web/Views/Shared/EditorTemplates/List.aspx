<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
  <p>
   <%= Html.DropDownListFor(model => model) %>
  </p>
</asp:Content>