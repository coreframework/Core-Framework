<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.ProductViewWidgetModel>" %>
<%@ Import Namespace="Products.Helpers" %>
<div class="widget-products">
    <% foreach (var product in Model.Products)
       {%>
    <div class="product <%= Model.Products.Last().Id == product.Id ? "last" : ""%>">
        <%if (!String.IsNullOrEmpty(product.FileName))
          {%>
        <div class="imgHolder">
            <img src="<%=product.FileName%>" />
        </div>
        <% } %>
        <div class="title">
            <h3>
             <%=Html.DetailsLink(product.Title, Model.Id, product.Id, new { currentRequestParams = Request.Params, isAjax = TempData[ProductConstants.IsAjaxPageQueryRequestParam] ?? false })%>
            </h3>
        </div>
        <div class="price">
            <span>
                <%=Html.Translate(".Price") %>:</span>
            <%=product.Price %>$</div>
        <div class="description">
            <%=product.Description %>
        </div>
        <div class="more">
            <%=Html.DetailsLink(Html.Translate(".More") + " »", Model.Id, product.Id, new { currentRequestParams = Request.Params, isAjax = TempData[ProductConstants.IsAjaxPageQueryRequestParam] ?? false })%></div>
    </div>
    <% }%>
    <%= Html.Pager(Model.PageSize, Model.CurrentPage, Model.TotalItemsCount, Model.Id, new { currentRequestParams = Request.Params, isAjax = TempData[ProductConstants.IsAjaxPageQueryRequestParam] ?? false })%>
</div>
