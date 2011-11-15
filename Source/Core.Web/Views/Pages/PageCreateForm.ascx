<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>
<div id="commonSettings">
    <% using (Ajax.BeginForm("SavePageCommonSettings", "Pages", new AjaxOptions { UpdateTargetId = "commonSettings", OnComplete = "completeUpdates" }))
       { %>
    <%:Html.Messages() %>
    <div class="form_area">
        <%if (TempData["Success"] != null)
          {%>
        <%:Html.Hidden("pageUrl", Url.Action(MVC.Pages.Show(Model.Url)))%>
        <%}%>
        <%:Html.HiddenFor(model => model.Id)%>
        <%:Html.HiddenFor(model => model.ParentPageId)%>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.Title) %><br />
            <%:Html.TextBoxFor(model => model.Title, new { Class = "inp_txt" })%>
            <%:Html.ValidationMessageFor(model => model.Title)%>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.Url) %><br />
            <%:Html.TextBoxFor(model => model.Url, new { Class = "inp_txt" })%>
            <%:Html.ValidationMessageFor(model => model.Url)%>
        </div>
        <div class="form_i">
            <div id="pageTemplateAccordion">
                <h3>
                    <a href="#">Copy from page</a></h3>
                <div>
                    <div class="form_i">
                        <%:Html.LocalizedLabelFor(model=>model.ClonedPageId) %><br />
                        <%:Html.DropDownListFor(model => model.ClonedPageId, new SelectList(Model.AvailablePages, "Id", "Title", Model.ClonedPageId), Html.Translate("Actions.PleaseSelect"), new { })%>
                        <%:Html.ValidationMessageFor(model => model.ClonedPageId)%>
                    </div>
                </div>
                <h3>
                    <a href="#">Copy from template</a></h3>
                <div>
                    <div id="templatesList" class="form_i">
                        <%:Html.LocalizedLabelFor(model=>model.TemplateId) %><br />
                        <%:Html.DropDownListFor(model => model.TemplateId, new SelectList(Model.AvailableTemplates, "Id", "Title", Model.TemplateId), Html.Translate("Actions.PleaseSelect"), new { })%>
                        <%:Html.ValidationMessageFor(model => model.TemplateId)%>
                    </div>
                    <div id="widgetsList" class="form_i" style="display: none">
                        <%:Html.LocalizedLabelFor(model=>model.WidgetId) %><br />
                        <%:Html.DropDownListFor(model => model.WidgetId, new SelectList(Model.AvailableWidgets, "Id", "Title", Model.WidgetId), Html.Translate("Actions.PleaseSelect"), new { })%>
                        <%:Html.ValidationMessageFor(model => model.WidgetId)%>
                    </div>
                </div>
            </div>
        </div>
        <div class="form_i">
            <%:Html.LocalizedLabelFor(model=>model.HideInMainMenu) %>
            <%:Html.CheckBoxFor(model => model.HideInMainMenu)%>
            <%:Html.ValidationMessageFor(model => model.HideInMainMenu)%>
        </div>
        <%:Html.AntiForgeryToken()%>
    </div>
    <div class="p_footer clrfix">
        <div class="btn1">
            <em></em>
            <%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
    </div>
    <script type="text/javascript">
    $(function () {
        $("#pageTemplateAccordion").accordion('destroy').accordion({
            collapsible: true,
            autoHeight: false
        });
        $("#templatesList select").change(function () {
        var templsWithOneHolder = [<%foreach(var template in Model.AvailableTemplates)
                                     {
                                         if (template.PlaceHoldersCount == 1)
                                         {%>'<%=template.Id%>',<%
                                         }
                                     }%>];
        var templId = $(this).val();
        var $widgetsList = $('#widgetsList');
        $('#widgetsList').find('option:first').attr('selected', 'selected').parent('select');
        if(templId && ($.inArray(templId, templsWithOneHolder) > -1))
        {
            if(!$widgetsList.is(":visible")) {
                $('#widgetsList').toggle('fast');
            }
        }
        else {
            if($widgetsList.is(":visible")) {
                $('#widgetsList').toggle('fast');
            }
        }
    });
    });
    function completeUpdates(content) {
        if ($('input[type=hidden]#pageUrl', content.get_data()).length > 0) {
            var pageUrl = $('input[type=hidden]#pageUrl', content.get_data()).val();
            location.href = pageUrl;
        }
    }
    </script>
    <%} %>
</div>
