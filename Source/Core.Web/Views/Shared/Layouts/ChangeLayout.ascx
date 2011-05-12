<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.ChangeLayoutModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<div style="height: 270px;">
    Click on any of the layout template to change it on the page
    <br />
    <br />
    <div>
        <%foreach (PageLayoutTemplate layoutTemplate in Model.Layouts)
          {%>
        <%= Ajax.ActionLink(" ", MVC.Pages.ChangeLayout(Model.PageId, layoutTemplate.Id), new AjaxOptions { UpdateTargetId = "tblLayoutHolder", OnSuccess = "function() {updatePageLayout(this);}" }, new { @class = String.Format("{0}{1}", layoutTemplate.LayoutCssClass,(Model.CurrentLayout.LayoutTemplate == layoutTemplate ? " active" : String.Empty) ) })%>
        <%
            }%>
    </div>
    <div id="layoutSettings" style="clear: left; padding-top:15px;">
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
