<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WidgetListingModel>" %>
<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>
<%@ Import Namespace="Core.WebContent.Models" %>
 
<%var curIndex = 1; %>
<%foreach (var article in Model.Articles) {%>
    <%Html.RenderPartial("DetailsMode", new WidgetDetailsModel(article, false));%>
    <%if (curIndex!= Model.Articles.Count())
      {%>
        <div class="delimiter"></div>
    <%} %>
     <%curIndex++; %>
<%} %>