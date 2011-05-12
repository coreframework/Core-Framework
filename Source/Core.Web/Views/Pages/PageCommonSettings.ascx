<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>

<% using (Ajax.BeginForm("UpdatePageCommonSettings", "Pages", new AjaxOptions { UpdateTargetId = "commonSettings",OnComplete = "completeUpdates"}))
   { %>
    <div id="commonSettings" style="height:190px;">
        <h2 class="settings-header">
            <%=Html.Translate(".CommonSettings") %>
        </h2>
        <div class="form_area">
            <%if (TempData["Success"] != null) {%>
                <%:Html.Hidden("pageUrl", Url.Action(MVC.Pages.Show(Model.Url)))%>
            <%}%>
            <%:Html.HiddenFor(model => model.Id)%>
            <%:Html.HiddenFor(model => model.ParentPageId)%>
           
            <fieldset>
                <label>
                    Title
                </label>
                <%:Html.TextBoxFor(model => model.Title, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all"})%>
                <%:Html.ValidationMessageFor(model => model.Title)%>
               <label>
                    Url
                </label>
                <%:Html.TextBoxFor(model => model.Url, new { Class = "colorPicker text-400 text ui-widget-content ui-corner-all"})%>
                <%:Html.ValidationMessageFor(model => model.Url)%>
            </fieldset>
            <%:Html.AntiForgeryToken()%>
        </div>
        <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
            <div class="ui-dialog-buttonset">
                <%: Html.Submit("Save", new { Class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" })%>
            </div>
        </div>
    </div>
<%} %>
<script type="text/javascript">
    function completeUpdates(content) {

        if ($('input[type=hidden]#pageUrl', content.get_data()).length > 0) {
            var pageUrl = $('input[type=hidden]#pageUrl', content.get_data()).val();
            location.href = pageUrl;
        }
     }
</script>