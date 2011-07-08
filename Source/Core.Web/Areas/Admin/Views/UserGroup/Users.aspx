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
                $.post('<%= Url.Content(Request.RawUrl + "/UpdateUsers/") %>' + '?' + decodeURIComponent($.param({ ids: $('#list').getDataIDs() }, true)) + '&' + decodeURIComponent($.param({ selids: $('#list').getGridParam('selarrrow') }, true)),
                function (data) { window.location = '<%= Url.Content(Request.RawUrl) %>'; });
            });
        });
    </script>
</asp:Content>