////////////////////////////////////////////////////////////////////////////////
//
// Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix
//
// Original Author: Adam M Craven (http://adammcraventech.wordpress.com)
//
// This extension to ASP.NET MVC2 makes any script loaded as part of dynamically loaded content
// be executable and reinitialises the MVC2 client validation to enable that processing to occur
// for an AJAX loaded view.
//
// Warning: This script may not work with future versions of the minimised MicrosoftMvcAjax.js.
//
// When content (e.g. Partial Views) is dynamically loaded into a DOM target element through AJAX
// via the standard ASP.NET MVC2 "Ajax.ActionLink()" or "Ajax.BeginForm()" methods, any script
// (such as javascript or MVC client validation script) that was emitted is not executed because
// the content is assigned to the target element’s "innerHTML" property. This will not cause the
// script to be executed or execuatable.
//
// Must be included after jQuery and the MicrosoftMvcValidation javascript, typically like this:
//    <script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery-1.4.1.min.js") %>"></script>
//    <script type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftAjax.js") %>"></script>
//    <script type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftMvcAjax.js") %>"></script>
//    <script type="text/javascript" src="<%=Url.Content("~/Scripts/MicrosoftMvcValidation.js") %>"></script>
//    <script type="text/javascript" src="<%=Url.Content("~/Scripts/AjaxLoadedContentScriptFix.js") %>"></script>
//

Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix = function Sys_Mvc_MvcHelpers_AjaxLoadedContentScriptFix() {
}

Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._onComplete = function Sys_Mvc_MvcHelpers_AjaxLoadedContentScriptFix$_onComplete(request, ajaxOptions, ajaxContext) {
    /// <param name="request" type="Sys.Net.WebRequest">
    /// </param>
    /// <param name="ajaxOptions" type="Sys.Mvc.AjaxOptions">
    /// </param>
    /// <param name="ajaxContext" type="Sys.Mvc.AjaxContext">
    /// </param>

    // Hook into the ajaxOptions.onSuccess delegate
    ajaxContext._ajaxLoadedContentScriptFixOrigAjaxOptionsOnSuccess = ajaxOptions.onSuccess;
    ajaxOptions.onSuccess = Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._ajaxOptionsOnSuccess;

    // Call the original MVC onComplete method
    Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._origOnComplete(request, ajaxOptions, ajaxContext);
}

Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._ajaxOptionsOnSuccess = function Sys_Mvc_MvcHelpers_AjaxLoadedContentScriptFix$_onSuccess(ajaxContext) {
    /// <param name="ajaxContext" type="Sys.Mvc.AjaxContext">
    /// </param>

    // Make any dynamically loaded script execute
    Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._globalEvalScriptInElementId(ajaxContext.get_updateTarget());

    // Reinitialise the MVC validation
    Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._reinitialiseMvcValidation();

    // Call the original success delegate
    if (ajaxContext._ajaxLoadedContentScriptFixOrigAjaxOptionsOnSuccess) {
        ajaxContext._ajaxLoadedContentScriptFixOrigAjaxOptionsOnSuccess(ajaxContext);
    }
}

Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._globalEvalScriptInElementId = function GlobalEvalScriptInElementId(element) {
    if (jQuery) {
        // jQuery.globalEval($("#" + element.id).find("script").text());
        if (element != null) {
            // It seems jQuery 1.4.1 & 1.4.2 has a problem in IE with .text() on script nodes… so do the loop ourselves…
            var scripts = $("#" + element.id).find("script");
            var allScriptText = "";
            for (var i = 0; i < scripts.length; i++) {
                allScriptText += scripts[i].text;
            }
            jQuery.globalEval(allScriptText);
        }
    } else {
        alert("Error: jQuery must be loaded in order to use Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix");
    }
}

Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._reinitialiseMvcValidation = function Sys_Mvc_MvcHelpers_AjaxLoadedContentScriptFix$ReinitialiseMvcValidation() {
    if (Sys.Mvc.FormContext) {
        Sys.Application.remove_load(arguments.callee);
        Sys.Mvc.FormContext._Application_Load();
    }
}

// Register this extension
Sys.Application.add_load(function () {

    if (typeof (Sys.Mvc) === 'undefined' || typeof (Sys.Mvc.MvcHelpers) === 'undefined' ||
        (!Sys.Mvc.MvcHelpers._onComplete && !Sys.Mvc.MvcHelpers.$3)) alert("Error: MicrosoftAjax and MicrosoftMvcAjax.js (or their debug versions) must be loaded in order to use Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix");

    var isMicrosoftMvcAjaxDebugJs = Sys.Mvc.MvcHelpers._onComplete;

    if (isMicrosoftMvcAjaxDebugJs) { // if using MicrosoftMvcAjax.Debug.js
        Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._origOnComplete = Sys.Mvc.MvcHelpers._onComplete;
        Sys.Mvc.MvcHelpers._onComplete = Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._onComplete;
    } else { // using MicrosoftMvcAjax.js
        Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._origOnComplete = Sys.Mvc.MvcHelpers.$3;
        Sys.Mvc.MvcHelpers.$3 = Sys.Mvc.MvcHelpers.AjaxLoadedContentScriptFix._onComplete;
    }
});