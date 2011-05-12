<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.GridViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: Html.Translate(".Title") %></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div class="outset">
        <%= Html.Partial(MVC.Admin.Shared.Views.Grid, Model)%>
        <%--    <table id="roles" class="index">--%>
        <%--      <thead>--%>
        <%--        <tr>--%>
        <%--          <th class="row"><%: Html.Translate(".Role") %></th>--%>
        <%--          <th class="actions" colspan="2"></th>--%>
        <%--        </tr>--%>
        <%--      </thead>--%>
        <%--      <tbody>--%>
        <%--      <% foreach (var role in Model.Roles) { %>--%>
        <%--        <tr class="node level_1">--%>
        <%--          <td class="row">--%>
        <%--            <% if (role.IsEditable) { %>--%>
        <%--            <%: Html.ActionLink(role.Name, MVC.Admin.Role.Edit(role.Id)) %>--%>
        <%--            <% } else { %>--%>
        <%--            <%: role.Name %>--%>
        <%--            <% } %>--%>
        <%--          </td>--%>
        <%--          <td class="action">--%>
        <%--            <%: Html.ActionLink(Html.Translate(".Users"), MVC.Admin.Role.Users(role.Id)) %><br/>--%>
        <%--            <%: Html.ActionLink(Html.Translate(".Permissions"), MVC.Admin.Role.Permissions(role.Id,null)) %>--%>
        <%--          </td>--%>
        <%--          <td class="action">--%>
        <%--            <% if (role.IsEditable) { %>--%>
        <%--            <%: Html.ActionLink(Html.Translate(".Remove"), MVC.Admin.Role.Remove(role.Id)) %>--%>
        <%--            <% } %>--%>
        <%--          </td>--%>
        <%--        </tr>--%>
        <%--      <% } %>--%>
        <%--      </tbody>--%>
        <%--    </table>--%>
    </div>
    <div id="actions">
        <ul>
            <li><a href="<%: Url.Action(MVC.Admin.Role.New()) %>">
                <img src="<%: Links.Content.Images.Admin.plus_png %>" alt="Plus"><%: Html.Translate(".New") %></a>
            </li>
        </ul>
    </div>
</asp:Content>
