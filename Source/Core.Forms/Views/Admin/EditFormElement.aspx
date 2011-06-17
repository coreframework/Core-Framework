<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"  Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormElementViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm(FormsMVC.Forms.Save(), FormMethod.Post))
         {%>    
              <div class="form_area">
                 
                    <%: Html.HiddenFor(model => model.Id) %> 

                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Title) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Title) %>
                        <%: Html.ValidationMessageFor(model => model.Title) %>
                    </div>

                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Type)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.DropDownListFor("Type", Model.Type, new {}) %>
                        <%: Html.ValidationMessageFor(model => model.Type) %>
                    </div>
                    <div class="editor-field editor-field-hidden" id="el_values">
                        <%:Html.LabelFor(model => model.Values)%>
                        <%:Html.TextBoxFor(model => model.Values)%>
                        <%:Html.ValidationMessageFor(model => model.Values)%>
                    </div>
                 
                    <div class="editor-field editor-field-hidden" id="el_is_required">
                        <%:Html.CheckBoxFor(model => model.IsRequired)%>
                        <%:Html.LabelFor(model => model.IsRequired)%>
                    </div>
                    <div class="editor-field editor-field-hidden" id="el_validation">
                        <%: Html.DropDownListFor("ValidationRegexTemplate", Model.ValidationRegexTemplate, new { })%>
                        <%: Html.ValidationMessageFor(model => model.ValidationRegexTemplate) %>
                    </div>

                   <%: Html.AntiForgeryToken()%>
             </div>
              <p class="buttons">
                     <%:Html.Submit("Save")%>
                     <%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowAll" })%>
               </p>
               <script type="text/javascript">
                   jQuery(function () {
                       $('#Type').change(function(){
                            curValue = $(this).val();
                            $('#el_validation').removeClass('editor-field-hidden');
                            $('#el_values').removeClass('editor-field-hidden');
                            $('#el_is_required').removeClass('editor-field-hidden');
                            switch(curValue)
                            {
                               <% foreach (var formType in Model.Types) {%>
                                    case "<%=formType.Type%>":
                                        <%if (!formType.IsRequiredEnabled) {%>
                                        $('#el_is_required').addClass('editor-field-hidden');
                                        <% } %>
                                        <%if (!formType.IsValidationEnabled) {%>
                                        $('#el_validation').addClass('editor-field-hidden');
                                        <% } %>
                                        <%if (!formType.IsValuesEnabled) {%>
                                        $('#el_values').addClass('editor-field-hidden');
                                        <% } %>
                                    break;
                               <% } %>
                               default: alert('default');
                            }
                       });
                   });
               </script>
          <% }%>
      </div>
</asp:Content>
