<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Framework.Mvc.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.Mvc.Grids.JqGrid" %>
<%@ Import Namespace="Core.WebContent.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("Titles.ArticleFiles", "WebContent")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%:Html.Translate("Titles.ArticleFiles", "WebContent")%></h1>
    <div class="tabs clrfix">
	<ul class="i-tab clrfix">
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Details", "WebContent.Views.Article"), "Edit")%>
            </span>
            <strong></strong>
        </li>
        <li class="active">
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Files", "WebContent.Views.Article"), "ShowFiles")%>
            </span>
            <strong></strong>
        </li>
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Permissions", "WebContent.Views.Article"), "ShowPermissions")%>
            </span>
            <strong></strong>
        </li>
	</ul>
  </div>
  <div class="tabs_b"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
        <%if (ViewData["Article"] is ArticleViewModel && ((ArticleViewModel)ViewData["Article"]).AllowManage)
          {%>
            <div class="e_table_bottom clrfix">
                <div class="btn1 clrfix">
                    <em></em>
                    <input id="New" type="button" class="button" value="<%:Html.Translate("AddNewFile","WebContent.Views.Article") %>" />
                    <strong></strong>
                </div>
            </div>
            <script type="text/javascript">
                $(function () { $('#New').click(function () { window.location = "<%:Url.Action("NewFile", "Article")%>"; }); });
            </script>
        <% }%>
    </div>
</asp:Content>

