﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.UserViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: Html.Translate(".Title") %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%: String.Format(Html.Translate(".Title")) %></h1>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <% using (Html.BeginForm(MVC.Admin.User.Create(), FormMethod.Post)) {%>
    <%: Html.HttpMethodOverride(HttpVerbs.Put) %>
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Email) %>
                    <%--<%: Html.ValidationMessageFor(model=>model.Email) %>--%>
                </div>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Nickname) %>
                </div>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Status) %>
                </div>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Password) %>
                </div>
            </div>
        </div>
		<div class="i_buttons clrfix">
			<div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate("Actions.Save"),new { @class="button"})%>
                <strong></strong>
            </div>
			<span><%: Html.ActionLink(Html.Translate("Actions.Cancel"), MVC.Admin.User.Index()) %></span>
		</div>
    </div>
    <% } %>
</asp:Content>