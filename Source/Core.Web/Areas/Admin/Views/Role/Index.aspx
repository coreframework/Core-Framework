<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>

<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>
<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: Html.Translate(".Title") %></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div class="outset">
        <%=Html.JqGrid(model => model.SearchString) %>
    </div>
    <div id="actions">
        <ul>
            <li><a href="<%: Url.Action(MVC.Admin.Role.New()) %>">
                <img src="<%: Links.Content.Images.Admin.plus_png %>" alt="Plus"><%: Html.Translate(".New") %></a>
            </li>
        </ul>
    </div>
</asp:Content>
