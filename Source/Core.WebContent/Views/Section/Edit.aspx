<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.WebContent.Models.SectionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("EditSection", "WebContent.Views.Section")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
      <%:Html.Translate("EditSection", "WebContent.Views.Section")%>
  </h1>
  <div class="tabs clrfix">
	<ul class="i-tab clrfix">
        <li class="active">
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Details", "WebContent.Views.Section"), "Edit") %>
            </span>
            <strong></strong>
        </li>
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Permissions", "WebContent.Views.Section"), "ShowPermissions") %>
            </span>
            <strong></strong>
        </li>
	</ul>
  </div>
  <div class="tabs_b"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="i_form clrfix">
        <div class="cols clrfix">
            <div class="fst_col colls_i">
                <div class="i_form_i">
                    <label><%:Html.Translate("Language", "WebContent.Views.Section")%></label>
                    <%:Html.DropDownListFor(model => model.SelectedCulture,
                                                             new SelectList(Model.Cultures, "Value", "Key",
                                                                            Model.SelectedCulture),
                                                             new {id = "SelectedCulture"})%>
                </div>
            </div>
        </div>
        <% using (Html.BeginForm(WebContentMVC.Section.Save(), FormMethod.Post)) {%> 
            <div id="localeForm">
                <%Html.RenderPartial("SectionDetails", Model);%>
            </div>
         <% }%>
    </div>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                var postData = {};
                postData.sectionId = <%=Model.Id %>;
                postData.culture = $(this).val();
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "Section") %>",
                data: postData,
                success: function (response) {
                    $('#localeForm div:first').replaceWith(response);
                }
                });
            });
        });
    </script>
</asp:Content>