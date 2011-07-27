<%@ Assembly Name="Products" %>
<%@ Import Namespace="Products.Models" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Products.Models.ProductLocaleViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1>
        <%=Html.Translate(".EditProduct") %></h1>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="i_form clrfix">
        <% using (Html.BeginForm("Edit", "Product", new { id = Model.ProductId }))
           {%>
        <div class="cols clrfix">
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.Price)%>
            </div>
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.FileName)%>
            </div>
            <div class="i_form_i">
                <%:Html.DropDownListFor(model => model.SelectedCulture,
                                                             new SelectList(Model.Cultures, "Value", "Key",
                                                                            Model.SelectedCulture),
                                                             new {id = "SelectedCulture"})%>
            </div>
        </div>
        <div id="localeForm">
            <% Html.RenderPartial("EditForm", Model); %>
        </div>
        <div class="i_buttons clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate(".Save"),new { @class="button"})%>
                <strong></strong>
            </div>
            <span>
                <%:Html.RouteLink(Html.Translate(".Cancel"), new { controller = "Product", action = "ShowAll" })%></span>
        </div>
        <% }%>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.productId = <%=Model.ProductId %>;
                postData.culture = $(this).val();
                postData.isShow = false;
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "Product") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm div').replaceWith(response);
                     var sBasePath = '<%= ResolveUrl("~/Scripts/fck/") %>';
                       var oFCKeditor = new FCKeditor('FckArea');
                       oFCKeditor.Config.Enabled = true;


                       oFCKeditor.Height = '500';
                       oFCKeditor.BasePath = sBasePath;
                       oFCKeditor.ReplaceTextarea();
                }
                });
            });
        });
    </script>
</asp:Content>
