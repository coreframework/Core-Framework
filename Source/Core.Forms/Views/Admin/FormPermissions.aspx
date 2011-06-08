<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Assembly Name="Core.Framework.Permissions" %>
<%@ Page Title="" Language="C#"  MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Framework.Permissions.Models.PermissionsModel>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% using (Ajax.BeginForm("ApplyPermissions", "Forms", new {area = "Forms"}, new AjaxOptions() { OnComplete = "completePermissionsUpdates"}))
       { %>
       <%:Html.HiddenFor(model=>model.EntityId) %>
          <div class="outset">
            <table class="index">
                <thead>
                    <th>
                        Role
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
                                <input type="checkbox" <%=Model.Permissions.Where(permission=>permission.RoleId==role.Id && (permission.Permissions & operation.Key) == operation.Key).Count()>0?"checked=\"checked\"":"" %> value="<%=String.Format("{0}_{1}", role.Id, operation.Key) %>" name="actions">
                            </td>
                        <%}%>
                       </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
        <div id="actions">
          <ul>
          <li>
             <%: Html.Submit("Save")%>
          </li>
        </ul>
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

</asp:Content>
