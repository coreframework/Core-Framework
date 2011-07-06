/** set tabs */
$(function() {
	$('ul.i-tab li:first').addClass ('active');
	$('ul.tab-content li.tabs_cont:first').css ('display', 'block');
	$('ul.i-tab').delegate('li:not(.active)', 'click', function() {
		$(this).addClass('active').siblings().removeClass('active')
			.parents('.tabs').find('ul.tab-content li.tabs_cont').hide()
			.eq($(this).index()).fadeIn('slow');
	})
});

/** custom select with flags */
function showvalue(arg) {
	alert(arg);
	//arg.visible(false);
}

$(document).ready(function () {	

	/** set tabs block heigh */
	var maxLiHeight = 0;
	$("li.tabs_cont").each(function() { 
		if ($(this).height() > maxLiHeight) {
			maxLiHeight = $(this).height();
		}
	});
	$(".tab-content").height(maxLiHeight);
	
	
	/** enable left sidebar accordion */	
	$( "#accordion" ).accordion();

	
	/** enable datepickers */
	$( "#datepicker" ).datepicker({
		showOn: "button",
		buttonImage: "images/datepicker_bg.png",
		buttonImageOnly: true
	});
	$( "#datepicker2" ).datepicker({
		showOn: "button",
		buttonImage: "images/datepicker_bg.png",
		buttonImageOnly: true
	});
	
	
	/** set input width */
	$(".cols .i_form_i input[type=text]").each(function() { 
		$(this).width($(this).parent().width() - 14);
	});
	$(".cols .i_form_i .wth_flg input[type=text]").each(function() { 
		$(this).width($(this).parent().width() - 56);
	});
	
	/** initialize the input highlight script */
	initInputHighlightScript();
	
	
	/** custom select with flags */
	try {
		oHandler = $(".mydds").msDropDown().data("dd");
		//oHandler.visible(true);
		//alert($.msDropDown.version);
		//$.msDropDown.create("body select");
		$("#ver").html($.msDropDown.version);
	} catch(e) {
		alert("Error: "+e.message);
	}

	
	/** set content & sidebar areas height */
	var windowContentHeight = $(window).height() - 82 - 49;
	var sidebarHeight = $(".sidebar_i").height();
	var contentHeight = $("#content").height();
	var maxHeight = 0;

	if ((sidebarHeight < windowContentHeight) && (contentHeight < windowContentHeight)) {
		$(".sidebar_i").height(windowContentHeight);
		$("#content").height(windowContentHeight);
	}
	else {
		if (sidebarHeight < contentHeight) {
			$(".sidebar_i").height(contentHeight);
		}
		if (contentHeight < sidebarHeight) {
			$("#content").height(sidebarHeight);
		}
	}
	
});






