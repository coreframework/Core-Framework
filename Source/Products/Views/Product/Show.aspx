<%@ Assembly Name="Products" %>
<%@ Assembly Name="Products.NHibernate" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Products.Models.ProductLocaleViewModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1>
        <%=Html.Translate(".ViewProduct") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="i_form clrfix">
        <div class="cols clrfix">
            <div class="fst_col colls_i">
                <div class="i_form_i">
                    <%:Html.DropDownListFor(model => model.SelectedCulture,
                                                             new SelectList(Model.Cultures, "Value", "Key",
                                                                            Model.SelectedCulture),
                                                             new {id = "SelectedCulture"})%>
                </div>
            </div>
        </div>
        <div id="localeForm">
            <% Html.RenderPartial("ShowForm", Model); %>
        </div>
        <div class="cols clrfix">
            <div class="fst_col colls_i">
                <div class="i_form_i">
                    <%:Html.LocalizedLabelFor(model=>model.Price) %><em>:</em>
                    <%=Model.Price %>
                </div>
                <%if (!String.IsNullOrEmpty(Model.FileName)){%>
                <div class="i_form_i">                   
                    <img class="preview" style="" src="<%=Model.FileName %>" alt="">
                </div>
                <%} %>
               
            </div>
        </div>
        <div class="i_buttons clrfix">
            <span>
                <%:Html.RouteLink(Html.Translate(".Cancel"), new { controller = "Product", action = "ShowAll" })%></span>
        </div>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.productId = <%=Model.ProductId %>;
                postData.culture = $(this).val();
                postData.isShow = true;
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "Product") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm div').replaceWith(response);
                }
                });
            });
        });
    </script>
</asp:Content>
