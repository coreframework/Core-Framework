<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.CategoryViewWidgetModel>" %>
<%@ Import Namespace="Products.Helpers" %>

<div id="products<%=Model.Id %>">
   <% foreach (var category in Model.Categories){%>
  <div>
  <p><%:Html.ActionLink(category.Title, "Category", new {id=category.Id}) %></p>
 
   </div>
   <% }%> 

 <%= Html.Pager(Model.PageSize, Model.CurrentPage, Model.TotalItemsCount, Model.Id, "Cat", new { currentRequestParams = Request.Params, isAjax = TempData["isAjax"] ?? false })%>                                                                        
</div>