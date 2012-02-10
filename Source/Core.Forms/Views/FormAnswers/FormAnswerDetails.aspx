<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.NHibernate.Models.FormWidgetAnswer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1><%:Html.Translate(".FormAnswerDetails") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <%foreach (var item in Model.AnswerValues) {%>
        <p>
            <%=Html.Encode(item.Field) %>:  <%=Html.Encode(item.Value)%>
        </p>
       <%}%>
</asp:Content>