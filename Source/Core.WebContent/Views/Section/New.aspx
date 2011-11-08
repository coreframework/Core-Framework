﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"Inherits="System.Web.Mvc.ViewPage<Core.WebContent.Models.SectionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%if (Model.Id > 0) {%>
        <%:Html.Translate(".EditSection") %>
    <%} else {%>
         <%:Html.Translate(".NewSection") %>
    <% }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
    <%if (Model.Id > 0) {%>
        <%:Html.Translate(".EditSection") %>
    <%} else {%>
         <%:Html.Translate(".NewSection") %>
    <% }%>
  </h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.ValidationSummary(true) %>
  <% using (Html.BeginForm())
    {%>    
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
                <%:Html.HiddenFor(model => model.Id)%>
                <h3><%:Html.Translate(".Common")%>:</h3>     
			    <div class="i_form_i">
                    <%:Html.LocalizedLabelFor(model=>model.Title) %>
                    <%:Html.TextBoxFor(model => model.Title)%>
                    <%:Html.ValidationMessageFor(model => model.Title) %>
                </div>
                <div class="i_form_i">
                    <%:Html.LocalizedLabelFor(model=>model.Description) %>
                    <%:Html.TextAreaFor(model => model.Description)%>
                    <%:Html.ValidationMessageFor(model => model.Description)%>
                </div>
                <h3><%:Html.Translate(".Settings")%>:</h3>  
                <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowTitle)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowTitle)%>
                </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.TitleLinkable)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.TitleLinkable)%>
                </div>
                  <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowSummaryText)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowSummaryText)%>
                </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowSection)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowSection)%>
                </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowCategory)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowCategory)%>
                </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowAuthor)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowAuthor)%>
                </div>
                <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowCreatedDate)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowCreatedDate)%>
                </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowModifiedDate)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowModifiedDate)%>
                 </div>
                  <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowPdfIcon)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowPdfIcon)%>
                 </div>
                  <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowPrintIcon)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowPrintIcon)%>
                 </div>
                 <div class="i_form_i">
                    <%:Html.CheckBoxFor(model => model.SectionSettings.ShowEmailIcon)%>
                    <%:Html.LocalizedLabelFor(model => model.SectionSettings.ShowEmailIcon)%>
                 </div>
                  <div class="i_form_i">
                    <%:Html.LocalizedLabelFor(model=>model.SectionSettings.AlternativeReadMoreText) %>
                    <%:Html.TextBoxFor(model => model.SectionSettings.AlternativeReadMoreText)%>
                    <%:Html.ValidationMessageFor(model => model.SectionSettings.AlternativeReadMoreText)%>
                </div>
			    <div class="i_form_i">
                <%:Html.AntiForgeryToken()%>
                </div>
            </div>
        </div>
	    <div class="i_buttons clrfix">
        <%if (Model.AllowManage){%>
		    <div class="btn1 clrfix">
                <em></em>
                <%:Html.Submit(Html.Translate("Actions.Save"), new {@class = "button"})%>
                <strong></strong>
            </div>
            <%}%>
		    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Section", action = "Show" })%></span>
	    </div>
    </div>
<% }%>
</asp:Content>