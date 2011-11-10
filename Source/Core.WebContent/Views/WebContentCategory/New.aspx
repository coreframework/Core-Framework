<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"Inherits="System.Web.Mvc.ViewPage<Core.WebContent.Models.CategoryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("NewCategory", "WebContent.Views.Category")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%:Html.Translate("NewCategory", "WebContent.Views.Category")%>
  </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm()) {%>
        <div class="i_form clrfix">    
            <% Html.RenderPartial("CategoryDetails", Model); %>
        </div>
  <% }%>
</asp:Content>