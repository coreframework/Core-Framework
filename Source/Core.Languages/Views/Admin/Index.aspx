<%@ Assembly Name="Core.Languages" %>
<%@ Assembly Name="Core.Languages.NHibernate" %>
<%@ Import Namespace="Core.Languages.NHibernate.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Language>>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th style="width:50%;">Title</th>
                <th style="width:20%;">Code</th>
                <th style="width:20%;">Culture</th>
                <th><%=Html.Translate(".Actions")%></th>
            </tr>
            </thead>
            <tbody>
            <% foreach (var language in Model){ %>
            <tr>
                <td>
                    <%:language.Title%>
                </td>
                <td>
                    <%:language.Code%>
                </td>
                <td>
                    <%:language.Culture%>
                </td>
              
                <td>
                   <%:Html.RouteLink("View", new { controller = "Languages", action = "ShowById", id = language.Id })%>
                   <%:Html.RouteLink("Edit", new { controller = "Languages", action = "Edit", id = language.Id })%>
                  
                 <% using (Html.BeginForm(LanguagesMVC.Languages.Remove(language.Id), FormMethod.Post))
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
         <%:Html.RouteLink("Create Language", new { controller = "Languages", action = "New" })%>
      </li>
    </ul>
  </div>
</asp:Content>
