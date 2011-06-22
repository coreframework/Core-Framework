<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Core.Forms.NHibernate.Models.Form>>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th style="width:90%;"><%:Html.Translate(".Title") %></th>
                <th><%:Html.Translate(".Actions") %></th>
            </tr>
            </thead>
            <tbody>
            <% foreach (var form in Model){ %>
            <tr>
                <td>
                    <%:form.Title%>
                </td>
                <td>
                   <%:Html.RouteLink(Html.Translate(".Permissions"), new { controller = "Forms", action = "ShowPermissions", formId = form.Id })%><br/>
                   <%:Html.RouteLink(Html.Translate(".Details"), new { controller = "Forms", action = "Edit", formId = form.Id })%><br/>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>
   <div id="actions">
    <ul>
      <li>
         <%:Html.RouteLink(Html.Translate(".CreateForm"), new { controller = "Forms", action = "New" })%>
      </li>
    </ul>
  </div>
</asp:Content>
