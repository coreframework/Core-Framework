<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Admin.Models.PermissionOperationsModel>" %>

<% using (Html.BeginForm("ApplyPermissions", "Role"))
   { %>
    <table id="operations" class="index">
        <thead>
        <tr> 
            <th class="chbx"><%: Html.CheckBox("checkAll")%></th>
            <th class="row"><%: Html.Translate(".Operation") %></th>
        </tr>
        </thead>
        <tbody>
            <%foreach (var operation in Model.Operations) {%>
                <tr>
                    <td>
                        <%: Html.HiddenFor(model=>model.RoleId) %>
                        <%: Html.HiddenFor(model=>model.ResourceId) %>
                        <%: Html.HiddenFor(model=>model.Area) %>
                        <input type="checkbox" <%= Model.Permissions != null && (Model.Permissions.Permissions & operation.Key)==operation.Key?"checked=\"checked\"":"" %> name="OperationIds" value="<%= operation.Key%>" />
                    </td>
                    <td><%:Html.Encode(operation.Title) %></td>
                </tr>
            <%}%>
        </tbody>
    </table>
    <p class="buttons">
        <%: Html.Submit("Save")%>
    </p>
     <script type="text/javascript">
         $(function () {
             $("#checkAll").bind_select_all();
         });
    </script>
<%} %>