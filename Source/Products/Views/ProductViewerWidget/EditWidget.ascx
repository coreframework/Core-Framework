<%@ Assembly Name="Products" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.ProductWidgetModel>" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i"> 
        <%:Html.LocalizedLabelFor(model => model.CategoriesId)%> <br/>
        <%:Html.ListBoxFor(model => model.CategoriesId, new MultiSelectList(Model.Categories, "Id", "Title", Model.CategoriesId), new { style="height:140px" })%>
        <%:Html.ValidationMessageFor(model => model.CategoriesId)%>
    </div>
    <div class="form_i">   
        <%:Html.LocalizedLabelFor(model => model.PageSize)%><br/>    
        <%:Html.TextBoxFor(model => model.PageSize, new { Class = "inp_txt" })%><br/>
        <%:Html.ValidationMessageFor(model=>model.PageSize) %>
    </div>
    <%:Html.AntiForgeryToken()%>  
</div>
