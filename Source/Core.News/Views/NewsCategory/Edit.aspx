<%@ Assembly Name="Core.News" %>
<%@ Import Namespace="Core.News.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.News.Models.CategoryLocaleViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>


<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%=Html.Translate(".EditCategory") %></h1>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>  
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
            <% Html.RenderPartial("EditForm", Model); %>
        </div>        
    </div>
   <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.categoryId = <%=Model.CategoryId %>;
                postData.culture = $(this).val();
                postData.isShow = false;
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "NewsCategory") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm form').replaceWith(response);
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