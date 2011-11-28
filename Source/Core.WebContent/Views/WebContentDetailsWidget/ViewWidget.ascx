<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WidgetDetailsModel>" %>

<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>

<%
    Html.RenderPartial("WebContentWidget/DetailsMode", Model);%>