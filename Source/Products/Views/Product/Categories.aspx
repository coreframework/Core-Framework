<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Framework.Mvc.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.Mvc.Grids.JqGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
	<h1><%: String.Format(Html.Translate(".Title"), Model.GridTitle) %></h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>	
        <div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="Save" type="button" class="button" value="<%: Html.Translate(".Save") %>" /><strong></strong></div>
		</div>
    </div>
      <script type="text/javascript">
          $(function () {
              $('#Save').click(function () {
                  $.post('<%= Url.Content(Request.RawUrl + "/UpdateCategories/") %>' + '?' + decodeURIComponent($.param({ ids: $('#list').getDataIDs() }, true)) + '&' + decodeURIComponent($.param({ selids: $('#list').getGridParam('selarrrow') }, true)),
                function (data) { window.location = '<%= Url.Content(Request.RawUrl) %>'; });
              });
          });
    </script>
</asp:Content>
