<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>
<%@ Import Namespace="Core.Web.Helpers" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>
<%@ Import Namespace="Core.Web.Models" %>
<div class="widget <%=Model.Settings != null ? Model.Settings.CustomCSSClasses : String.Empty %>" id="<%=WidgetHelper.GetWidgetClientId(Model.Id) %>">
    <%=Html.Hidden("pageWidgetId", Model.Id) %>
    <%=Html.Hidden("column", Model.Column) %>
    <%=Html.Hidden("order", Model.Order) %>
    <%if(PageHelper.GetCurrentUserPageMode() == PageMode.Edit)
      {%><div class="widget-head">
        <h3>
            <%:Model.Widget != null ? Model.Widget.Title : Html.Translate(".WidgetNotFound")%>
        </h3>
        <%
          if (Model.PageAccess[(int) PageOperations.Update] &&
              (Model.Widget == null || !(Model.Widget is BaseWidget) ||
               Model.Access[((BaseWidget) Model.Widget).ManageOperationCode] ||
               Model.Access[((BaseWidget) Model.Widget).PermissionOperationCode]))
          {
              if (Model.Widget == null || !(Model.Widget is BaseWidget) ||
                  Model.Access[((BaseWidget) Model.Widget).ManageOperationCode])
              {%>

            <%=Ajax.ActionLink("remove",
                                                    MVC.Pages.RemovePageWidget(Model.Id),
                                                    new AjaxOptions
                                                        {
                                                            Confirm = "This widget will be removed, ok?",
                                                            OnSuccess =
                                                                "function(){updateAfterRemoving(this,'.widget');}"
                                                        },
                                                    new {@class = "remove"})%>
    
            <%
              }
              if (Model.Widget != null)
              {%>
                 <a class="edit" href="#">edit</a>
           <%
              }%>
           <%
          }%>
    </div>
    <%
      }%>
    <div class="widget-content" style="<%: WidgetHelper.GetWidgetHolderStyles(Model.Settings) %>">
        <div style="<%: (Model.Settings != null && !String.IsNullOrEmpty(Model.Settings.LookAndFeelSettings.OtherStyles)) ? Model.Settings.LookAndFeelSettings.OtherStyles : String.Empty %>">
            <%if (Model.Widget != null)
              {%>
            <% Html.RenderAction(Model.Widget.ViewAction.Action,
                                Model.Widget.ViewAction.Controller,
                                new
                                    {
                                        instance = Model.WidgetInstance,
                                        area = Model.Widget.ViewAction.Area
                                    });%>
            <% }%>
        </div>
    </div>
</div>
