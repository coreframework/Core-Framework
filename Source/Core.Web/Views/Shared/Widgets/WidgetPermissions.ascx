<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PermissionsModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions" %>
<div>
    <h2 class="settings-header">
        <%=Html.Translate(".Permissions") %></h2>
    <div>
        <% using (Ajax.BeginForm("ApplyWidgetPermissions", "Pages", new AjaxOptions() { OnComplete = "completePermissionsUpdates" }))
           { %>
        <%:Html.HiddenFor(model=>model.EntityId) %>
        <table class="permissions-grid">
            <thead>
                <th>
                    <%=Html.Translate(".Role") %>
                </th>
                <%foreach (var operation in Model.Operations)
                  {%>
                <th>
                    <%=Html.Encode(operation.Title)%>
                </th>
                <%} %>
            </thead>
            <tbody>
                <%foreach (var role in Model.Roles)
                  {%>
                <tr>
                    <td>
                        <%=Html.Encode(role.Name) %>
                    </td>
                    <%foreach (var operation in Model.Operations)
                      {%>
                    <td class="chbx">
                        <%=Html.OperationCheckbox("actions",role.Id,operation.Key,Model.Permissions.Where(permission=>permission.Role==role))%>
                    </td>
                    <%}%>
                </tr>
                <%} %>
            </tbody>
        </table>
        <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
            <div class="ui-dialog-buttonset">
                <%: Html.Submit("Save", new { Class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" })%>
            </div>
        </div>
        <%} %>
    </div>
</div>
<script type="text/javascript">
    function completePermissionsUpdates(content) {

        if (content.get_data().length > 0) {
            var pageUrl = content.get_data();
            location.href = pageUrl;
        }
    }
</script>
