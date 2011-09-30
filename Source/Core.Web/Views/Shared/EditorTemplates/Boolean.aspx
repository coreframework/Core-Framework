<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Framework.Mvc.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
  <p>
    <%: Html.CheckBox("") %>
    <%: Html.CustomLabelForModel(new { cssClass = "checkbox-label" }) %>
  </p>
</asp:Content>