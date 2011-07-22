<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Admin.Models.PermissionOperationsModel>" %>

<% using (Html.BeginForm("ApplyPermissions", "Role"))
   { %>
        <div class="e_table_area">
        <table class="e_table">
            <tbody>
                <tr>
                    <th class="chbx" style="text-align: center; width: 60px; padding: 8px 0 3px 0px;"><%: Html.CheckBox("checkAll")%></th>
                    <th class="row"><span></span><%: Html.Translate(".Operation") %></th>
                </tr>
            <%foreach (var operation in Model.Operations) {%>
                <tr>
                    <td style="text-align: center; width: 60px; padding-left: 0px;">
                        <%: Html.HiddenFor(model=>model.RoleId) %>
                        <%: Html.HiddenFor(model=>model.ResourceId) %>
                        <%: Html.HiddenFor(model=>model.Area) %><span></span>
                        <input type="checkbox" <%= Model.Permissions != null && (Model.Permissions.Permissions & operation.Key)==operation.Key?"checked=\"checked\"":"" %> name="OperationIds" value="<%= operation.Key%>" />
                    </td>
                    <td><%:Html.Encode(operation.Title) %></td>
                </tr>
            <%}%>
            </tbody>
        </table>
		    <div class="e_table_bottom clrfix">
			    <div class="btn1 clrfix">
                    <em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { @class = "button" })%><strong></strong>
                </div>
		    </div>
        </div>
     <script type="text/javascript">
         $(function () {
             $("#checkAll").bind_select_all();
         });
    </script>
<%} %>