<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"Inherits="System.Web.Mvc.ViewPage<Core.Profiles.Models.ProfileTypeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("NewProfileType", "Profiles.Views.ProfileType")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%:Html.Translate("NewProfileType", "Profiles.Views.ProfileType")%>
  </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm()) {%>
        <div class="i_form clrfix">    
            <% Html.RenderPartial("ProfileTypeDetails", Model); %>
        </div>
  <% }%>
</asp:Content>