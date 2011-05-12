<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.SiteMapWidgetModel>" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
        <label> Root Page</label>
        <%:Html.DropDownListFor(model => model.RootPageId, new SelectList(Model.RootPages, "Id", "Title", Model.RootPageId), "Please select", new { Class = "select-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.RootPageId) %>

        <%:Html.LabelFor(model=>model.Depth) %>
        <%:Html.DropDownListFor(model => model.Depth,new SelectList(new List<SelectListItem>{new SelectListItem{Value ="1",Text = @"1"},
                                                                                             new SelectListItem{Value ="2",Text = @"2"},
                                                                                             new SelectListItem{Value ="3",Text = @"3"}  }, "Value", "Text", Model.RootPageId), "Please select",
                                                                                                                  new { Class = "select-400 text ui-widget-content ui-corner-all" })%>
        <%:Html.ValidationMessageFor(model=>model.Depth) %>
        <%:Html.CheckBoxFor(model => model.IncludeRootInTree)%>
        <%:Html.LabelFor(model => model.IncludeRootInTree, "Include Root in Tree", new { @class = "checkbx-label" })%>
        <%:Html.ValidationMessageFor(model => model.IncludeRootInTree)%>

    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>

