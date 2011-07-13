(function ($) {

  // Adds error tooltip for field.
  $.fn.error_tool_tip = function () {
    $(this).qtip({
      style: {
        name: "red",
        border: { radius: 5 },
        tip: { corner: "leftMiddle" }
      },
      position: {
        corner: {
          target: "rightMiddle",
          tooltip: "leftMiddle"
        }
      },
      hide: {
        fixed: true,
        delay: 2000
      }
    });
  };

  // Displays success message in page messages region.
  $.display_success = function (message) {
    $.display_message(message, "success");
  };

  // Displays notice (warning) message in page messages region.
  $.display_notice = function (message) {
    $.display_message(message, "notice");
  };

  // Displays error message in page messages region.
  $.display_error = function (message) {
    $.display_message(message, "error");
  };

  // Displays message in page messages region.
  $.display_message = function (message, messageType) {
    $("#messages").append("<div class=\"" + messageType + "\">" + message + "</div>");
  };

  // Clears page messages.
  $.clear_messages = function (message, messageType) {
    $("#messages").empty();
  };

  // Display ajax preloader.
  $.show_preloader = function (message) {
    $.blockUI({
      message: "<h1>" + message + "</h1>",
      css: {
        border: "none",
        padding: "15px",
        backgroundColor: "#000",
        "-webkit-border-radius": "10px",
        "-moz-border-radius": "10px",
        opacity: .5,
        color: "#fff"
      }
    });
  };

  // Hides ajax preloader.
  $.hide_preloader = function (message) {
    $.unblockUI();
  };


  $.fix_ui_corners = function (radius) {
    $(".ui-corner-all").corner(radius + "px");
    $(".ui-corner-top").corner("top " + radius + "px");
    $(".ui-corner-bottom").corner("bottom " + radius + "px");
    $(".ui-corner-right").corner("right " + radius + "px");
    $(".ui-corner-left").corner("left " + radius + "px");
    $(".ui-corner-tl").corner("tl " + radius + "px");
    $(".ui-corner-tr").corner("tr " + radius + "px");
    $(".ui-corner-bl").corner("bl " + radius + "px");
    $(".ui-corner-br").corner("br " + radius + "px");
    $("ul.tabs li").corner("top " + radius + "px");
    $(".form").corner(radius + "px");
    $(".tab-content").uncorner().corner("tr bl br " + radius + "px");
    $(".button").corner(radius + "px");
    $(".people .vcard").corner(radius + "px");
  };

    // Adds error tooltip for field.
  $.fn.bind_select_all = function () {
    $(this).bind("click", function () {
      $("INPUT[type='checkbox']").attr('checked', $(this).is(':checked'));
    });
  };

  // Initializes forms on document ready event.
  $(function () {

    // Adds form submit trigger for link buttons marked as submit button.
    $("form").each(function () {
      var current_form = this;
      $(".submit-button", current_form).bind("click", function () {
        $(current_form).submit();
      });
    });

    // Adds tooltips for form validation messages.
    $(".tooltip img").error_tool_tip();

    // Cross browser rounded corners.
    //$.fix_ui_corners(5);
  });

  $(function () {
    $("a[rel=external]").attr({ target: "_blank" });
  });

})(jQuery)