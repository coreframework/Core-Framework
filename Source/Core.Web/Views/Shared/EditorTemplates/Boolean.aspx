<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
  <p>
    <%: Html.CheckBox("") %>
    <%: Html.CustomLabelForModel(new { cssClass = "checkbox-label" }) %>
  </p>
</asp:Content>