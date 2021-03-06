﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"Inherits="System.Web.Mvc.ViewPage<Core.WebContent.Models.SectionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("NewSection", "WebContent.Views.Section")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%:Html.Translate("NewSection", "WebContent.Views.Section")%>
  </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm()) {%>
        <div class="i_form clrfix">    
            <% Html.RenderPartial("SectionDetails", Model); %>
        </div>
  <% }%>
</asp:Content>