<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsArticleViewerWidgetModel>" %>

<%@ Import Namespace="Core.News.Helpers" %>

<div class="widget-news">
        <%for (var i = Model.CurrentPage * Model.ItemsOnPage; i < (Model.NewsArticles.Count < (Model.CurrentPage + 1) * Model.ItemsOnPage ? Model.NewsArticles.Count : (Model.CurrentPage + 1) * Model.ItemsOnPage); i++)
          { %>
            <div class="newslist">
                <p>
                    <a onclick="RedirectToDetales();" href="<%= Request.Url + (!Request.Url.ToString().Contains("newsvidgetid=" + Model.Id) ? (Request.Url.ToString().Contains("?") ? "&" : "?") + "newsvidgetid=" + Model.Id + "&articleid" + Model.Id + "=" + Model.NewsArticles[i].Id : "&articleid" + Model.Id + "=" + Model.NewsArticles[i].Id) %>">
                        <b><%=Model.NewsArticles[i].Title%></b>
                    </a>
                </p>
                <p>
                    <%=Model.NewsArticles[i].Summary%>
                </p>
                <p>
                    <%=Html.Translate(".Added") %> <%=Model.NewsArticles[i].LastModifiedDate.ToString("dd.MM.yy")%>
                </p>
            </div>
        <%} %>
        <%if (Model.ShowPaginator){%>
        <%=Html.Pager(Model.ItemsOnPage, Model.CurrentPage, Model.TotalItemsCount, Model.Id, Request.Url.ToString())%>
        <%}%>
</div>
<script>
    function RedirectToDetales() {var t = document.location.href; }
</script>