<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.ListMenuWidgetModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <label> <%=Html.Translate(".SelectPagesToDisplay")%></label>
       <%=Html.RenderTree(Model.PagesTree, "list-menu",
                                      model =>Html.Partial(MVC.Navigation.ListMenu.Views.ListMenuPageItem,model).ToString())%>
    </div>
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model=>model.Orientation) %><br/>
        <%:Html.DropDownListFor("Orientation", Model.Orientation, new {})%>
        <%:Html.ValidationMessageFor(model=>model.Orientation) %>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>

