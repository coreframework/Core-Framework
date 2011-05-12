<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.UserToRoleAssignmentModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
  <% using (Html.BeginForm(MVC.Admin.Role.UpdateUsers(), FormMethod.Post)) {%>
    <%: Html.HttpMethodOverride(HttpVerbs.Put) %>
    <ul>
      <% if (Model.Users.Length > 0) { %>
        <li>
          <%: Html.CheckBox("All") %>
          <%: Html.Label("All", Html.Translate(".All")) %>
        </li>
      <% } %>
      <% for (var i = 0; i < Model.Users.Length; i++) { %>
        <li>
          <%: Html.HiddenFor(x => x.Users[i].Id) %>
          <%: Html.HiddenFor(x => x.Users[i].Name) %>
          <%: Html.CheckBoxFor(x => x.Users[i].Assigned) %>
          <%: Html.LabelFor(x => x.Users[i].Assigned, Model.Users[i].Name) %>
        </li>
      <% } %>
    </ul>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.Role.Index()) %>
    </p>
  <% } %>


  <script type="text/javascript">

      $(function () {
          $("#All").bind_select_all();
      });

  </script>
</asp:Content>