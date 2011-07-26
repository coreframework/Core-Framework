

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
	
	/** initialize the input highlight script */
	initInputHighlightScript();
});






