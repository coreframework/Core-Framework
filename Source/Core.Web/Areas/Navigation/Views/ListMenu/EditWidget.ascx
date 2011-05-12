<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.ListMenuWidgetModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
       <%:Html.Label("Select pages to display") %>
       <%=Html.RenderTree(Model.PagesTree, "list-menu",
                                      model =>Html.Partial(MVC.Navigation.ListMenu.Views.ListMenuPageItem,model).ToString())%>
        <%:Html.LabelFor(model=>model.Orientation) %>
        <%:Html.DropDownListFor("Orientation", Model.Orientation, new { @Class = "select-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.Orientation) %>
    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>

