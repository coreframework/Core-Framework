<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Nhibernate.Models.NewsArticleWidget>" %>

<div>
    <ul class="widgets">
        <%for (var i = 0; i < (Model.NewsArticles.Count < Model.ItemsOnPage ? Model.NewsArticles.Count : Model.ItemsOnPage); i++){ %>
            <li>
                <a href="<%=Request.Url + (Request.Url.ToString().Contains("?") ? "&" : "?") + "newsvidgetid=" + Model.Id + "&articleid" + Model.Id + "=" + Model.NewsArticles[i].Id %>">
                    <%=Model.NewsArticles[i].Title%>
                </a>
            </li>
        <%} %>
    </ul>
</div>
