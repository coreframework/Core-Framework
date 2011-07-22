<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageLocaleViewModel>" %>
<div id="commonSettings">
    <% using (Ajax.BeginForm("UpdatePageCommonSettings", "Pages", new AjaxOptions { UpdateTargetId = "commonSettings", OnComplete = "completeUpdates" }))
       { %>
    <%:Html.Messages() %>
    <div class="form_area">
        <%if (TempData["Success"] != null)
          {%>
        <%:Html.Hidden("pageUrl", Url.Action(MVC.Pages.Show(Model.Url)))%>
        <%}%>
        <%:Html.HiddenFor(model => model.Id)%>
        <%:Html.HiddenFor(model => model.ParentPageId)%>
        <%:Html.HiddenFor(model => model.LocalesString)%>
        <div class="form_i">
            <label>
                Language</label><br />
            <%:Html.DropDownListFor(model => model.SelectedCulture,
                                                             new SelectList(Model.Cultures, "Value", "Key",
                                                                            Model.SelectedCulture),
                                                             new {id = "SelectedCulture"})%>
        </div>
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
        <%:Html.AntiForgeryToken()%>
    </div>
    <div class="p_footer clrfix">
        <div class="btn1">
            <em></em>
            <%: Html.Submit(Html.Translate("Actions.Save"), new { Class = "button" })%><strong></strong></div>
    </div>
    <%} %>
</div>
<script type="text/javascript">
    var locales = {};
    var currentCulture = '<%=Model.SelectedCulture %>';
    
    function completeUpdates(content) {
        if ($('input[type=hidden]#pageUrl', content.get_data()).length > 0) {
            var pageUrl = $('input[type=hidden]#pageUrl', content.get_data()).val();
            location.href = pageUrl;
        }
    }

    $(document).ready(function () {
            $('#SelectedCulture').change(function () {
                locales[currentCulture] = $('#Title').val();
                var postData = {};
                postData.pageId = <%=Model.Id %>;
                postData.culture = $(this).val();
                $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "Pages") %>",
                data: postData,
                success: function (response) {
                    var model = eval('(' + response + ')');
                    $('#Title').val(model.Title);
                    currentCulture = model.Culture;
                }
                });
            });
            $("#commonSettings form input[type=submit]").click(function() {
                locales[currentCulture] = $('#Title').val();
              $('#commonSettings #LocalesString').val(JSON.stringify(locales));
              return true;
            });
        });

</script>
