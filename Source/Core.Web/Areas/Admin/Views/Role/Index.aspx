<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>

<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: Html.Translate(".Title") %></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%: Html.Translate(".Title") %></h1>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div class="outset">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="New" type="button" class="button" value="<%: Html.Translate("Actions.New") %>" /><strong></strong></div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action(MVC.Admin.Role.New()) %>"; }); });
    </script>
</asp:Content>
