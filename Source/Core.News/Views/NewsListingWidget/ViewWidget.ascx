<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsListingWidgetViewModel>" %>
<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>
<%@ Import Namespace="Core.News.Helpers" %>
<%@ Import Namespace="Core.News.Nhibernate.Models" %>
<div class="widget-news">
    <%foreach (var newsArticle in Model.NewsArticles)
      {%>
    <div class="newslist">
        <p>
            <a href="<%=Url.RouteUrl("NewsDetails.Show", new {newsId = Model.LinkMode.Equals(NewsDetailsLinkMode.Id) ? newsArticle.Id.ToString() : newsArticle.Url})%>">
                <b>
                    <%=newsArticle.Title%></b> </a>
        </p>
        <p>
            <%=newsArticle.Summary%>
        </p>
        <p>
            <%=Html.Translate(".Added") %>
            <%=newsArticle.LastModifiedDate.ToString(NewsConstants.DateFormat)%>
        </p>
    </div>
    <%
      }%>
    <%if (Model.ShowPaginator)
      {%>
    <%=Html.Pager(Model.ItemsOnPage, Model.CurrentPage, Model.TotalItems, Model.Id, Model.Url)%>
    <%}%>
</div>
