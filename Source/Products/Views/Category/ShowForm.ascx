<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.CategoryLocaleViewModel>" %>

<div class="cols clrfix">
    <div class="fst_col colls_i">
        <%:Html.HiddenFor(model => model.SelectedCulture) %>
        <div class="i_form_i">
            <%:Html.LocalizedLabelFor(model=>model.Title) %><em>:</em>
            <%=Model.Title%>
        </div>
        <div class="i_form_i">
         <%:Html.LocalizedLabelFor(model => model.Description)%><em>:</em>
            <%=Model.Description%>
        </div>       
    </div>
</div>