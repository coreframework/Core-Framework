﻿<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%=Html.Translate(".Products") %></h1>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="New" type="button" class="button" value="<%=Html.Translate(".NewProduct") %>" /><strong></strong>
            </div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action("New","Product") %>"; }); });
    </script>
</asp:Content>


