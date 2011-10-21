<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsDetailsWidgetViewModel>" %>
<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>
<%if (Model != null)
  {%>
<div>
    <h1>
        <%=Model.Title%></h1>
    <p>
        <b>
            <%=Model.Summary%>
        </b>
    </p>
    <p>
        <%=Model.Content%>
    </p>
    <p>
        <%=Html.Translate(".Added") %>
        <%=Model.LastModifiedDate%>
    </p>
</div>
<%} %>