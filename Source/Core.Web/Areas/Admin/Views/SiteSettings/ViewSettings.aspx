<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Web.NHibernate.Models.SiteSettings>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%:Html.Translate(".SiteSettings") %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%:Html.Translate(".SiteSettings") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Edit", "SiteSettings"))
   {%>
     <%:Html.ValidationSummary(true)%>
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
                 <div class="i_form_i">
                    <%:Html.HiddenFor(model => model.Id)%>
                    <%:Html.CheckBoxFor(model => model.ShowMainMenu)%>
                    <%:Html.LocalizedLabelFor(model => model.ShowMainMenu)%>
                </div>
            </div>
        </div>
        <div class="i_buttons clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate("Actions.Save"), new { @class = "button" })%>
                <strong></strong>
            </div>
        </div>
    </div>
    <%}%>
</asp:Content>
