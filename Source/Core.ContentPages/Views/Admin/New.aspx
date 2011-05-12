<%@ Assembly Name="Core.ContentPages" %>
<%@ Import Namespace="Core.ContentPages.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<ContentPageViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm())
         {%>    
          <div class="form_area">     
              <%:Html.EditorFor(model => model.Title)%>
              <%:Html.EditorFor(model => model.Content)%>
              <%:Html.AntiForgeryToken()%>
         </div>
           <p class="buttons">
                  <%: Html.Submit("Save")%>
                    <%:Html.RouteLink("Cancel", new { controller = "ContentPage", action = "ShowAll"})%>
             </p>
          <% }%>
      </div>
</asp:Content>