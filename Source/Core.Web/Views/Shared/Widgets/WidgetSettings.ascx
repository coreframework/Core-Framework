<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<div class="tabs">
    <ul>
    <%if(Model.SystemWidget == null || !(Model.SystemWidget is BaseWidget) || Model.Access[((BaseWidget)Model.SystemWidget).ManageOperationCode])
      {
          if (Model.SystemWidget != null && Model.SystemWidget.EditAction != null)
          {%>
        <li><a href="#common"><%:Html.Translate(".Common")%></a></li>
        <%
          }%>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetLookAndFeel(Model.Widget.Id))%>"><%:Html.Translate(".LookAndFeel")%></a></li>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetCSS(Model.Widget.Id))%>"><%:Html.Translate(".CSS")%></a></li>
        <%
            } if (Model.SystemWidget == null || !(Model.SystemWidget is BaseWidget) || Model.Access[((BaseWidget)Model.SystemWidget).PermissionOperationCode])
        {%>
        <li><a href="<%=Url.Action(MVC.Pages.ShowWidgetPermissions(Model.Widget.Id))%>"><%:Html.Translate(".Permissions")%></a></li>
        <%
        }%>
    </ul>
    <%if ((Model.SystemWidget == null || !(Model.SystemWidget is BaseWidget) || Model.Access[((BaseWidget)Model.SystemWidget).ManageOperationCode]) && Model.SystemWidget != null && Model.SystemWidget.EditAction != null)
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

