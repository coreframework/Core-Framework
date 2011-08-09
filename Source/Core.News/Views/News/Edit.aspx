<%@ Assembly Name="Core.News" %>
<%@ Import Namespace="Core.News.Models" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NewsArticleLocaleViewModel>"  MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"%>

<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="i_form clrfix">
        <div class="cols clrfix">
            <div class="fst_col colls_i" style="width: 60%;">
                <div class="i_form_i">
                    <%:Html.DropDownListFor(model => model.SelectedCulture,
                                                             new SelectList(Model.Cultures, "Value", "Key",
                                                                            Model.SelectedCulture),
                                                             new {id = "SelectedCulture"})%>
                </div>
            </div>
        </div>
        <div id="localeForm">
            <% Html.RenderPartial("EditForm", Model); %>
        </div>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.newsArticleId = <%=Model.NewsArticleId %>;
                postData.culture = $(this).val();
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "News") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm form').replaceWith(response);
                }
                });
            });
        });
    </script>
</asp:Content>
