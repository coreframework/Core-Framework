﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Core.Web.Helpers.Layouts" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<table class="widgets">
<% int columnIndex = 1; %>
    <%foreach (PageLayoutRow row in Model.Layout.LayoutTemplate.Rows)
      { %>
    <tr rowid="<%=row.Id %>">
        <%foreach (var column in row.Columns)
          { %>
        <td style="width: <%=LayoutHelper.GetColumnWidth(Model.Layout, column) %>%;" colspan="<%=LayoutHelper.GetColumnColspan(Model.Layout, column) %>"
            class="column">
            <%foreach (var widget in Model.Widgets.FindAll(wd => wd.Widget.ColumnNumber == columnIndex))
              {%>
            <%if (widget.Widget == null || !(widget.SystemWidget is BaseWidget) || widget.Access[((BaseWidget)widget.SystemWidget).ViewOperationCode])
              {%>
            <%Html.RenderPartial(
                MVC.Shared.Views.Widgets.WidgetContentHolder,
                widget);%>
            <%}%>
            <%}%>
        </td>
        <%columnIndex++;
          }
        %>
    </tr>
    <%} %>
</table>
