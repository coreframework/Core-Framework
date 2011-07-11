<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.ChangeLayoutModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Framework.Core.Infrastructure" %>
<div>
    <%:Html.Message(MessageType.Info, Html.Translate(".ChangeLayout"))%>
     <div id="successedResult" style="display: none;" class="validation_success">
        <%:Html.Translate(".Successful")%>
    </div>
    <div id="notSuccessedResult" style="display: none;" class="validation_error">
        <%:Html.Translate(".Error")%>
    </div>
    <div class="form_area">
        <%foreach (PageLayoutTemplate layoutTemplate in Model.Layouts)
          {%>
               <%= Ajax.ActionLink(" ", MVC.Pages.ChangeLayout(Model.PageId, layoutTemplate.Id), new AjaxOptions { UpdateTargetId = "tblLayoutHolder", OnSuccess = "function() {updatePageLayout(this);}" }, new { @class = String.Format("{0}{1}", layoutTemplate.LayoutCssClass,(Model.CurrentLayout.LayoutTemplate == layoutTemplate ? " active" : String.Empty) ) })%>
        <%}%>
    </div>
    <div class="clear"></div>
    <div id="layoutSettings">
        <%Html.RenderPartial(MVC.Shared.Views.Layouts.LayoutSettings, Model);%>
    </div>
</div>
<script type="text/javascript">
    function updatePageLayout(ref) {
        $(ref).addClass("active").siblings().removeClass("active");
        iNettutsInit();
        $.ajax({
            type: "POST",
            url: "<%=Url.Action(MVC.Pages.ShowLayoutSettingsForm(Model.PageId)) %>",
            data: {},
            success: function (response) {
                $('#layoutSettings').html('');
                $('#layoutSettings').html(response);
            }
        });
    }
</script>
