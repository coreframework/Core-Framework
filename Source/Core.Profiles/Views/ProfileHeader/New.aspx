<%@ Page Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Profiles.Models.ProfileHeaderViewModel>" %>
<%@ Assembly Name="Core.Profiles"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("NewProfileHeader", "Profiles.Views.ProfileType")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%:Html.Translate("NewProfileHeader", "Profiles.Views.ProfileType")%>
  </h1>
  <div class="tabs clrfix">
	<ul class="i-tab clrfix">
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Details", "Profiles.Views.ProfileType"), "Edit", "ProfileType")%>
            </span>
            <strong></strong>
        </li>
        <li class="active">
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Elements", "Profiles.Views.ProfileType"), "Show", "ProfileElement")%>
            </span>
            <strong></strong>
        </li>
	</ul>
  </div>
  <div class="tabs_b"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm()) {%>
        <div class="i_form clrfix">    
            <% Html.RenderPartial("HeaderDetails", Model); %>
        </div>
  <% }%>
</asp:Content>