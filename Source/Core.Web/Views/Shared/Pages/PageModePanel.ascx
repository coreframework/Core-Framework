<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageModeModel>" %>
<div class="page-mode-block">
    <%:Html.RadioListFor("pageMode", Model.PageMode, new { onchange = "changePageMode($(this).val());" })%>
</div>
<script type="text/javascript">
    $(function () {
        $(".page-mode-block").buttonset();
    });
    function changePageMode(newPageMode) {
        $.ajax({
            type: "POST",
            url: "<%=Url.Action(MVC.Pages.ChangePageMode()) %>",
            data: { pageMode: newPageMode },
            success: function (response) {
                location.reload();
            }
        });
    }
</script>
