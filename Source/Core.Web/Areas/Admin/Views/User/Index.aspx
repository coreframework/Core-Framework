<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.Translate(".Title") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="New" type="button" class="button" value="<%: Html.Translate(".New") %>" /><strong></strong></div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action(MVC.Admin.User.New()) %>"; }); });
    </script>
<%--    <div id="actions">
        <ul>
            <li><a href="<%: Url.Action(MVC.Admin.User.New()) %>">
                <img src="<%: Links.Content.Images.Admin.plus_png %>" alt="Plus"></a>
            </li>
        </ul>
    </div>--%>
</asp:Content>
