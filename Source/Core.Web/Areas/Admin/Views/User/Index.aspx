<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%: Html.Translate(".Title") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="outset">
        <%=Html.JqGrid(model => model.SearchString) %>
    </div>
    <div id="actions">
        <ul>
            <li><a href="<%: Url.Action(MVC.Admin.User.New()) %>">
                <img src="<%: Links.Content.Images.Admin.plus_png %>" alt="Plus"><%: Html.Translate(".New") %></a>
            </li>
        </ul>
    </div>
</asp:Content>
