<%@ Assembly Name="Core.Languages" %>
<%@ Import Namespace="Core.Languages.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<LanguageViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent"><%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
</asp:Content>

<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
  <% using (Html.BeginForm()) {%>
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Title) %>
                </div>
			    <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Code) %>
                </div>
                <div class="i_form_i">
                    <%: Html.EditorFor(model => model.Culture) %>
                </div>
            </div>
        </div>
		<div class="i_buttons clrfix">
			<div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate(".Save"),new { @class="button"})%>
                <strong></strong>
            </div>
			<span><%:Html.RouteLink(Html.Translate(".Cancel"), new { controller = "Languages", action = "ShowAll" })%></span>
		</div>
    </div>
  <% } %>
</asp:Content>