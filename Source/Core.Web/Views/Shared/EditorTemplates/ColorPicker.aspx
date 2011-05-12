<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditorTemplates/Template.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">
  <%= Html.TextBoxFor(model => model, new { Class =  "colorPicker" })%>
  <script type="text/javascript">
      $(function () {
          $(".colorPicker").ColorPicker({
              color: '#0000ff',
              onShow: function (colpkr) {
                  $(colpkr).fadeIn(100);
                  return false;
              },
              onHide: function (colpkr) {
                  $(colpkr).fadeOut(100);
                  return false;
              }
          });
      });
  </script>
</asp:Content>