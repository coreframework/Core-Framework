<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<div class="tabs">
    <ul>
    <%if(Model.Widget == null || !(Model.Widget is BaseWidget) || Model.Access[((BaseWidget)Model.Widget).ManageOperationCode])
      {%>
        <li><a href="#common"><%:Html.Translate(".Common")%></a></li>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetLookAndFeel(Model.Id))%>"><%:Html.Translate(".LookAndFeel")%></a></li>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetCSS(Model.Id))%>"><%:Html.Translate(".CSS")%></a></li>
        <%
            } if (Model.Widget == null || !(Model.Widget is BaseWidget) || Model.Access[((BaseWidget)Model.Widget).PermissionOperationCode])
        {%>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetPermissions(Model.Id))%>"><%:Html.Translate(".Permissions")%></a></li>
        <%
        }%>
    </ul>
    <%if (Model.Widget == null || !(Model.Widget is BaseWidget) || Model.Access[((BaseWidget)Model.Widget).ManageOperationCode])
      {%>
    <div id="common">
        <%
          Html.RenderPartial(MVC.Shared.Views.Widgets.WidgetCommonSettings, Model);%>
    </div>
    <%
      }%>
</div>
<script type="text/javascript">
    $(function () {
        $(".tabs").tabs({
            ajaxOptions: { cache: false,
                error: function (xhr, status, index, anchor) {
                    $(anchor.hash).html(
                    "Couldn't load this tab. We'll try to fix this as soon as possible");
                }
            }
        });
    });
</script>

