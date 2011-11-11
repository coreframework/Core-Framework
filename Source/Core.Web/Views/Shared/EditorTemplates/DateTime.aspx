<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditorTemplates/Template.Master" Inherits="System.Web.Mvc.ViewPage<System.DateTime>" %>
<%@ Import Namespace="Core.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
 <%: Html.TextBox("", Model.ToString(ConstantsHelper.DateFormat), new { @class = "datepicker" })%>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('.datepicker').datepicker({ dateFormat: "<%=ConstantsHelper.JqueryDateFormat %>" });
        });
    </script>
</asp:Content>

