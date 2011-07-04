<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.NHibernate.Models.FormWidgetAnswer>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="outset">
       <%foreach (var item in Model.AnswerValues) {%>
        <p>
            <%=Html.Encode(item.Field) %>:  <%=Html.Encode(item.Value)%>
        </p>
       <%}%>
   </div>
</asp:Content>