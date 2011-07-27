<%@ Page Language="C#" MasterPageFile="Template.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Data" runat="server">

  <%: Html.FileBoxFor(model => model) %>

  <script type="text/javascript">

    $(function () {
      $("#FileName").makeAsyncUploader({
        upload_url: '<%= Url.Action(MVC.Upload.Image()) %>',
        flash_url: '<%= Links.Content.swfupload_swf %>',
        button_image_url: '<%= Links.Content.Images.btn2_png %>',
        button_width : 90,
	    button_height : 27,
        button_text: "<span class=\"swfupload-button\"><%: Html.Translate("Actions.Browse") %></span>",
        button_text_style: ".swfupload-button { font-family: Arial, Helvetica, sans-serif; font-size: 13px; font-weight: bold;color: #F9F9F9;}",
        button_cursor: SWFUpload.CURSOR.HAND, 
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        value: '<%= Url.AbsoluteUrl(Model as String) %>',
        <% if (ViewData.ModelMetadata.AdditionalValues.ContainsKey(UploadHelper.DefaultValueKey)) { %>
          default_value: "<%: VirtualPathUtility.ToAbsolute(ViewData.ModelMetadata.AdditionalValues[UploadHelper.DefaultValueKey] as String) %>",
        <% } %>
        post_params: {
          <% if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) { %>
            <%: UploadHelper.AuthenticationCookieKey %>: "<%= Request.Cookies[FormsAuthentication.FormsCookieName].Value %>",
          <% } %>
          <%: Html.UploadOptions() %>
        },
        <% if (ViewData.ModelMetadata.AdditionalValues.ContainsKey(UploadHelper.FileTypesKey)) { %>
          file_types: "<%: ViewData.ModelMetadata.AdditionalValues[UploadHelper.FileTypesKey] %>",
        <% } %>
        <% if (ViewData.ModelMetadata.AdditionalValues.ContainsKey(UploadHelper.FileTypesDescriptionKey)) { %>
          file_types_description: "<%: ViewData.ModelMetadata.AdditionalValues[UploadHelper.FileTypesDescriptionKey] %>",
        <% } %>
        messages: {
          success: "<%: Html.Translate("Messages.FileUploaded") %>",
          http_error: "<%: Html.Translate("Messages.UploadHttpError") %>",
          file_exceed_size_limit: "<%: Html.Translate("Messages.UploadFileExceedSizeLimit") %>",
          invalid_file_type: "<%: Html.Translate("Messages.UploadInvalidFileType") %>",
          zero_byte_file: "<%: Html.Translate("Messages.UploadZeroByteFile") %>",
          unknown_error: "<%: Html.Translate("Messages.UploadUnknownError") %>",
          confirm_delete: "<%: Html.Translate("Messages.ConfirmDeleteFile") %>"
        }
      });
    });

  </script>
</asp:Content>