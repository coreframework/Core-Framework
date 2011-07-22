<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Framework.Permissions.Models.PermissionsModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions" %>

<% using (Ajax.BeginForm("ApplyPagePermissions", "Pages", new AjaxOptions() { OnComplete = "completePermissionsUpdates" }))
   { %>
   <%:Html.HiddenFor(model=>model.EntityId) %>
     <div class="form_area overflow_x_a">
        <table class="permissions-grid">
            <thead>
                <th>
                    <%=Html.Translate(".Role") %>
                </th>
                <%foreach (var operation in Model.Operations) {%>
                    <th>
                        <%=Html.Encode(operation.Title)%>
                    </th>
                <%} %>
            </thead>
            <tbody>
                <%foreach (var role in Model.Roles) {%>
                 <tr>
                    <td>
                        <%=Html.Encode(role.Name) %>
                    </td>
                    <%foreach (var operation in Model.Operations) {%>
                        <td class="chbx">
                             <%=Html.OperationCheckbox("actions", role.Id, operation.Key, Model.Permissions.Where(permission => permission.RoleId == role.Id))%>
                        </td>
                    <%}%>
                   </tr>
                <%} %>
            </tbody>
        </table>
    </div>
     <div class="p_footer clrfix">
	    <div class="btn1"><em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
    </div>
<%} %>
<script type="text/javascript">
    function completePermissionsUpdates(content) {

        if (content.get_data().length > 0) {
            var pageUrl = content.get_data();
            location.href = pageUrl;
        }
    }
</script>