<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.UserViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
  <% using (Html.BeginForm(MVC.Admin.User.Update(), FormMethod.Post)) {%>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.User.Index()) %>
    </p>
    <div class="form_area">
      <%: Html.HiddenFor(model => model.Id) %>

      <%: Html.EditorFor(model => model.Email) %>
      <%: Html.ValidationMessageFor(model => model.Email) %>

      <%: Html.EditorFor(model => model.Nickname) %>
      <%: Html.EditorFor(model => model.Status) %>
      <%: Html.EditorFor(model => model.Password) %>
    </div>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.User.Index()) %>
    </p>
  <% } %>
</asp:Content>