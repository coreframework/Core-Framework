<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.CategoryLocaleViewModel>" %>

<div class="cols clrfix">
    <div class="fst_col colls_i" style="width: 60%;">
        <%:Html.HiddenFor(model => model.SelectedCulture) %>
        <div class="i_form_i">
            <%=Model.Title%>
        </div>
        <div class="i_form_i">
            <%=Model.Description%>
        </div>       
    </div>
</div>