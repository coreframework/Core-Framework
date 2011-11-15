/// jQuery plugin to add support for SwfUpload
/// (c) 2008 Steven Sanderson

(function ($) {
    $.fn.makeAsyncUploader = function (options) {
        return this.each(function () {
            // Put in place a new container with a unique ID
            var id = $(this).attr("id");

            var container = $("<div class=\"image-uploader\">");
            var previewContainer;
            var image_url = options.value || options.default_value;
            var title;

            if (image_url) {
                previewContainer = $("<div class=\"file-preview\">");
                title = $('#Title').val().replace(/ /g, '_') + '.' + image_url.substring(image_url.lastIndexOf('.') + 1);

                previewContainer.append($("<div class=\"wrapper\"><img src=\"" + image_url + "\" alt=\"" + title + "\" class=\"preview\" /><div class=\"title\">" + title + "</div><div>"));
                previewContainer.append($("<input type=\"hidden\" name=\"" + id + "\"  value=\"" + image_url + "\"/>"));

                previewContainer.append($("<div class=\"action\"><input type=\"button\" name=\"" + id + "button\"  value=\"\" onclick=\"clearInputs('" + id + "','" + options.messages.confirm_delete + "')\" /></div>"));
            } else {
                previewContainer = $("<div class=\"file-preview\" style=\"display:none;\">");

                previewContainer.append($("<div class=\"wrapper\"><img class=\"preview\" /><div class=\"title\"></div><div>"));
                previewContainer.append($("<input type=\"hidden\" name=\"" + id + "\"  value=\"\"/>"));

                previewContainer.append($("<div class=\"action\"><input type=\"button\" name=\"" + id + "button\" value=\"\" onclick=\"clearInputs('" + id + "')\" /></div>"));
            }
            container.append(previewContainer);
            container.append($("<div class=\"clear\"></div>"));
            container.append($("<div class=\"upload-button\" id=\"" + id + "Upload\"></div>"));
            container.append($("<div class=\"progressbar\"></div>"));


            $(this).before(container).remove();
            $(".progressbar", container).progressbar({ value: 0 }).hide();

            // Instantiate the uploader SWF
            var swfu;
            var width = 90, height = 27, text_width = 70, text_height = 10;
            if (options) {
                width = options.button_width || width;
                height = options.button_height || height;
                if (options.button_text) {
                    var test_element = $(options.button_text).hide().appendTo("body");
                    text_width = test_element.width();
                    text_height = test_element.height();
                    test_element.remove();
                }
            }
            var defaults = {
                flash_url: "swfupload.swf",
                upload_url: "/Home/AsyncUpload",
                file_size_limit: "10 MB",
                file_types: "*.*",
                file_types_description: "All Files",
                debug: false,

                button_image_url: "blank-button.png",
                button_placeholder_id: id + "Upload",
                button_text: "<font face=\"Arial\" size=\"13pt\">Choose file</span>",
                button_text_left_padding: (width - text_width) / 2 -8, //
                button_text_top_padding: (height - text_height) / 2 -3, //

                messages: {
                    success: "File \"{0}\" has been uploaded successfully. Don't forget to save changes.",
                    http_error: "Server internal error. Please try again later.",
                    file_exceed_size_limit: "Selected file exceed size limit ({0}).",
                    invalid_file_type: "You have attempted to upload file of incorrect format.",
                    zero_byte_file: "You have attempted to upload empty file.",
                    unknown_error: "Unknown error was occured.",
                    confirm_delete: "Are you sure you want to delete file?"
                },

                // Called when the user chooses a new file from the file browser prompt (begins the upload)
                file_queued_handler: function (file) {
                    swfu.startUpload();
                },

                // Called when a file doesn't even begin to upload, because of some error
                file_queue_error_handler: function (file, error_code, message) {
                    switch (error_code) {
                        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                            message = this.settings.messages.zero_byte_file;
                            break;
                        case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                            message = String.format(this.settings.messages.file_exceed_size_limit, this.settings.file_size_limit);
                            break;
                        case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
                            message = this.settings.messages.invalid_file_type;
                            break;
                        default:
                            message = this.settings.messages.unknown_error;
                            break;
                    }

                    $.display_error(message);
                },

                // Called when an error occurs during upload
                upload_error_handler: function (file, error_code, message) {
                    if (error_code == SWFUpload.UPLOAD_ERROR.HTTP_ERROR) {
                        message = this.settings.messages.http_error;
                    }
                    else {
                        message = this.settings.messages.unknown_error;
                    }

                    $.display_error(message);
                },

                // Called when upload is beginning (switches controls to uploading state)
                upload_start_handler: function () {
                    swfu.setButtonDimensions(0, height);
                    $("input[name=" + id + "]", container).val("");
                    $(".progressbar", container).show().progressbar("value", 0);

                    if (options.disableDuringUpload) {
                        $(options.disableDuringUpload).attr("disabled", "disabled");
                    }

                  //  $.clear_messages();
                },

                // Called when upload completed successfully (puts success details into hidden fields)
                upload_success_handler: function (file, response) {
                    var response = eval("(" + response + ")");
                    $("input[name=" + id + "]", container).val(response.FileName);
                    $("div.title", container).empty();
                    $("div.title", container).append(response.FileTitle);

                    var thumbnailPath = response.Thumbnail ? response.Thumbnail : response.FileName;
                    $("img.preview", container).attr({ src: thumbnailPath, alt: response.FileTitle });

                    $("img.preview", container).error(function () {
                        $("img.preview", container).css('display', 'none');
                    });
                    $("div.file-preview", container).css('display', '');
                    $.display_notice(String.format(this.settings.messages.success, file.name));
                },

                // Called when upload is finished (either success or failure - reverts controls to non-uploading state)
                upload_complete_handler: function () {
                    var clearup = function () {
                        $(".progressbar", container).hide();
                        swfu.setButtonDimensions(width, height);
                    };
                    if ($("input[name=" + id + "]", container).val() != "") {
                        $(".progressbar", container).progressbar("value", 100);
                        window.setTimeout(function () { clearup(); }, 1000);
                    }
                    else {
                        clearup();
                    }

                    if (options.disableDuringUpload) {
                        $(options.disableDuringUpload).removeAttr("disabled");
                    }
                },

                // Called periodically during upload (moves the progess bar along)
                upload_progress_handler: function (file, bytes, total) {
                    var percent = 100 * bytes / total;
                    $(".progressbar", container).progressbar("value", percent);
                }
            };
            swfu = new SWFUpload($.extend(defaults, options || {}));
        });
    };
})(jQuery);

function clearInputs(id, message) {
    if (!confirm(message))
        return false;
    $(".image-uploader input[name=" + id + "]").val('');
    $(".image-uploader img.preview").attr({ src: '', alt: '' });
    $(".image-uploader div.title").empty();
    $(".image-uploader div.file-preview").css('display', 'none');
  //  $.clear_messages();
    $.display_notice("File has been deleted successfully");
}