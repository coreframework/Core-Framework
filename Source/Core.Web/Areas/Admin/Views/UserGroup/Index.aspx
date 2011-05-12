<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.GridViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: Html.Translate(".Title") %></asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <div class="outset">
        <%= Html.Partial(MVC.Admin.Shared.Views.Grid, Model)%>
    </div>
    <div id="actions">
        <ul>
            <li><a href="<%: Url.Action(MVC.Admin.UserGroup.New()) %>">
                <img src="<%: Links.Content.Images.Admin.plus_png %>" alt="Plus"><%: Html.Translate(".New") %></a>
            </li>
        </ul>
    </div>
</asp:Content>
