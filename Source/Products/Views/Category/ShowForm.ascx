<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Products.Models.CategoryLocaleViewModel>" %>

<div class="cols clrfix">
    <div class="fst_col colls_i">
        <%:Html.HiddenFor(model => model.SelectedCulture) %>
        <div class="i_form_i">
            <%:Html.LocalizedLabelFor(model=>model.Title) %>
            <%=Model.Title%>
        </div>
        <div class="i_form_i">
         <%:Html.LocalizedLabelFor(model => model.Description, new{cssClass="for_some_field"})%>
            <%=Model.Description%>
        </div>       
    </div>
</div>