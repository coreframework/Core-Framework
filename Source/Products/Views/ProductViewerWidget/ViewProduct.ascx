<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.NHibernate.Models.Product>" %>
<%@ Import Namespace="Products.Helpers" %>
<div class="widget-products">
    <div class="product">
        <%if (!String.IsNullOrEmpty(Model.FileName))
          {%>
        <div class="imgHolder">
            <img src="<%=Model.FileName%>" />
        </div>
        <% } %>
        <div class="title">
            <h3>
                <%=Model.Title %></h3>
        </div>
        <div class="price">
            <span>
                <%=Html.Translate(".Price") %>:</span>
            <%=Model.Price %>$</div>
        <div class="full_description">
            <%=Model.Description %></div>
        <div class="back">
            <%= Html.ListLink("« " + Html.Translate(".Back"), (long)TempData[ProductConstants.ProductWidgetIdQueryRequestParam], new { currentRequestParams = Request.Params, isAjax = TempData[ProductConstants.IsAjaxPageQueryRequestParam] ?? false })%>
        </div>
    </div>
</div>
