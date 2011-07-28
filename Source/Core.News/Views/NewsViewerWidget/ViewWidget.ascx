<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Nhibernate.Models.NewsArticleWidget>" %>

<div>
    <ul class="widgets">
        <%for (var i = 0; i < (Model.NewsArticles.Count < Model.ItemsOnPage ? Model.NewsArticles.Count : Model.ItemsOnPage); i++){ %>
            <li>
                <p>
                    <a href="<%=Request.Url + (Request.Url.ToString().Contains("?") ? "&" : "?") + "newsvidgetid=" + Model.Id + "&articleid" + Model.Id + "=" + Model.NewsArticles[i].Id %>">
                        <b><%=Model.NewsArticles[i].Title%></b>
                    </a>
                </p>
                <p>
                    <%=Model.NewsArticles[i].Summary%>
                </p>
                <p>
                    <%=Html.Translate(".Added") %> <%=Model.NewsArticles[i].LastModifiedDate.ToString("dd.MM.yy")%>
                </p>
                <hr />
            </li>
        <%} %>
    </ul>
</div>
