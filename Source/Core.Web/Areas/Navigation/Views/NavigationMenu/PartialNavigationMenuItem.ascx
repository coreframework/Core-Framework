<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.PartialNavigationMenuItemModel>" %>

<%@ Import Namespace="Core.Web.Models" %>

<%if (Model.Page!=null) {%>
    <%=Html.Hidden("menuPageId", Model.Page.Id) %>
    <%= Html.ActionLink(Html.Encode(Model.Page.Title), MVC.Pages.Show(Model.Page.Url)).ToString()%>
   
<%} %>
 
