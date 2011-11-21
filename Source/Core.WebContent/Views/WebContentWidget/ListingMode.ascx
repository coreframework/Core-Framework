<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WidgetListingModel>" %>
<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>
<%@ Import Namespace="Core.WebContent.Models" %>

<%foreach (var article in Model.Articles) {%>
    <%Html.RenderPartial("DetailsMode", new WidgetDetailsModel(article, false));%>
<%
} %>