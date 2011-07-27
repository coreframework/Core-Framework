<%@ Assembly Name="Products" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.CategoryWidgetModel>" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
     <div class="form_i">  
        <%:Html.CustomLabelFor(model => model.PageSize)%><br/>
        <%:Html.TextBoxFor(model => model.PageSize, new { Class = "inp_txt" })%><br/>
        <%:Html.ValidationMessageFor(model=>model.PageSize) %>
    </div>
    <%:Html.AntiForgeryToken()%>  
</div>
