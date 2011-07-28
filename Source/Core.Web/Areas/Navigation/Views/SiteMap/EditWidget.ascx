<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.SiteMapWidgetModel>" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <label> <%=Html.Translate(".RootPage")%></label>
        <%:Html.DropDownListFor(model => model.RootPageId, new SelectList(Model.RootPages, "Id", "Title", Model.RootPageId), Html.Translate(".PleaseSelect"), new {})%>
        <%:Html.ValidationMessageFor(model=>model.RootPageId) %>
    </div>
     <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.Depth)%>
        <%:Html.DropDownListFor(model => model.Depth,new SelectList(new List<SelectListItem>{new SelectListItem{Value ="1",Text = @"1"},
                                                                                             new SelectListItem{Value ="2",Text = @"2"},
                                                                                             new SelectListItem{Value ="3",Text = @"3"}  }, "Value", "Text", Model.RootPageId), Html.Translate(".PleaseSelect"),
                                                                                                                  new {})%>
         <%:Html.ValidationMessageFor(model=>model.Depth) %>
     </div>
     <div class="form_i">
        <%:Html.CheckBoxFor(model => model.IncludeRootInTree)%>
        <%:Html.LabelFor(model => model.IncludeRootInTree, Html.Translate(".IncludeRootInTree"), new { @class = "checkbx-label" })%>
        <%:Html.ValidationMessageFor(model => model.IncludeRootInTree)%>
     </div>
    <%:Html.AntiForgeryToken()%>
</div>

