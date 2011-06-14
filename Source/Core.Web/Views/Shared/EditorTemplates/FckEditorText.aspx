<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditorTemplates/Template.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
   <p>
       <%= Html.TextAreaFor(model => model,new { id ="FckArea" }) %>
  </p>
   <script type="text/javascript">
       window.onload = function () {
           var sBasePath = '<%= ResolveUrl("~/Scripts/fck/") %>';
           var oFCKeditor = new FCKeditor('FckArea');
           oFCKeditor.Config.Enabled = true;


           oFCKeditor.Height = '500';
           oFCKeditor.BasePath = sBasePath;
           oFCKeditor.ReplaceTextarea();

       }
    </script>
</asp:Content>

