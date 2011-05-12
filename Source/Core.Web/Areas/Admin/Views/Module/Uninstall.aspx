<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.PluginViewModel>" %>

<%@ Import Namespace="Framework.MVC.Extensions" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <h1><%: String.Format(Html.Translate(".Title"), Model.Title)%></h1>
  <p><%: String.Format(Html.Translate(".AreYouSure"), Model.Title)%></p>
  <% using (Html.BeginForm(MVC.Admin.Module.ConfirmUninstall(Model.Id), FormMethod.Post)) {%>
    <p class="buttons">
      <%: Html.Submit("Uninstall") %>
      <%: Html.ActionLink("Cancel", MVC.Admin.Module.Index()) %>
    </p>
  <% } %>
</asp:Content>
