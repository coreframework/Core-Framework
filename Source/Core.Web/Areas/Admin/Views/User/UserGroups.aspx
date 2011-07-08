<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="Save" type="button" class="button" value="<%: Html.Translate(".Save") %>" /><strong></strong></div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#Save').click(function () {
                $.post('<%= Url.Content(Request.RawUrl + "/UpdateUserGroups/") %>' + '?' + decodeURIComponent($.param({ ids: $('#list').getDataIDs() }, true)) + '&' + decodeURIComponent($.param({ selids: $('#list').getGridParam('selarrrow') }, true)),
                function (data) { window.location = '<%= Url.Content(Request.RawUrl) %>'; });
            });
        });
    </script>

<%--  <% using (Html.BeginForm(MVC.Admin.User.UpdateUserGroups(), FormMethod.Post)) {%>
    <%: Html.HttpMethodOverride(HttpVerbs.Put) %>


    <ul>
      <% if (Model.UserGroups.Length > 0) { %>
        <li>
          <%: Html.CheckBox("All") %>
          <%: Html.Label("All", Html.Translate(".All")) %>
        </li>
      <% } %>
      <% for (var i = 0; i < Model.UserGroups.Length; i++)
         { %>
        <li>
          <%: Html.HiddenFor(x => x.UserGroups[i].Id)%>
          <%: Html.HiddenFor(x => x.UserGroups[i].Name)%>
          <%: Html.CheckBoxFor(x => x.UserGroups[i].Assigned)%>
          <%: Html.LabelFor(x => x.UserGroups[i].Assigned, Model.UserGroups[i].Name)%>
        </li>
      <% } %>
    </ul>
    <p class="buttons">
      <%: Html.Submit(Html.Translate(".Save"))%><%: Html.ActionLink(Html.Translate(".Cancel"), MVC.Admin.User.Index()) %>
    </p>
  <% } %>--%>

<%--  <script type="text/javascript">

      $(function () {
          $("#All").bind_select_all();
      });

  </script>--%>
</asp:Content>