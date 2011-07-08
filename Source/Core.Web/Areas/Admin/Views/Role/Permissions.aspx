﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.RolePermissionsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Html.Translate(".Permissions") %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%: Html.Translate(".Permissions") %></h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%using (Html.BeginForm(MVC.Admin.Role.Permissions(), FormMethod.Get)) {%>
        <%= Html.OptionGroupDropDownList(
                        "resource",
                        Model.PermissibleObjects,
                        permissionItem => permissionItem.Area,
                                    permissionItem => String.Format("{0}_{1}", permissionItem.Id, (int)permissionItem.Area),
                        permissionItem =>
                        String.IsNullOrEmpty(permissionItem.Title)
                            ? String.Empty
                            : permissionItem.Title,
                        permissionItem =>
                        String.Format("{0}_{1}", permissionItem.Id, (int)permissionItem.Area)==
                                    (Model.ResourceId.HasValue ? String.Format("{0}_{1}", Model.ResourceId, (int)Model.Area) : String.Empty), true)%>
        
        <script type='text/javascript'>
            $(document).ready(function () {
                $('#resource').change(function () {
                    this.form.submit();
                });
            });
        </script>
     <%}%>

    <br /> <br />
    <%if (Model.OperationsModel != null && Model.OperationsModel.Operations != null)
      {%>
        <% Html.RenderPartial(MVC.Admin.Role.Views.PermissionOperations, Model.OperationsModel);%>
    <% }
      else
      {%>
       <%: Html.Translate(".SelectPermissionSection") %>
    <% }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
