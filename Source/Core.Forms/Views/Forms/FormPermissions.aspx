<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Assembly Name="Core.Framework.Permissions" %>
<%@ Page Title="" Language="C#"  MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Framework.Permissions.Models.PermissionsModel>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% using (Ajax.BeginForm("ApplyPermissions", "Forms", new {area = "Forms"}, new AjaxOptions() { OnComplete = "completePermissionsUpdates"}))
       { %>
       <%:Html.HiddenFor(model=>model.EntityId) %>
          <div class="e_table_area">
            <table class="e_table">
                <tbody>
                    <tr>
                        <th>
                            <span></span>
                            Role
                        </th>
                        <%foreach (var operation in Model.Operations) {%>
                            <th>
                                <span></span>
                                <%=Html.Encode(operation.Title)%>
                            </th>
                        <%} %>
                    </tr>
                    <%foreach (var role in Model.Roles) {%>
                     <tr>
                        <td>
                            <%=Html.Encode(role.Name) %>
                        </td>
                        <%foreach (var operation in Model.Operations) {%>
                            <td class="chbx" style="width: 100px;">
                                <input type="checkbox" <%=Model.Permissions.Where(permission=>permission.RoleId==role.Id && (permission.Permissions & operation.Key) == operation.Key).Count()>0?"checked=\"checked\"":"" %> value="<%=String.Format("{0}_{1}", role.Id, operation.Key) %>" name="actions">
                            </td>
                        <%}%>
                       </tr>
                    <%} %>
                </tbody>
            </table>
		    <div class="e_table_bottom clrfix">
			    <div class="btn1 clrfix">
                    <em></em><%: Html.Submit("Save",new { @class="button"})%><strong></strong>
                </div>
		    </div>
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
