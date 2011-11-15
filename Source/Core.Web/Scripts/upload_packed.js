var SWFUpload;if(SWFUpload==undefined){SWFUpload=function(a){this.initSWFUpload(a)}}SWFUpload.prototype.initSWFUpload=function(a){try{this.customSettings={};this.settings=a;this.eventQueue=[];this.movieName="SWFUpload_"+SWFUpload.movieCount++;this.movieElement=null;SWFUpload.instances[this.movieName]=this;this.initSettings();this.loadFlash();this.displayDebugInfo()}catch(b){delete SWFUpload.instances[this.movieName];throw b}};SWFUpload.instances={};SWFUpload.movieCount=0;SWFUpload.version="2.2.0 Beta 2";SWFUpload.QUEUE_ERROR={QUEUE_LIMIT_EXCEEDED:-100,FILE_EXCEEDS_SIZE_LIMIT:-110,ZERO_BYTE_FILE:-120,INVALID_FILETYPE:-130};SWFUpload.UPLOAD_ERROR={HTTP_ERROR:-200,MISSING_UPLOAD_URL:-210,IO_ERROR:-220,SECURITY_ERROR:-230,UPLOAD_LIMIT_EXCEEDED:-240,UPLOAD_FAILED:-250,SPECIFIED_FILE_ID_NOT_FOUND:-260,FILE_VALIDATION_FAILED:-270,FILE_CANCELLED:-280,UPLOAD_STOPPED:-290};SWFUpload.FILE_STATUS={QUEUED:-1,IN_PROGRESS:-2,ERROR:-3,COMPLETE:-4,CANCELLED:-5};SWFUpload.BUTTON_ACTION={SELECT_FILE:-100,SELECT_FILES:-110,START_UPLOAD:-120};SWFUpload.CURSOR={ARROW:-1,HAND:-2};SWFUpload.WINDOW_MODE={WINDOW:"window",TRANSPARENT:"transparent",OPAQUE:"opaque"};SWFUpload.prototype.initSettings=function(){this.ensureDefault=function(a,b){this.settings[a]=(this.settings[a]==undefined)?b:this.settings[a]};this.ensureDefault("upload_url","");this.ensureDefault("file_post_name","Filedata");this.ensureDefault("post_params",{});this.ensureDefault("use_query_string",false);this.ensureDefault("requeue_on_error",false);this.ensureDefault("http_success",[]);this.ensureDefault("file_types","*.*");this.ensureDefault("file_types_description","All Files");this.ensureDefault("file_size_limit",0);this.ensureDefault("file_upload_limit",0);this.ensureDefault("file_queue_limit",0);this.ensureDefault("flash_url","swfupload.swf");this.ensureDefault("prevent_swf_caching",true);this.ensureDefault("button_image_url","");this.ensureDefault("button_width",1);this.ensureDefault("button_height",1);this.ensureDefault("button_text","");this.ensureDefault("button_text_style","color: #000000; font-size: 16pt;");this.ensureDefault("button_text_top_padding",0);this.ensureDefault("button_text_left_padding",0);this.ensureDefault("button_action",SWFUpload.BUTTON_ACTION.SELECT_FILES);this.ensureDefault("button_disabled",false);this.ensureDefault("button_placeholder_id",null);this.ensureDefault("button_cursor",SWFUpload.CURSOR.ARROW);this.ensureDefault("button_window_mode",SWFUpload.WINDOW_MODE.WINDOW);this.ensureDefault("debug",false);this.settings.debug_enabled=this.settings.debug;this.settings.return_upload_start_handler=this.returnUploadStart;this.ensureDefault("swfupload_loaded_handler",null);this.ensureDefault("file_dialog_start_handler",null);this.ensureDefault("file_queued_handler",null);this.ensureDefault("file_queue_error_handler",null);this.ensureDefault("file_dialog_complete_handler",null);this.ensureDefault("upload_start_handler",null);this.ensureDefault("upload_progress_handler",null);this.ensureDefault("upload_error_handler",null);this.ensureDefault("upload_success_handler",null);this.ensureDefault("upload_complete_handler",null);this.ensureDefault("debug_handler",this.debugMessage);this.ensureDefault("custom_settings",{});this.customSettings=this.settings.custom_settings;if(this.settings.prevent_swf_caching){this.settings.flash_url=this.settings.flash_url+"?swfuploadrnd="+Math.floor(Math.random()*999999999)}delete this.ensureDefault};SWFUpload.prototype.loadFlash=function(){if(this.settings.button_placeholder_id!==""){this.replaceWithFlash()}else{this.appendFlash()}};SWFUpload.prototype.appendFlash=function(){var b,a;if(document.getElementById(this.movieName)!==null){throw"ID "+this.movieName+" is already in use. The Flash Object could not be added"}b=document.getElementsByTagName("body")[0];if(b==undefined){throw"Could not find the 'body' element."}a=document.createElement("div");a.style.width="1px";a.style.height="1px";a.style.overflow="hidden";b.appendChild(a);a.innerHTML=this.getFlashHTML()};SWFUpload.prototype.replaceWithFlash=function(){var b,a;if(document.getElementById(this.movieName)!==null){throw"ID "+this.movieName+" is already in use. The Flash Object could not be added"}b=document.getElementById(this.settings.button_placeholder_id);if(b==undefined){throw"Could not find the placeholder element."}a=document.createElement("div");a.innerHTML=this.getFlashHTML();b.parentNode.replaceChild(a.firstChild,b)};SWFUpload.prototype.getFlashHTML=function(){return['<object id="',this.movieName,'" type="application/x-shockwave-flash" data="',this.settings.flash_url,'" width="',this.settings.button_width,'" height="',this.settings.button_height,'" class="swfupload">','<param name="wmode" value="',this.settings.button_window_mode,'" />','<param name="movie" value="',this.settings.flash_url,'" />','<param name="quality" value="high" />','<param name="menu" value="false" />','<param name="allowScriptAccess" value="always" />','<param name="flashvars" value="'+this.getFlashVars()+'" />',"</object>"].join("")};SWFUpload.prototype.getFlashVars=function(){var a=this.buildParamString();var b=this.settings.http_success.join(",");return["movieName=",encodeURIComponent(this.movieName),"&amp;uploadURL=",encodeURIComponent(this.settings.upload_url),"&amp;useQueryString=",encodeURIComponent(this.settings.use_query_string),"&amp;requeueOnError=",encodeURIComponent(this.settings.requeue_on_error),"&amp;httpSuccess=",encodeURIComponent(b),"&amp;params=",encodeURIComponent(a),"&amp;filePostName=",encodeURIComponent(this.settings.file_post_name),"&amp;fileTypes=",encodeURIComponent(this.settings.file_types),"&amp;fileTypesDescription=",encodeURIComponent(this.settings.file_types_description),"&amp;fileSizeLimit=",encodeURIComponent(this.settings.file_size_limit),"&amp;fileUploadLimit=",encodeURIComponent(this.settings.file_upload_limit),"&amp;fileQueueLimit=",encodeURIComponent(this.settings.file_queue_limit),"&amp;debugEnabled=",encodeURIComponent(this.settings.debug_enabled),"&amp;buttonImageURL=",encodeURIComponent(this.settings.button_image_url),"&amp;buttonWidth=",encodeURIComponent(this.settings.button_width),"&amp;buttonHeight=",encodeURIComponent(this.settings.button_height),"&amp;buttonText=",encodeURIComponent(this.settings.button_text),"&amp;buttonTextTopPadding=",encodeURIComponent(this.settings.button_text_top_padding),"&amp;buttonTextLeftPadding=",encodeURIComponent(this.settings.button_text_left_padding),"&amp;buttonTextStyle=",encodeURIComponent(this.settings.button_text_style),"&amp;buttonAction=",encodeURIComponent(this.settings.button_action),"&amp;buttonDisabled=",encodeURIComponent(this.settings.button_disabled),"&amp;buttonCursor=",encodeURIComponent(this.settings.button_cursor)].join("")};SWFUpload.prototype.getMovieElement=function(){if(this.movieElement==undefined){this.movieElement=document.getElementById(this.movieName)}if(this.movieElement===null){throw"Could not find Flash element"}return this.movieElement};SWFUpload.prototype.buildParamString=function(){var b=this.settings.post_params;var a=[];if(typeof(b)==="object"){for(var c in b){if(b.hasOwnProperty(c)){a.push(encodeURIComponent(c.toString())+"="+encodeURIComponent(b[c].toString()))}}}return a.join("&amp;")};SWFUpload.prototype.destroy=function(){try{this.stopUpload();var a=null;try{a=this.getMovieElement()}catch(d){}if(a!=undefined&&a.parentNode!=undefined&&typeof a.parentNode.removeChild==="function"){var c=a.parentNode;if(c!=undefined){c.removeChild(a);if(c.parentNode!=undefined&&typeof c.parentNode.removeChild==="function"){c.parentNode.removeChild(c)}}}SWFUpload.instances[this.movieName]=null;delete SWFUpload.instances[this.movieName];delete this.movieElement;delete this.settings;delete this.customSettings;delete this.eventQueue;delete this.movieName;delete window[this.movieName];return true}catch(b){return false}};SWFUpload.prototype.displayDebugInfo=function(){this.debug(["---SWFUpload Instance Info---\n","Version: ",SWFUpload.version,"\n","Movie Name: ",this.movieName,"\n","Settings:\n","\t","upload_url:               ",this.settings.upload_url,"\n","\t","flash_url:                ",this.settings.flash_url,"\n","\t","use_query_string:         ",this.settings.use_query_string.toString(),"\n","\t","requeue_on_error:         ",this.settings.requeue_on_error.toString(),"\n","\t","http_success:             ",this.settings.http_success.join(", "),"\n","\t","file_post_name:           ",this.settings.file_post_name,"\n","\t","post_params:              ",this.settings.post_params.toString(),"\n","\t","file_types:               ",this.settings.file_types,"\n","\t","file_types_description:   ",this.settings.file_types_description,"\n","\t","file_size_limit:          ",this.settings.file_size_limit,"\n","\t","file_upload_limit:        ",this.settings.file_upload_limit,"\n","\t","file_queue_limit:         ",this.settings.file_queue_limit,"\n","\t","debug:                    ",this.settings.debug.toString(),"\n","\t","prevent_swf_caching:      ",this.settings.prevent_swf_caching.toString(),"\n","\t","button_placeholder_id:    ",this.settings.button_placeholder_id.toString(),"\n","\t","button_image_url:         ",this.settings.button_image_url.toString(),"\n","\t","button_width:             ",this.settings.button_width.toString(),"\n","\t","button_height:            ",this.settings.button_height.toString(),"\n","\t","button_text:              ",this.settings.button_text.toString(),"\n","\t","button_text_style:        ",this.settings.button_text_style.toString(),"\n","\t","button_text_top_padding:  ",this.settings.button_text_top_padding.toString(),"\n","\t","button_text_left_padding: ",this.settings.button_text_left_padding.toString(),"\n","\t","button_action:            ",this.settings.button_action.toString(),"\n","\t","button_disabled:          ",this.settings.button_disabled.toString(),"\n","\t","custom_settings:          ",this.settings.custom_settings.toString(),"\n","Event Handlers:\n","\t","swfupload_loaded_handler assigned:  ",(typeof this.settings.swfupload_loaded_handler==="function").toString(),"\n","\t","file_dialog_start_handler assigned: ",(typeof this.settings.file_dialog_start_handler==="function").toString(),"\n","\t","file_queued_handler assigned:       ",(typeof this.settings.file_queued_handler==="function").toString(),"\n","\t","file_queue_error_handler assigned:  ",(typeof this.settings.file_queue_error_handler==="function").toString(),"\n","\t","upload_start_handler assigned:      ",(typeof this.settings.upload_start_handler==="function").toString(),"\n","\t","upload_progress_handler assigned:   ",(typeof this.settings.upload_progress_handler==="function").toString(),"\n","\t","upload_error_handler assigned:      ",(typeof this.settings.upload_error_handler==="function").toString(),"\n","\t","upload_success_handler assigned:    ",(typeof this.settings.upload_success_handler==="function").toString(),"\n","\t","upload_complete_handler assigned:   ",(typeof this.settings.upload_complete_handler==="function").toString(),"\n","\t","debug_handler assigned:             ",(typeof this.settings.debug_handler==="function").toString(),"\n"].join(""))};SWFUpload.prototype.addSetting=function(c,a,b){if(a==undefined){return(this.settings[c]=b)}else{return(this.settings[c]=a)}};SWFUpload.prototype.getSetting=function(a){if(this.settings[a]!=undefined){return this.settings[a]}return""};SWFUpload.prototype.callFlash=function(b,c){c=c||[];var a=this.getMovieElement();var d;if(typeof a[b]==="function"){if(c.length===0){d=a[b]()}else{if(c.length===1){d=a[b](c[0])}else{if(c.length===2){d=a[b](c[0],c[1])}else{if(c.length===3){d=a[b](c[0],c[1],c[2])}else{throw"Too many arguments"}}}}if(d!=undefined&&typeof d.post==="object"){d=this.unescapeFilePostParams(d)}return d}else{throw"Invalid function name: "+b}};SWFUpload.prototype.selectFile=function(){this.callFlash("SelectFile")};SWFUpload.prototype.selectFiles=function(){this.callFlash("SelectFiles")};SWFUpload.prototype.startUpload=function(a){this.callFlash("StartUpload",[a])};SWFUpload.prototype.cancelUpload=function(b,a){if(a!==false){a=true}this.callFlash("CancelUpload",[b,a])};SWFUpload.prototype.stopUpload=function(){this.callFlash("StopUpload")};SWFUpload.prototype.getStats=function(){return this.callFlash("GetStats")};SWFUpload.prototype.setStats=function(a){this.callFlash("SetStats",[a])};SWFUpload.prototype.getFile=function(a){if(typeof(a)==="number"){return this.callFlash("GetFileByIndex",[a])}else{return this.callFlash("GetFile",[a])}};SWFUpload.prototype.addFileParam=function(b,c,a){return this.callFlash("AddFileParam",[b,c,a])};SWFUpload.prototype.removeFileParam=function(b,a){this.callFlash("RemoveFileParam",[b,a])};SWFUpload.prototype.setUploadURL=function(a){this.settings.upload_url=a.toString();this.callFlash("SetUploadURL",[a])};SWFUpload.prototype.setPostParams=function(a){this.settings.post_params=a;this.callFlash("SetPostParams",[a])};SWFUpload.prototype.addPostParam=function(a,b){this.settings.post_params[a]=b;this.callFlash("SetPostParams",[this.settings.post_params])};SWFUpload.prototype.removePostParam=function(a){delete this.settings.post_params[a];this.callFlash("SetPostParams",[this.settings.post_params])};SWFUpload.prototype.setFileTypes=function(b,a){this.settings.file_types=b;this.settings.file_types_description=a;this.callFlash("SetFileTypes",[b,a])};SWFUpload.prototype.setFileSizeLimit=function(a){this.settings.file_size_limit=a;this.callFlash("SetFileSizeLimit",[a])};SWFUpload.prototype.setFileUploadLimit=function(a){this.settings.file_upload_limit=a;this.callFlash("SetFileUploadLimit",[a])};SWFUpload.prototype.setFileQueueLimit=function(a){this.settings.file_queue_limit=a;this.callFlash("SetFileQueueLimit",[a])};SWFUpload.prototype.setFilePostName=function(a){this.settings.file_post_name=a;this.callFlash("SetFilePostName",[a])};SWFUpload.prototype.setUseQueryString=function(a){this.settings.use_query_string=a;this.callFlash("SetUseQueryString",[a])};SWFUpload.prototype.setRequeueOnError=function(a){this.settings.requeue_on_error=a;this.callFlash("SetRequeueOnError",[a])};SWFUpload.prototype.setHTTPSuccess=function(a){if(typeof a==="string"){a=a.replace(" ","").split(",")}this.settings.http_success=a;this.callFlash("SetHTTPSuccess",[a])};SWFUpload.prototype.setDebugEnabled=function(a){this.settings.debug_enabled=a;this.callFlash("SetDebugEnabled",[a])};SWFUpload.prototype.setButtonImageURL=function(a){if(a==undefined){a=""}this.settings.button_image_url=a;this.callFlash("SetButtonImageURL",[a])};SWFUpload.prototype.setButtonDimensions=function(b,a){this.settings.button_width=b;this.settings.button_height=a;var c=this.getMovieElement();if(c!=undefined){c.style.width=b+"px";c.style.height=a+"px"}this.callFlash("SetButtonDimensions",[b,a])};SWFUpload.prototype.setButtonText=function(a){this.settings.button_text=a;this.callFlash("SetButtonText",[a])};SWFUpload.prototype.setButtonTextPadding=function(a,b){this.settings.button_text_top_padding=b;this.settings.button_text_left_padding=a;this.callFlash("SetButtonTextPadding",[a,b])};SWFUpload.prototype.setButtonTextStyle=function(a){this.settings.button_text_style=a;this.callFlash("SetButtonTextStyle",[a])};SWFUpload.prototype.setButtonDisabled=function(a){this.settings.button_disabled=a;this.callFlash("SetButtonDisabled",[a])};SWFUpload.prototype.setButtonAction=function(a){this.settings.button_action=a;this.callFlash("SetButtonAction",[a])};SWFUpload.prototype.setButtonCursor=function(a){this.settings.button_cursor=a;this.callFlash("SetButtonCursor",[a])};SWFUpload.prototype.queueEvent=function(a,b){if(b==undefined){b=[]}else{if(!(b instanceof Array)){b=[b]}}var c=this;if(typeof this.settings[a]==="function"){this.eventQueue.push(function(){this.settings[a].apply(this,b)});setTimeout(function(){c.executeNextEvent()},0)}else{if(this.settings[a]!==null){throw"Event handler "+a+" is unknown or is not a function"}}};SWFUpload.prototype.executeNextEvent=function(){var a=this.eventQueue?this.eventQueue.shift():null;if(typeof(a)==="function"){a.apply(this)}};SWFUpload.prototype.unescapeFilePostParams=function(a){var e=/[$]([0-9a-f]{4})/i;var c={};var f;if(a!=undefined){for(var b in a.post){if(a.post.hasOwnProperty(b)){f=b;var d;while((d=e.exec(f))!==null){f=f.replace(d[0],String.fromCharCode(parseInt("0x"+d[1],16)))}c[f]=a.post[b]}}a.post=c}return a};SWFUpload.prototype.flashReady=function(){var a=this.getMovieElement();if(typeof a.StartUpload!=="function"){throw"ExternalInterface methods failed to initialize."}if(window[this.movieName]==undefined){window[this.movieName]=a}this.queueEvent("swfupload_loaded_handler")};SWFUpload.prototype.fileDialogStart=function(){this.queueEvent("file_dialog_start_handler")};SWFUpload.prototype.fileQueued=function(a){a=this.unescapeFilePostParams(a);this.queueEvent("file_queued_handler",a)};SWFUpload.prototype.fileQueueError=function(b,a,c){b=this.unescapeFilePostParams(b);this.queueEvent("file_queue_error_handler",[b,a,c])};SWFUpload.prototype.fileDialogComplete=function(a,b){this.queueEvent("file_dialog_complete_handler",[a,b])};SWFUpload.prototype.uploadStart=function(a){a=this.unescapeFilePostParams(a);this.queueEvent("return_upload_start_handler",a)};SWFUpload.prototype.returnUploadStart=function(a){var b;if(typeof this.settings.upload_start_handler==="function"){a=this.unescapeFilePostParams(a);b=this.settings.upload_start_handler.call(this,a)}else{if(this.settings.upload_start_handler!=undefined){throw"upload_start_handler must be a function"}}if(b===undefined){b=true}b=!!b;this.callFlash("ReturnUploadStart",[b])};SWFUpload.prototype.uploadProgress=function(a,b,c){a=this.unescapeFilePostParams(a);this.queueEvent("upload_progress_handler",[a,b,c])};SWFUpload.prototype.uploadError=function(b,a,c){b=this.unescapeFilePostParams(b);this.queueEvent("upload_error_handler",[b,a,c])};SWFUpload.prototype.uploadSuccess=function(a,b){a=this.unescapeFilePostParams(a);this.queueEvent("upload_success_handler",[a,b])};SWFUpload.prototype.uploadComplete=function(a){a=this.unescapeFilePostParams(a);this.queueEvent("upload_complete_handler",a)};SWFUpload.prototype.debug=function(a){this.queueEvent("debug_handler",a)};SWFUpload.prototype.debugMessage=function(c){if(this.settings.debug){var b,a=[];if(typeof c==="object"&&typeof c.name==="string"&&typeof c.message==="string"){for(var d in c){if(c.hasOwnProperty(d)){a.push(d+": "+c[d])}}b=a.join("\n")||"";a=b.split("\n");b="EXCEPTION: "+a.join("\nEXCEPTION: ");SWFUpload.Console.writeLine(b)}else{SWFUpload.Console.writeLine(c)}}};SWFUpload.Console={};SWFUpload.Console.writeLine=function(c){var d,a;try{d=document.getElementById("SWFUpload_Console");if(!d){a=document.createElement("form");document.getElementsByTagName("body")[0].appendChild(a);d=document.createElement("textarea");d.id="SWFUpload_Console";d.style.fontFamily="monospace";d.setAttribute("wrap","off");d.wrap="off";d.style.overflow="auto";d.style.width="700px";d.style.height="350px";d.style.margin="5px";a.appendChild(d)}d.value+=c+"\n";d.scrollTop=d.scrollHeight-d.clientHeight}catch(b){alert("Exception: "+b.name+" Message: "+b.message)}};(function($){$.fn.makeAsyncUploader=function(options){return this.each(function(){var id=$(this).attr("id");var container=$('<div class="image-uploader">');var previewContainer;var image_url=options.value||options.default_value;var title;if(image_url){previewContainer=$('<div class="file-preview">');title=$("#Title").val().replace(/ /g,"_")+"."+image_url.substring(image_url.lastIndexOf(".")+1);previewContainer.append($('<div class="wrapper"><img src="'+image_url+'" alt="'+title+'" class="preview" /><div class="title">'+title+"</div><div>"));previewContainer.append($('<input type="hidden" name="'+id+'"  value="'+image_url+'"/>'));previewContainer.append($('<div class="action"><input type="button" name="'+id+'button"  value="" onclick="clearInputs(\''+id+"','"+options.messages.confirm_delete+"')\" /></div>"))}else{previewContainer=$('<div class="file-preview" style="display:none;">');previewContainer.append($('<div class="wrapper"><img class="preview" /><div class="title"></div><div>'));previewContainer.append($('<input type="hidden" name="'+id+'"  value=""/>'));previewContainer.append($('<div class="action"><input type="button" name="'+id+'button" value="" onclick="clearInputs(\''+id+"')\" /></div>"))}container.append(previewContainer);container.append($('<div class="clear"></div>'));container.append($('<div class="upload-button" id="'+id+'Upload"></div>'));container.append($('<div class="progressbar"></div>'));$(this).before(container).remove();$(".progressbar",container).progressbar({value:0}).hide();var swfu;var width=90,height=27,text_width=70,text_height=10;if(options){width=options.button_width||width;height=options.button_height||height;if(options.button_text){var test_element=$(options.button_text).hide().appendTo("body");text_width=test_element.width();text_height=test_element.height();test_element.remove()}}var defaults={flash_url:"swfupload.swf",upload_url:"/Home/AsyncUpload",file_size_limit:"10 MB",file_types:"*.*",file_types_description:"All Files",debug:false,button_image_url:"blank-button.png",button_placeholder_id:id+"Upload",button_text:'<font face="Arial" size="13pt">Choose file</span>',button_text_left_padding:(width-text_width)/2-8,button_text_top_padding:(height-text_height)/2-3,messages:{success:'File "{0}" has been uploaded successfully. Don\'t forget to save changes.',http_error:"Server internal error. Please try again later.",file_exceed_size_limit:"Selected file exceed size limit ({0}).",invalid_file_type:"You have attempted to upload file of incorrect format.",zero_byte_file:"You have attempted to upload empty file.",unknown_error:"Unknown error was occured.",confirm_delete:"Are you sure you want to delete file?"},file_queued_handler:function(file){swfu.startUpload()},file_queue_error_handler:function(file,error_code,message){switch(error_code){case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:message=this.settings.messages.zero_byte_file;break;case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:message=String.format(this.settings.messages.file_exceed_size_limit,this.settings.file_size_limit);break;case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:message=this.settings.messages.invalid_file_type;break;default:message=this.settings.messages.unknown_error;break}$.display_error(message)},upload_error_handler:function(file,error_code,message){if(error_code==SWFUpload.UPLOAD_ERROR.HTTP_ERROR){message=this.settings.messages.http_error}else{message=this.settings.messages.unknown_error}$.display_error(message)},upload_start_handler:function(){swfu.setButtonDimensions(0,height);$("input[name="+id+"]",container).val("");$(".progressbar",container).show().progressbar("value",0);if(options.disableDuringUpload){$(options.disableDuringUpload).attr("disabled","disabled")}},upload_success_handler:function(file,response){var response=eval("("+response+")");$("input[name="+id+"]",container).val(response.FileName);$("div.title",container).empty();$("div.title",container).append(response.FileTitle);var thumbnailPath=response.Thumbnail?response.Thumbnail:response.FileName;$("img.preview",container).attr({src:thumbnailPath,alt:response.FileTitle});$("img.preview",container).error(function(){$("img.preview",container).css("display","none")});$("div.file-preview",container).css("display","");$.display_notice(String.format(this.settings.messages.success,file.name))},upload_complete_handler:function(){var clearup=function(){$(".progressbar",container).hide();swfu.setButtonDimensions(width,height)};if($("input[name="+id+"]",container).val()!=""){$(".progressbar",container).progressbar("value",100);window.setTimeout(function(){clearup()},1000)}else{clearup()}if(options.disableDuringUpload){$(options.disableDuringUpload).removeAttr("disabled")}},upload_progress_handler:function(file,bytes,total){var percent=100*bytes/total;$(".progressbar",container).progressbar("value",percent)}};swfu=new SWFUpload($.extend(defaults,options||{}))})}})(jQuery);function clearInputs(a,b){if(!confirm(b)){return false}$(".image-uploader input[name="+a+"]").val("");$(".image-uploader img.preview").attr({src:"",alt:""});$(".image-uploader div.title").empty();$(".image-uploader div.file-preview").css("display","none");$.display_notice("File has been deleted successfully")};