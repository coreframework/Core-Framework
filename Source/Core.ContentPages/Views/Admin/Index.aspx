<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Core.ContentPages.NHibernate.Models.ContentPage>>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th style="width:90%;">Title</th>
                <th><%=Html.Translate(".Actions")%></th>
            </tr>
            </thead>
            <tbody>
            <% foreach (var page in Model){ %>
            <tr>
                <td>
                    <%:page.Title%>
                </td>
              
                <td>
                   <%:Html.RouteLink("View", new { controller = "ContentPage", action = "ShowById", id = page.Id })%>
                   <%:Html.RouteLink("Edit", new { controller = "ContentPage", action = "Edit", id = page.Id })%>
                  
                 <% using (Html.BeginForm(ContentPagesMVC.ContentPage.Remove(page.Id), FormMethod.Post))
                    { %>
                        <%: Html.LinkSubmitButton("Remove")%>
                      
                   <% } %>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>
   <div id="actions">
    <ul>
      <li>
         <%:Html.RouteLink("Create Content Page", new { controller = "ContentPage", action = "New" })%>
      </li>
    </ul>
  </div>
</asp:Content>
