<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.WidgetHolderViewModel>" %>
<%@ Import Namespace="Core.Web.Helpers" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<%@ Import Namespace="Core.Web.NHibernate.Permissions.Operations" %>
<%@ Import Namespace="Core.Web.Models" %>
<div class="widget <%=Model.Widget.Settings != null ? Model.Widget.Settings.CustomCSSClasses : String.Empty %>"
    id="<%=WidgetHelper.GetWidgetClientId(Model.Widget.Id) %>">
    <div class="widget_container"  style="<%: WidgetHelper.GetWidgetStyles(Model.Widget.Settings) %>">
        <%=Html.Hidden("pageWidgetId", Model.Widget.Id) %>
        <%=Html.Hidden("pageSection", Model.Widget.PageSection)%>
        <%=Html.Hidden("column", Model.Widget.ColumnNumber) %>
        <%=Html.Hidden("order", Model.Widget.OrderNumber) %>
        <%if (PageHelper.CurrentUserPageMode == PageMode.Edit)
          {%>
        <div class="widget_title">
            <div class="widget_title_i clrfix">
                <h1>
                    <%:Model.SystemWidget != null &&  Model.Widget!=null && Model.Widget.Widget!=null ? Model.Widget.Widget.Title : Html.Translate(".WidgetNotFound")%>
                </h1>
                <div class="widget_edit">
                    <%if (Model.PageAccess[(int)PageOperations.Update] &&
                (Model.SystemWidget == null || !(Model.SystemWidget is BaseWidget) ||
                Model.Access[((BaseWidget)Model.SystemWidget).ManageOperationCode] ||
                Model.Access[((BaseWidget)Model.SystemWidget).PermissionOperationCode]))
                      {
                          if (Model.SystemWidget == null || !(Model.SystemWidget is BaseWidget) ||
                              Model.Access[((BaseWidget)Model.SystemWidget).ManageOperationCode])
                          {%>
                    <%=Ajax.ActionLink(" ",
                                                    MVC.Pages.RemovePageWidget(Model.Widget.Id),
                                                    new AjaxOptions
                                                        {
                                                            Confirm = Html.Translate("Messages.DeleteConfirm"),
                                                            OnSuccess =
                                                                "function(){updateAfterRemoving(this,'.widget');}"
                                                        },
                                                    new {@class = "remove"})%>
                    <%}
                if (Model.SystemWidget != null)
                {%>
                    <a class="edit" href="javascript:void(0)"></a>
                    <% }%>
                    <% }%>
                </div>
            </div>
        </div>
        <% }%>
        <div class="widget_content" style="<%: WidgetHelper.GetWidgetHolderStyles(Model.Widget.Settings) %>">
            <div class="widget_content_i" style="<%: (Model.Widget.Settings != null && !String.IsNullOrEmpty(Model.Widget.Settings.LookAndFeelSettings.OtherStyles)) ? Model.Widget.Settings.LookAndFeelSettings.OtherStyles : String.Empty %>">
                <%if (Model.SystemWidget != null)
                  {%>
                <% Html.RenderAction(Model.SystemWidget.ViewAction.Action,
                                Model.SystemWidget.ViewAction.Controller,
                                new
                                    {
                                        instance = Model.WidgetInstance,
                                        area = Model.SystemWidget.ViewAction.Area
                                    });%>
                <% }%>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</div>
