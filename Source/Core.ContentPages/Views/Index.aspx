<%@ Assembly Name="Core.ContentPages" %>
<%@ Import Namespace="Core.ContentPages.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<ContentPage>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Content Pages</h2>
    <table class="index">
        <thead>
        <tr>
            <th>Title</th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <% foreach (var page in Model){ %>
        <tr class="node level_1">
            <td>
                <%:page.Title%>
            </td>
            <td><%:Html.ActionLink("View", "Show","ContentPage",new {id=page.Id},null)%></td>
        </tr>
        <% } %>
        </tbody>
    </table>
</asp:Content>
