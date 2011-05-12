<%@ Assembly Name="Core.ContentPages" %>
<%@ Assembly Name="Core.ContentPages.NHibernate" %>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.ContentPages.NHibernate.Models.ContentPageWidget>" %>

<div>
   <%=Model.ContentPage.Content%>
</div>
