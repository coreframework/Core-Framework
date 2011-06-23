<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"  Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormElementViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm(FormsMVC.Forms.SaveElement(), FormMethod.Post))
         {%>    
              <div class="form_area">
                 
                    <%: Html.Hidden("formId", Model.FormId) %> 
                    <%: Html.HiddenFor(model=>model.Id) %> 

                    <p>
                        <%: Html.LabelFor(model => model.Title) %>
                        <%: Html.TextBoxFor(model => model.Title) %>
                        <%: Html.ValidationMessageFor(model => model.Title) %>
                    </p>
                    <p>
                        <%: Html.LabelFor(model => model.Type)%>
                        <%: Html.DropDownListFor("Type", Model.Type, new {}) %>
                        <%: Html.ValidationMessageFor(model => model.Type) %>
                    </p>
                    <p class="editor-field-hidden" id="el_values">
                        <%:Html.LabelFor(model => model.Values)%>
                        <%:Html.TextBoxFor(model => model.Values)%>
                        <%:Html.ValidationMessageFor(model => model.Values)%>
                    </p>
                 
                    <p class="editor-field-hidden" id="el_is_required">
                        <%:Html.CheckBoxFor(model => model.IsRequired)%>
                        <%:Html.LabelFor(model => model.IsRequired)%>
                    </p>
                    <p class="editor-field-hidden" id="el_validation">
                        <%: Html.LabelFor(model => model.RegexTemplate)%>
                        <%: Html.DropDownListFor("RegexTemplate", Model.RegexTemplate, new { })%>
                        <%: Html.ValidationMessageFor(model => model.RegexTemplate)%>
                    </p>
                      <p>
                        <%: Html.LabelFor(model=>model.MaxLength)%>
                        <%: Html.TextBoxFor(model => model.MaxLength)%>
                        <%: Html.ValidationMessageFor(model => model.MaxLength)%>
                    </p>

                   <%: Html.AntiForgeryToken()%>
             </div>
              <p class="buttons">
                     <%:Html.Submit("Save")%>
                     <%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowAll" })%>
               </p>
               <script type="text/javascript">
                   jQuery(function () {
                       function checkForm() {
                         curValue = $('#Type').val();
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
                       }

                       $('#Type').change(function () {
                           checkForm();
                       });
                       checkForm();
                   });
               </script>
          <% }%>
      </div>
</asp:Content>
