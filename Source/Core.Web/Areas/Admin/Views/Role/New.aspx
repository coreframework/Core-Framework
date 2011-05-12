<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.RoleViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: Html.Translate(".Title") %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <h1><%: Html.Translate(".Title") %></h1>
  <% using (Html.BeginForm(MVC.Admin.Role.Create(), FormMethod.Post)) {%>
    <%: Html.HttpMethodOverride(HttpVerbs.Put) %>
    <div class="form_area">
      <%: Html.EditorFor(model => model.Name) %>
    </div>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.Role.Index()) %>
    </p>
  <% } %>
</asp:Content>