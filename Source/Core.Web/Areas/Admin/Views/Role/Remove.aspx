﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.RoleViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1>
        <%: String.Format(Html.Translate(".Title"), Model) %></h1>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <p>
        <%: String.Format(Html.Translate(".AreYouSure"), Model) %></p>
    <% using (Html.BeginForm(MVC.Admin.Role.ConfirmRemove(Model.Id), FormMethod.Post))
       {%>
    <%: Html.HttpMethodOverride(HttpVerbs.Delete) %>
    <p class="buttons">
        <%: Html.Submit(Html.Translate("Actions.Remove"))%>
        <%: Html.ActionLink(Html.Translate("Actions.Cancel"), MVC.Admin.Role.Index())%>
    </p>
    <% } %>
</asp:Content>
