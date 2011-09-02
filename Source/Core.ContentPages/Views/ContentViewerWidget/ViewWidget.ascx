<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.ContentPages.NHibernate.Models.ContentPageWidget>" %>

<%=Html.RegisterScript("test.js")%>
<%=Html.RegisterScript("test2.js")%>

<div>
   <%=Model.ContentPage.Content%>
</div>
