<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %><%@ Import Namespace="System.Web.Mvc" %>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>Forms</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="New" type="button" class="button" value="Add Form" /><strong></strong></div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action("New","Forms") %>"; }); });
    </script>

<%--     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th style="width:90%;"><%:Html.Translate(".Title") %></th>
                <th><%:Html.Translate(".Actions") %></th>
            </tr>
            </thead>
            <tbody>
            <% foreach (var form in Model){ %>
            <tr>
                <td>
                    <%:form.Title%>
                </td>
                <td>
                   <%:Html.RouteLink(Html.Translate(".Permissions"), new { controller = "Forms", action = "ShowPermissions", formId = form.Id })%><br/>
                   <%:Html.RouteLink(Html.Translate(".Details"), new { controller = "Forms", action = "Edit", formId = form.Id })%><br/>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>
   <div id="actions">
    <ul>
      <li>
         <%:Html.RouteLink(Html.Translate(".CreateForm"), new { controller = "Forms", action = "New" })%>
      </li>
    </ul>
  </div>--%>
</asp:Content>
