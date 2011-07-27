<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.ProductLocaleViewModel>" %>
<div class="cols clrfix">
    <div class="fst_col colls_i" style="width: 60%;">
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Title)%>
        </div>
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Description)%>
        </div>
        <div class="i_form_i">
            <%:Html.AntiForgeryToken()%>
        </div>
    </div>
</div>
