<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsArticleViewerWidgetModel>" %>

<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>
<%@ Import Namespace="Core.News.Helpers" %>
<div class="widget-news">
        <%for (var i = Model.CurrentPage * Model.ItemsOnPage; i < (Model.NewsArticles.Count < (Model.CurrentPage + 1) * Model.ItemsOnPage ? Model.NewsArticles.Count : (Model.CurrentPage + 1) * Model.ItemsOnPage); i++)
          { %>
            <div class="newslist">
                <p>
                    <a href="<%=Url.RouteUrl("NewsDetails.Show", new {newsId = Model.NewsArticles[i].Id})%>">
                        <b><%=Model.NewsArticles[i].Title%></b>
                    </a>
                </p>
                <p>
                    <%=Model.NewsArticles[i].Summary%>
                </p>
                <p>
                    <%=Html.Translate(".Added") %> <%=Model.NewsArticles[i].LastModifiedDate.ToString(NewsConstants.DateFormat)%>
                </p>
            </div>
        <%} %>
        <%if (Model.ShowPaginator){%>
        <%=Html.Pager(Model.ItemsOnPage, Model.CurrentPage, Model.TotalItemsCount, Model.Id, Model.Url)%>
        <%}%>
</div>
