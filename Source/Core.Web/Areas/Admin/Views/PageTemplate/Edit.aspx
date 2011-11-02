﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.PageTemplateViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="PageTitleContent">  
<h1><%: String.Format(Html.Translate(".Title"), Model.Title) %></h1>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <% using (Html.BeginForm(MVC.Admin.PageTemplate.Update(), FormMethod.Post)) {%>

    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
              <%: Html.HiddenFor(model => model.Id) %>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Title) %>
                </div>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Url) %>
                </div>			    
            </div>
        </div>
		<div class="i_buttons clrfix">
			<div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate("Actions.Save"),new { @class="button"})%>
                <strong></strong>
            </div>
			<span><%: Html.ActionLink(Html.Translate("Actions.Cancel"), MVC.Admin.PageTemplate.Index())%></span>
		</div>
    </div>
  <% } %>
</asp:Content>