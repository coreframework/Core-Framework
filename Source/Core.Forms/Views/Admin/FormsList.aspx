<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Core.Forms.NHibernate.Models.Form>>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th style="width:90%;">Title</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>
            <% foreach (var form in Model){ %>
            <tr>
                <td>
                    <%:form.Title%>
                </td>
                <td>
                   <%:Html.RouteLink("Permissions", new { controller = "Forms", action = "ShowPermissions", formId = form.Id })%>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>
   <div id="actions">
    <ul>
      <li>
        <%-- <%:Html.RouteLink("Create Content Page", new { controller = "ContentPage", action = "New" })%>--%>
      </li>
    </ul>
  </div>
</asp:Content>
