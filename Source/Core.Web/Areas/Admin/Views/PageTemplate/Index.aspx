<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.Mvc.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.Mvc.Grids.JqGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%:Html.Translate("Titles.PageTemplates") %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1>
        <%:Html.Translate("Titles.PageTemplates")%></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
        <div class="e_table_bottom clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <input id="New" type="button" class="button" value="<%: Html.Translate("Actions.New") %>" /><strong></strong></div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action(MVC.Admin.PageTemplate.New()) %>"; }); });
    </script>
</asp:Content>
