<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormElementViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
      <%if (Model.Id > 0) {%>
        <%:Html.Translate("Titles.EditForm", "Forms")%>
      <% } else {%>
        <%:Html.Translate("Titles.NewForm", "Forms")%>
      <% }%>
  </h1>
 <%Html.RenderAction(FormsMVC.Forms.FormTabs(Model.FormId??0, false, true, false));%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="i_form clrfix">
      <%if (Model.Id > 0){%>
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
             <script type='text/javascript'>
                $(document).ready(function () {
                    $('#SelectedCulture').change(function () {
                        var postData = {};
                        postData.formElementId = <%=Model.Id %>;
                        postData.culture = $(this).val();
                        $.ajax({
                        type: "POST",
                        url: "<%=Url.Action("ChangeFormElementLanguage", "Forms") %>",
                        data: postData,
                        success: function (response) {
                            $('#localeForm form').replaceWith(response);
                        }
                        });
                    });
                });
           </script>
        <%}%>
        <div id="localeForm">
            <% Html.RenderPartial("Forms/FormElementEditor", Model); %>
        </div>
   </div>
</asp:Content>