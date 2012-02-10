<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
      <%:Html.Translate("Titles.EditForm","Forms") %>
  </h1>
 <%Html.RenderAction(FormsMVC.Forms.FormTabs(Model.Id, true, false, false));%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <% Html.RenderPartial("Forms/EditForm", Model); %>
        </div>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.formId = <%=Model.Id %>;
                postData.culture = $(this).val();
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "Forms") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm form').replaceWith(response);
                }
                });
            });
        });
    </script>
</asp:Content>