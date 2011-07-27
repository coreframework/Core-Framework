<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Nhibernate.Models.NewsArticle>" %>

<%@ Import Namespace="Core.News.Helpers" %>

<div>
    <h1><%=Model.Title%></h1>
    <p>
        <%=Model.Content%>
    </p>
    <p>
        <a href="<% =NewsViewerWidgetHelper.GetBackUrl(Request.Url.ToString(), Model.WidgetId, Model.Id) %>"><%=Html.Translate(".Back") %></a>
    </p>
</div>
