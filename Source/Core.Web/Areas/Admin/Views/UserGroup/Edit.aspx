<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.UserGroupViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
  <% using (Html.BeginForm(MVC.Admin.UserGroup.Update(), FormMethod.Post)) {%>
    <div class="form_area">
      <%: Html.EditorFor(model => model.Name) %>
    </div>
    <div class="form_area">
      <%: Html.EditorFor(model => model.Description) %>
    </div>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.UserGroup.Index()) %>
    </p>
  <% } %>
</asp:Content>