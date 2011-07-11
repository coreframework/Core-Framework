﻿<%@ Assembly Name="Core.Languages" %>
<%@ Import Namespace="Core.Languages.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<LanguageViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm())
         {%>    
          <div class="form_area">     
              <%:Html.EditorFor(model => model.Title)%>
              <%:Html.EditorFor(model => model.Code)%>
              <%:Html.EditorFor(model => model.Culture)%>
              <%:Html.AntiForgeryToken()%>
         </div>
           <p class="buttons">
                  <%: Html.Submit("Save")%>
                    <%:Html.RouteLink("Cancel", new { controller = "Languages", action = "ShowAll" })%>
             </p>
          <% }%>
      </div>
</asp:Content>