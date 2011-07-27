<%@ Assembly Name="Products" %>
<%@ Import Namespace="Products.Models" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Products.Models.CategoryViewModel>" %>

<%@ Import Namespace="System.Web.Mvc" %>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1> <%=Html.Translate(".NewCategory")%></h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <% using (Html.BeginForm())
       {%>
    <div class="i_form clrfix">
        <div class="cols clrfix">
            <div class="fst_col colls_i" style="width: 60%;">
                <div class="i_form_i">
                    <%:Html.EditorFor(model => model.Title)%>
                </div>
                <div class="i_form_i">
                    <%:Html.EditorFor(model => model.Description)%>
                </div>
                            
                <div class="i_form_i">
                    <%:Html.AntiForgeryToken()%>
                </div>
            </div>
        </div>
        <div class="i_buttons clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(Html.Translate(".Save"), new { @class = "button" })%>
                <strong></strong>
            </div>
            <span>
                <%:Html.RouteLink(Html.Translate(".Cancel"), new { controller = "Category", action = "ShowAll" })%></span>
        </div>
    </div>
    <% }%>
</asp:Content>
