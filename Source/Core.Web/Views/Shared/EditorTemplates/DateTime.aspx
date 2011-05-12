<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditorTemplates/Template.Master" Inherits="System.Web.Mvc.ViewPage<System.DateTime>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
    <%: Html.TextBoxFor(model => model, new { @class = "datePicker" })%>
    <script type='text/javascript'>
        $(document).ready(function () {
            $(".datePicker").datepicker({
                showAnim: 'slideDown',
             /*   dateFormat: 'dd/mm/yyyy'*/
            });
        });
    </script>
</asp:Content>

