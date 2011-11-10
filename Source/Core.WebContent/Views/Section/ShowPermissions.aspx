<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Framework.Permissions.Models.PermissionsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("Permissions", "WebContent.Views.Section")%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%:Html.Translate("Permissions", "WebContent.Views.Section")%></h1>
    <div class="tabs clrfix">
	    <ul class="i-tab clrfix">
            <li>
                <em></em>
                <span>
                 <%:Html.ActionLink(Html.Translate("Details", "WebContent.Views.Section"), "Edit") %>
                </span>
                <strong></strong>
            </li>
            <li class="active">
                <em></em>
                <span>
                 <%:Html.ActionLink(Html.Translate("Permissions", "WebContent.Views.Section"), "ShowPermissions") %>
                </span>
                <strong></strong>
            </li>
	    </ul>
   </div>
   <div class="tabs_b"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <% using (Ajax.BeginForm("ApplyPermissions", "Section", new {area = "WebContent"}, new AjaxOptions() { OnComplete = "completePermissionsUpdates"}))
       { %>
       <%:Html.HiddenFor(model=>model.EntityId) %>
          <div class="e_table_area">
            <table class="e_table">
                <tbody>
                    <tr>
                        <th>
                            <span></span>
                            <%:Html.Translate("Role", "WebContent.Views.Section")%>
                        </th>
                        <%foreach (var operation in Model.Operations) {%>
                            <th>
                                <span></span>
                                <%=Html.Translate("SectionOperations." + Html.Encode(operation.Title), "WebContent.Models")%>
                            </th>
                        <%} %>
                    </tr>
                    <%foreach (var role in Model.Roles) {%>
                     <tr>
                        <td>
                            <%=Html.Encode(role.Name) %>
                        </td>
                        <%foreach (var operation in Model.Operations) {%>
                            <td class="chbx" style="width: 200px;">
                                <input type="checkbox" <%=Model.Permissions.Where(permission=>permission.RoleId==role.Id && (permission.Permissions & operation.Key) == operation.Key).Count()>0?"checked=\"checked\"":"" %> value="<%=String.Format("{0}_{1}", role.Id, operation.Key) %>" name="actions">
                            </td>
                        <%}%>
                       </tr>
                    <%} %>
                </tbody>
            </table>
		    <div class="e_table_bottom clrfix">
			    <div class="btn1 clrfix">
                    <em></em><%: Html.Submit(Html.Translate("Actions.Save"), new { @class = "button" })%><strong></strong>
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