﻿<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.Models.FormElementViewModel>" %>

<% using (Html.BeginForm("SaveElement", "Forms", new {formId = Model.FormId}))
    {%>    
	<div class="cols clrfix">
        <div class="fst_col colls_i">
                <%: Html.Hidden("formId", Model.FormId) %> 
                <%: Html.HiddenFor(model=>model.Id) %>
                <%:Html.HiddenFor(model => model.SelectedCulture) %>     
			<div class="i_form_i">
                    <%: Html.LocalizedLabelFor(model => model.Type)%>
                    <%: Html.DropDownListFor("Type", Model.Type, new {}) %>
                    <%: Html.ValidationMessageFor(model => model.Type) %>
            </div>
                <div class="i_form_i">
                    <%: Html.LocalizedLabelFor(model => model.Title)%>
                    <%: Html.TextBoxFor(model => model.Title) %>
                    <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
			<div class="i_form_i"  id="el_values">
                    <%:Html.LocalizedLabelFor(model => model.Values)%>
                    <%:Html.TextBoxFor(model => model.Values)%>
                    <%:Html.Translate(".SeparateWithComma")%>
                    <%:Html.ValidationMessageFor(model => model.Values)%>
            </div>
			<div class="i_form_i"  id="el_is_required">
                    <%:Html.CheckBoxFor(model => model.IsRequired)%>
                    <%:Html.LocalizedLabelFor(model => model.IsRequired)%>
            </div>
			<div class="i_form_i"  id="el_validation">
                    <%: Html.LocalizedLabelFor(model => model.RegexTemplate)%>
                    <%: Html.DropDownListFor("RegexTemplate", Model.RegexTemplate, new { })%>
                    <%: Html.ValidationMessageFor(model => model.RegexTemplate)%>
            </div>
			<div class="i_form_i"  id="el_maxLength">
                    <%: Html.LocalizedLabelFor(model => model.MaxLength)%>
                    <%: Html.TextBoxFor(model => model.MaxLength)%>
                    <%: Html.ValidationMessageFor(model => model.MaxLength)%>
            </div>
            <%: Html.AntiForgeryToken()%>
        </div>
    </div>
	<div class="i_buttons clrfix">
		<div class="btn1 clrfix">
            <em></em>
            <%: Html.Submit("Save",new { @class="button"})%>
            <strong></strong>
        </div>
		<span><%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowFormElements" },new {formId = Model.FormId})%></span>
	</div>
    <script type="text/javascript">
        jQuery(function () {
            function checkForm() {
                curValue = $('#Type').val();
                $('#el_validation').removeClass('editor-field-hidden');
                $('#el_values').removeClass('editor-field-hidden');
                $('#el_is_required').removeClass('editor-field-hidden');
                $('#el_maxLength').removeClass('editor-field-hidden');
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
                            <%if (!formType.IsMaxLengthEnabled) {%>
                            $('#el_maxLength').addClass('editor-field-hidden');
                            <% } %>
                        break;
                    <% } %>
                    default: break;
                }
            }

            $('#Type').change(function () {
                checkForm();
            });
            checkForm();
        });
    </script>
<% }%>
